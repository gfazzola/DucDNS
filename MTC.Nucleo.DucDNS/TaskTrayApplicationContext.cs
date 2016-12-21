using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using MTC.Host.IComun;
namespace MTC.Nucleo.DucDNS
{
    public class TaskTrayApplicationContextBase : ApplicationContext
    {
        #region variables
        protected MenuItem itemModoServicio, itemModoServicio_iniciarDetener, itemModoServicio_reiniciar, itemModoServicio_instalarDesinstalar,
           itemModoServicio_iniciarDetenerYSalir, itemModoAplicacion, itemModoAplicacion_iniciarDetener, itemModoAplicacion_iniciarDetenerYSalir,
           itemLogDeEventos, itemConfigurador, itemAbrirCarpetaContenedora,
           exitMenuItem;

        //1.0.0.1
        private readonly Subscriber<int> suscriberConectadoAInternet;
        private readonly Subscriber<string> suscriberIpPublica, suscriberNuevaVersion;

        MonitorEstadoServicio monitorEstadoServicio = null;
        protected NotifyIcon notifyIcon = new NotifyIcon();
        public delegate_ponerEstadoServicio m_ponerEstadoServicio = null;
        Dictionary<string, object> parametros = null;
        private bool nucleoIniciadoComoApp = false;

        //1.0.0.1
        private int conexionInternetAnt = -1;
        private string ipAnt = "";
        #endregion

        public TaskTrayApplicationContextBase() { }

        public TaskTrayApplicationContextBase(object[] args)
        {
            try
            {
                parametros = args[0] as Dictionary<string, object>;
                Thread.CurrentThread.CurrentUICulture = string.IsNullOrEmpty(nucleo.cultura) ? Thread.CurrentThread.CurrentCulture : new CultureInfo(nucleo.cultura);
                m_ponerEstadoServicio = new delegate_ponerEstadoServicio(ponerEstadoServicio);
                inicializar();
                ponerEstadoServicio();
                iniciarMonitoreoServicio();

                #region 1.0.0.1
                notifyIcon.BalloonTipTitle = string.Format("{0} V{1}", nucleo.nombre, nucleo.productVersion);
                notifyIcon.BalloonTipText = Properties.Resources.infoAccesoAConfig;
                notifyIcon.ShowBalloonTip(15000);

                suscriberConectadoAInternet = new Subscriber<int>(nucleo.publicadorConectadoAInternet);
                suscriberConectadoAInternet.Publisher.DataPublisher += Publisher_DataPublisher_CambioConectadoAInternet;

                suscriberIpPublica = new Subscriber<string>(nucleo.publicadorIpPublica);
                suscriberIpPublica.Publisher.DataPublisher += Publisher_DataPublisher_CambioIpPublica;

                suscriberNuevaVersion = new Subscriber<string>(nucleo.publicadorNuevaVersionDisponible);
                suscriberNuevaVersion.Publisher.DataPublisher += Publisher_DataPublisher_NuevaVersionDisponible;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion("TaskTrayApplicationContextBase: ", ex));
            }
        }

        //1.0.0.1 se hizo el cambio de la interfaz a la clase directamente para poder suscribirnos a los eventos que nos interesan
        // protected INucleo nucleo { get { return parametros["nucleo"] as INucleo; } }
        protected NucleoDucDNS nucleo { get { return parametros["nucleo"] as NucleoDucDNS; } }

        protected string nombreServicio { get { return parametros["nombreServicio"] as string; } }
        protected string appHost { get { return parametros["appHost"] as string; } }

        protected void iniciarMonitoreoServicio()
        {
            monitorEstadoServicio = new MonitorEstadoServicio(nombreServicio);
            monitorEstadoServicio.eventoCambioEstadoServicio += monitorEstadoServicio_eventoCambioEstadoServicio;
            monitorEstadoServicio.iniciar();
        }

        void monitorEstadoServicio_eventoCambioEstadoServicio(object sender, EventoCambioEstadoServicioParamArgs e)
        {
            ponerEstadoServicio();
        }

        protected System.ServiceProcess.ServiceControllerStatus estadoServicio
        {
            get
            {
                return MTCServiceInstaller.GetServiceStatus2(nombreServicio);
            }
        }

        protected bool servicioInstalado { get { return MTCServiceInstaller.ServiceIsInstalled(nombreServicio); } }

        protected string descripcionEstadoServicio
        {
            get
            {
                if (servicioInstalado)
                    switch (estadoServicio)
                    {
                        case System.ServiceProcess.ServiceControllerStatus.Running:
                            return string.Format("{0}", Properties.Resources.servicioEnEjecucion);
                        case System.ServiceProcess.ServiceControllerStatus.Stopped:
                            return string.Format("{0}", Properties.Resources.servicioDetenido);
                        default:
                            return string.Format("{0}", Properties.Resources.servicioEstadoDesconocido);
                    }
                else
                    return string.Format("{0}", Properties.Resources.servicioNoInstalado);
            }
        }

        public void ponerEstadoServicio()
        {
            string texto = "";
            if (MTCServiceInstaller.ServiceIsInstalled(nombreServicio))
                switch (estadoServicio)
                {
                    case System.ServiceProcess.ServiceControllerStatus.Running:
                        notifyIcon.Icon = Properties.Resources.Ejecutando;
                        texto = string.Format("{0} {1} {2}", nombreServicio, nucleo.productVersion, descripcionEstadoServicio);
                        itemModoAplicacion.Enabled = false;
                        break;

                    case System.ServiceProcess.ServiceControllerStatus.Stopped:
                        notifyIcon.Icon = Properties.Resources.Detenido;
                        texto = string.Format("{0} {1} {2}", nombreServicio, nucleo.productVersion, descripcionEstadoServicio);
                        itemModoAplicacion.Enabled = true;
                        itemModoAplicacion_iniciarDetenerYSalir.Enabled = false;
                        break;

                    default:
                        notifyIcon.Icon = Properties.Resources.SinConfigurar;
                        texto = string.Format("{0} {1} {2}", nombreServicio, nucleo.productVersion, descripcionEstadoServicio);
                        itemModoAplicacion.Enabled = true;
                        break;
                }
            else
            {
                notifyIcon.Icon = Properties.Resources.SinConfigurar;
                texto = string.Format("{0} {1}", nombreServicio, Properties.Resources.servicioNoInstalado);
                itemModoAplicacion.Enabled = true;
            }

            ponerHint(texto);
        }

        private void ponerHint(string texto)
        {
            if (texto.Length > 63)
                NotifyIconFix.SetNotifyIconText(notifyIcon, texto);
            else
                notifyIcon.Text = texto;
        }

        protected virtual void Exit(object sender, EventArgs e)
        {
            try
            {
                if (nucleoIniciadoComoApp)
                {
                    iniciarODetenerAplicacion();
                }

                nucleo.Dispose();

                destruirMonitorEstadoServicio();
                // We must manually tidy up and remove the icon before we exit.
                // Otherwise it will be left behind until the user mouses over.
                notifyIcon.Visible = false;

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion("Exit: ", ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void destruirMonitorEstadoServicio()
        {
            if (monitorEstadoServicio == null)
                return;
            try
            {
                monitorEstadoServicio.eventoCambioEstadoServicio -= monitorEstadoServicio_eventoCambioEstadoServicio;
                monitorEstadoServicio.Dispose();
                monitorEstadoServicio = null;
            }
            catch (Exception ex) { MessageBox.Show(Utils.armarMensajeErrorExcepcion("destruirMonitorEstadoServicio: ", ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected bool detenerOIniciar(bool detener)
        {
            bool result = false;
            System.ServiceProcess.ServiceControllerStatus estado = estadoServicio;
            if (detener && estado == System.ServiceProcess.ServiceControllerStatus.Stopped ||
                !detener && estado == System.ServiceProcess.ServiceControllerStatus.Running)
                goto salida;

            string error = "";

            if (detener)
                MTCServiceInstaller.StopService2(nombreServicio, out error);
            else
                MTCServiceInstaller.StartService2(nombreServicio, out error);

            result = string.IsNullOrEmpty(error);
            if (!result)
                MessageBox.Show(error, Properties.Resources.errorEjecucionProceso, MessageBoxButtons.OK, MessageBoxIcon.Error);
            salida:
            return result;
        }

        protected void inicializar()
        {
            try
            {
                itemModoServicio_iniciarDetener = new MenuItem(Properties.Resources.servicioIniciar, new EventHandler(itemIniciarDetenerServicio_Click));
                itemModoServicio_reiniciar = new MenuItem(Properties.Resources.servicioReiniciar, new EventHandler(itemReiniciar_Click));
                itemModoServicio_instalarDesinstalar = new MenuItem(Properties.Resources.servicioInstalar, new EventHandler(itemInstalarDesinstalar_Click));
                itemModoServicio_iniciarDetenerYSalir = new MenuItem(Properties.Resources.servicioDetenerYSalir, new EventHandler(iniciarODetenerServicioYSalir));

                itemModoServicio = new MenuItem(Properties.Resources.servicio);
                itemModoServicio.MenuItems.Add(itemModoServicio_iniciarDetener);
                itemModoServicio.MenuItems.Add(itemModoServicio_iniciarDetenerYSalir);
                itemModoServicio.MenuItems.Add(itemModoServicio_reiniciar);
                itemModoServicio.MenuItems.Add(new MenuItem("-"));
                itemModoServicio.MenuItems.Add(itemModoServicio_instalarDesinstalar);

                itemModoAplicacion_iniciarDetener = new MenuItem(Properties.Resources.servicioIniciar, new EventHandler(itemIniciarDetenerAplicacion_Click));
                itemModoAplicacion_iniciarDetenerYSalir = new MenuItem(Properties.Resources.servicioDetenerYSalir, new EventHandler(iniciarODetenerAplicacionYSalir));
                itemModoAplicacion = new MenuItem(Properties.Resources.aplicacion);
                itemModoAplicacion.MenuItems.Add(itemModoAplicacion_iniciarDetener);

                itemModoAplicacion.MenuItems.Add(itemModoAplicacion_iniciarDetenerYSalir);
                itemConfigurador = new MenuItem(Properties.Resources.configurar, new EventHandler(itemConfigurador_Click));
                itemAbrirCarpetaContenedora = new MenuItem(Properties.Resources.abrirCarpetaContenedora, new EventHandler(itemAbrirCarpetaContenedora_Click));

                exitMenuItem = new MenuItem(Properties.Resources.itemSalir, new EventHandler(Exit));

                notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                    itemModoServicio,
                    new MenuItem("-"),
                    itemModoAplicacion,

                    new MenuItem("-"),  itemConfigurador, itemAbrirCarpetaContenedora, new MenuItem("-"), exitMenuItem
                    });

                notifyIcon.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);
                notifyIcon.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion("inicializar: ", ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ContextMenu_Popup(object sender, EventArgs e)
        {
            ponerItemAccionSobreServicio();
        }

        private void ponerItemAccionSobreServicio()
        {
            itemModoServicio_iniciarDetenerYSalir.Enabled = itemModoServicio_reiniciar.Enabled = itemModoServicio_iniciarDetener.Enabled = servicioInstalado;
            if (itemModoServicio_iniciarDetener.Enabled)
                switch (estadoServicio)
                {
                    case System.ServiceProcess.ServiceControllerStatus.Running:
                        itemModoServicio_iniciarDetener.Text = Properties.Resources.detener;
                        itemModoServicio_iniciarDetenerYSalir.Enabled = itemModoServicio_reiniciar.Enabled = true;
                        itemModoServicio_iniciarDetenerYSalir.Text = Properties.Resources.servicioDetenerYSalir;
                        break;
                    case System.ServiceProcess.ServiceControllerStatus.Stopped:
                        itemModoServicio_iniciarDetener.Text = Properties.Resources.servicioIniciar;
                        itemModoServicio_iniciarDetenerYSalir.Enabled = itemModoServicio_reiniciar.Enabled = true;
                        itemModoServicio_iniciarDetenerYSalir.Text = Properties.Resources.servicioIniciarYSalir;
                        break;
                    default:
                        itemModoServicio_iniciarDetenerYSalir.Enabled = itemModoServicio_reiniciar.Enabled = itemModoServicio_iniciarDetener.Enabled = false;
                        break;
                }

            itemModoServicio_instalarDesinstalar.Text = itemModoServicio_iniciarDetener.Enabled ? Properties.Resources.servicioDesinstalar : Properties.Resources.servicioInstalar;
        }

        #region eventos del menu

        private void iniciarODetenerServicioYSalir(object sender, EventArgs e)
        {
            if (detenerOIniciar(itemModoServicio_iniciarDetenerYSalir.Text == Properties.Resources.servicioDetenerYSalir))
                Exit(sender, e);
        }

        private void itemIniciarDetenerServicio_Click(object sender, EventArgs e)
        {
            System.ServiceProcess.ServiceControllerStatus estado = estadoServicio;
            if (MessageBox.Show(string.Format(Properties.Resources.operacionSobreServicio,
                estado == System.ServiceProcess.ServiceControllerStatus.Stopped ?
                Properties.Resources.servicioIniciar.ToLower() : Properties.Resources.detener, nombreServicio), Properties.Resources.K_CONFIRME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            string error = "";
            if (estado == System.ServiceProcess.ServiceControllerStatus.Stopped)
                MTCServiceInstaller.StartService2(nombreServicio, out error);
            else
                MTCServiceInstaller.StopService2(nombreServicio, out error);

            if (!string.IsNullOrEmpty(error))
                MessageBox.Show(error, Properties.Resources.errorEjecucionProceso, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                ponerEstadoServicio();
        }


        private bool iniciarODetenerAplicacion()
        {
            bool result = false;
            try
            {
                string texto = "";
                if (!nucleo.iniciado)
                {
                    string error = "";
                    nucleo.iniciar(out error);
                    itemModoAplicacion_iniciarDetener.Text = Properties.Resources.detener;
                    itemModoServicio.Enabled = false;
                    itemModoAplicacion_iniciarDetenerYSalir.Enabled = true;
                    texto = string.Format("{0} {1} {2}", nombreServicio, nucleo.productVersion, Properties.Resources.ejecutandoComoApp);
                    notifyIcon.Icon = Properties.Resources.Ejecutando;
                    nucleoIniciadoComoApp = true;
                }
                else
                {
                    nucleo.detener();
                    itemModoAplicacion_iniciarDetener.Text = Properties.Resources.servicioIniciar;
                    itemModoServicio.Enabled = true;
                    itemModoAplicacion_iniciarDetenerYSalir.Enabled = false;
                    texto = string.Format("{0} {1} {2}", nombreServicio, nucleo.productVersion, Properties.Resources.servicioDetenido);
                    notifyIcon.Icon = Properties.Resources.Detenido;
                    nucleoIniciadoComoApp = false;
                }
                ponerHint(texto);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion("iniciarODetenerAplicacion: ", ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void itemIniciarDetenerAplicacion_Click(object sender, EventArgs e)
        {
            iniciarODetenerAplicacion();
        }

        private void iniciarODetenerAplicacionYSalir(object sender, EventArgs e)
        {
            if (iniciarODetenerAplicacion())
                Exit(sender, e);
        }

        private void itemInstalarDesinstalar_Click(object sender, EventArgs e)
        {
            try
            {
                bool desinstala = string.Compare(itemModoServicio_instalarDesinstalar.Text, Properties.Resources.servicioDesinstalar, true) == 0;
                if (MessageBox.Show(string.Format(Properties.Resources.operacionSobreServicio,
                    itemModoServicio_instalarDesinstalar.Text.ToLower(),
                    nombreServicio), Properties.Resources.K_CONFIRME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                    return;

                if (itemModoServicio_iniciarDetener.Enabled && estadoServicio == System.ServiceProcess.ServiceControllerStatus.Running)
                    MTCServiceInstaller.StopService(nombreServicio);

                string error = "";

                if (desinstala)
                {
                    Utils.ejecutarProceso2(true, appHost, " /u", System.IO.Path.GetDirectoryName(appHost), true, true, out error);

                    if (!MTCServiceInstaller.ServiceIsInstalled(nombreServicio))
                    {
                        Utils.setAppAlIniciarWindows(nombreServicio, "", false);//esto parece que no anda
                    }
                }
                else
                {
                    string parametros = " /i";

                    Utils.ejecutarProceso2(true, appHost, parametros, System.IO.Path.GetDirectoryName(appHost), true, true, out error);
                    if (MTCServiceInstaller.ServiceIsInstalled(nombreServicio))
                    {
                        var exeAppHost = string.Format("\"{0}\" /w", appHost);
                        Utils.setAppAlIniciarWindows(nombreServicio, exeAppHost, true);
                    }
                }

                ponerEstadoServicio();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemConfigurador_Click(object sender, EventArgs e)
        {
            nucleo.configurar();
        }

        private void itemReiniciar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format(Properties.Resources.confirmeReinicioServicio,
              nombreServicio), Properties.Resources.K_CONFIRME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            string error = "";
            MTCServiceInstaller.StopService2(nombreServicio, out error);

            if (string.IsNullOrEmpty(error))
                MTCServiceInstaller.StartService2(nombreServicio, out error);

            if (!string.IsNullOrEmpty(error))
                MessageBox.Show(error, Properties.Resources.errorEjecucionProceso, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                ponerEstadoServicio();
        }

        private void itemAbrirCarpetaContenedora_Click(object sender, EventArgs e)
        {
            Utils.ejecutarProceso(nucleo.path, false);
        }

        #endregion

        #region 1.0.0.1

        /// <summary>
        /// 1.0.0.1
        /// 0 no hay conexion
        /// 1 hay conexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Publisher_DataPublisher_CambioConectadoAInternet(object sender, MessageArgument<int> e)
        {
            if (conexionInternetAnt != -1)
            {
                if (conexionInternetAnt == 0 && e.Message == 1)
                {
                    notifyIcon.BalloonTipText = Properties.Resources.conexionInternetReestablecida;
                    notifyIcon.ShowBalloonTip(10000);
                }
                else
                    if (conexionInternetAnt == 1 && e.Message == 0)
                {
                    notifyIcon.BalloonTipText = Properties.Resources.perdidaConexionInternet;
                    notifyIcon.ShowBalloonTip(10000);
                }
            }
            conexionInternetAnt = e.Message;
        }

        /// <summary>
        /// 1.0.0.1
        /// e= el valor de la ip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Publisher_DataPublisher_CambioIpPublica(object sender, MessageArgument<string> e)
        {
            if (ipAnt != e.Message)
            {
                notifyIcon.BalloonTipText = (ipAnt == "" ? Properties.Resources.direccionIP : Properties.Resources.cambioDireccionIP) + " " + e.Message;
                notifyIcon.ShowBalloonTip(10000);
               // nucleo.logear2(TipoLog.INFO, " 1 " + notifyIcon.BalloonTipText);
            }

            ipAnt = e.Message;
        }

        private void Publisher_DataPublisher_NuevaVersionDisponible(object sender, MessageArgument<string> e)
        {
            notifyIcon.BalloonTipText = e.Message;
            notifyIcon.ShowBalloonTip(10000);

           // nucleo.logear2(TipoLog.INFO, " 2 "+ notifyIcon.BalloonTipText);
        }
        #endregion
    }

    public class TaskTrayApplicationContext : TaskTrayApplicationContextBase
    {
        public TaskTrayApplicationContext(object[] args)
            : base(args)
        {

        }

    }
}