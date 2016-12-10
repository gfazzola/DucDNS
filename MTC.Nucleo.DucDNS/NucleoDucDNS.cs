using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Configuration;
using MTC.Host.IComun;
using System.Linq;
namespace MTC.Nucleo.DucDNS
{
    public partial class NucleoDucDNS : NucleoBase, INucleo
    {
        ColaProcesamiento<Notificacion> colaNotificaciones = null;
        bool _disposed = false, _actualizacionDetenida=false;
        object lockEntidad = new object();
        public ConfiguracionServidorDucDNS configuracionServidorDucDNS;
        public string seccionConfiguracionServicio = "ConfiguracionServidorDucDNS";
        protected static TaskTrayApplicationContext _contextoAplicacion = null;
        private System.Timers.Timer timerMinuto = null;

        public string ipPublica = "", ipPublicaAnt = "";

        public int conectadoAInternet = 0;
        int cantMinutos = 0,  conectadoAInternetAnt = -1;
        public readonly IPublisher<int> publicadorConectadoAInternet;
        public readonly IPublisher<string> publicadorIpPublica, publicadorTickMinuto;
        private readonly string __passPhrase = "MTCSistemas.com";

        public NucleoDucDNS(Dictionary<string, object> parametros): base(parametros)
        {
            buscarInfoEnsamblado(System.Reflection.Assembly.GetExecutingAssembly());
            _nombre = this.GetType().Name;

            var archivoConfig = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location)+".config";
            inicializar(archivoConfig);
            configuracionServidorDucDNS = (ConfiguracionServidorDucDNS)config.GetSection(seccionConfiguracionServicio);
            if (configuracionServidorDucDNS == null)
                throw new Exception(string.Format(Properties.Resources.seccionNoEncontrada, seccionConfiguracionServicio));

            publicadorConectadoAInternet = new Publisher<int>();
            publicadorIpPublica = new Publisher<string>();
            publicadorTickMinuto = new Publisher<string>();

            colaNotificaciones = new ColaProcesamiento<Notificacion>();
            colaNotificaciones.nuevaTarea += Cola_nuevaTarea;
        }

        #region propiedades
        public string passPhrase { get { return __passPhrase; } }

        public bool actualizacionDetenida { get { return _actualizacionDetenida; } set { _actualizacionDetenida = value; } }

#endregion

        public bool iniciar(out string error)
        {
            bool result = false;
            error = "";
            log.Info(string.Format(Properties.Resources.iniciandoNucleo, _nombre, productVersion, productVersionHostContenedor));
            iniciarTimerActualizacion();
            _iniciado = result = true;
            _actualizacionDetenida = false;
            log.Info(Properties.Resources.nucleoIniciado);
            return result;
        }

        private void iniciarTimerActualizacion()
        {
            if (timerMinuto == null)
            {
                timerMinuto = new System.Timers.Timer();
                timerMinuto.Elapsed += timerMinuto_Elapsed;
                timerMinuto.Interval = 2000;//para que inicie rapido
            }
            timerMinuto.Enabled = true;
            cantMinutos = 0;
        }

        private void timerMinuto_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timerMinuto.Enabled = false;
            if (timerMinuto.Interval == 2000)
                timerMinuto.Interval = 60000;//ajusto a un minuto
            procesar();
            timerMinuto.Enabled = true;
        }

        public void detener()
        {
            try
            {
                if (timerMinuto != null)
                    timerMinuto.Enabled = false;
                _iniciado = false;
                log.Info(Properties.Resources.nucleoDetenido);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public ApplicationContext contextoAplicacion(object[] args)
        {
            if (_contextoAplicacion == null)
                _contextoAplicacion = new TaskTrayApplicationContext(args);
            return _contextoAplicacion;
        }

        public void cambioDeSesion(SessionChangeDescription changeDescription)
        {
        }

        public void configurar()
        {
            if (configuracionServidorDucDNS.adminRequierePassword)
            {
                var formCredenciales = new formCredenciales(validarCredenciales);
                if (formCredenciales.ShowDialog() != DialogResult.OK)
                    return;
            }

            var form = new mainForm(this);
            form.ShowDialog();
        }

        private object validarCredenciales(object sender)
        {
            return Encriptacion.getMd5Hash(sender as string) == configuracionServidorDucDNS.passwordAdmin;
        }

        public void guardarConfiguracion()
        {
            try
            {
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(seccionConfiguracionServicio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void procesar()
        {
            try
            {
                publicadorTickMinuto.PublishData(DateTime.Now.ToString());
                bool huboCambios = cantMinutos == 0, huboCambioIP = false, huboCambioConexionAInternet = false;

                string error = "";

                if (huboCambios)
                {
                    conectadoAInternet = Utils.conexionAInternet ? 1 : 0;
                    ipPublica = Utils.ipPublica(out error);
                }
                else
                {
                    conectadoAInternetAnt = conectadoAInternet;
                    conectadoAInternet = Utils.conexionAInternet ? 1 : 0;
                    huboCambioConexionAInternet = conectadoAInternet != conectadoAInternetAnt;

                    ipPublicaAnt = ipPublica;
                    ipPublica = Utils.ipPublica(out error);

                    huboCambioIP = huboCambios = !string.IsNullOrEmpty(ipPublicaAnt) && !string.IsNullOrEmpty(ipPublica) && ipPublicaAnt != ipPublica;

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug(string.Format("huboCambioIP: {0}. ipPublica: {1}. ipPublicaAnt: {2}. conectadoAInternet: {3}. conectadoAInternetAnt: {4}",
                            huboCambioIP, ipPublica, ipPublicaAnt, conectadoAInternet, conectadoAInternetAnt));
                }

                if (huboCambioIP)
                {
                    colaNotificaciones.encolar(new Notificacion()
                    {
                        tipoNotificacion = TipoNotificacion.CambioIP,
                        fecha = DateTime.Now,
                        asunto = Properties.Resources.cambioDireccionIP,
                        detalles = ""
                    });

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug("Se encolo la notific. de cambio en la direccion IP ");
                }

                if (conectadoAInternet == 0 && conectadoAInternetAnt == 1)
                {
                    colaNotificaciones.encolar(new Notificacion()
                    {
                        tipoNotificacion = TipoNotificacion.PerdidaConexionInternet,
                        fecha = DateTime.Now,
                        asunto = Properties.Resources.perdidaConexionInternet,
                        detalles = ""
                    });

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug("Se encolo la notific. de perdida de conexion a internet");
                }
                else
                if (conectadoAInternet == 1 && conectadoAInternetAnt == 0)
                {
                    colaNotificaciones.encolar(new Notificacion()
                    {
                        tipoNotificacion = TipoNotificacion.ReestablecimientoConexionInternet,
                        fecha = DateTime.Now,
                        asunto = Properties.Resources.conexionInternetReestablecida,
                        detalles = ""
                    });

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug("Se encolo la notific. de reestablecim de conexion a internet");
                }

                if (!_actualizacionDetenida && ((cantMinutos % configuracionServidorDucDNS.minutosActualizacion) == 0 || huboCambios))
                    actualizarDominios();

                cantMinutos++;

                publicadorConectadoAInternet.PublishData(conectadoAInternet);
                publicadorIpPublica.PublishData(ipPublica);

                if (!string.IsNullOrEmpty(error))
                {
                    colaNotificaciones.encolar(new Notificacion()
                    {
                        tipoNotificacion = TipoNotificacion.OtrosErrores,
                        fecha = DateTime.Now,
                        asunto = Properties.Resources.errorIntentoObtencionIpPublica,
                        detalles = error
                    });

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug("Se encolo la notific. de error en el intento de obtener la IP publica");
                }
            }
            catch(Exception ex)
            {
                log.Error(Utils.armarMensajeErrorExcepcion("procesar: ", ex));
            }
        }

        public void actualizarDominios() { _actualizarDominios(); }

        private void _actualizarDominios()
        {
            string token = "";
            if (string.IsNullOrEmpty(configuracionServidorDucDNS.token))
            {
                log.Error(Properties.Resources.tokenNoDefinido);
                return;
            }

            if (configuracionServidorDucDNS.modoDebug == 2)
                log.Debug("actualizarDominios");

            try
            {
                token = Encriptacion.desencriptar(configuracionServidorDucDNS.token, __passPhrase);

                string html = string.Empty, protocolo = configuracionServidorDucDNS.https ? "https" : "http",
                    verbose = configuracionServidorDucDNS.verbose ? "true" : "false";

                var dominios = new Dictionary<string, bool>();
                var sdominios = configuracionServidorDucDNS.dominios;
                if (!string.IsNullOrEmpty(sdominios))
                {
                    sdominios = Encriptacion.desencriptar(sdominios, __passPhrase);
                    foreach (var d in sdominios.Split(';').Where(x => !string.IsNullOrEmpty(x)))
                    {
                        string[] entradaDominio = d.Split('|');
                        string dominio = entradaDominio[0];
                        bool actualiza = Int32.Parse(entradaDominio[1]) > 0;

                        if (actualiza)
                        {
                            if (!dominios.ContainsKey(dominio))
                            {
                                dominios.Add(dominio, actualiza);

                                actualizarDominio(dominio, token, protocolo, verbose);
                            }
                        }
                        else { }
                    }
                }
                else
                    log.Error(Properties.Resources.sinDominiosQueActualizar);
            }
            catch (Exception ex)
            {
                string error = Utils.armarMensajeErrorExcepcion("actualizarDominios: ", ex);
                log.Error(error);

                if (!string.IsNullOrEmpty(error))
                    colaNotificaciones.encolar(new Notificacion()
                    {
                        tipoNotificacion = TipoNotificacion.OtrosErrores,
                        fecha = DateTime.Now,
                        asunto = Properties.Resources.errorIntentandoActualizarLosDominios,
                        detalles = error
                    });
            }
        }

        private void actualizarDominio(string dominio, string token, string protocolo, string verbose)
        {
            string html = string.Empty;
            string url =
           configuracionServidorDucDNS.simple ?
               string.Format(@"{0}://duckdns.org/update/{1}/{2}/{3}",
               protocolo, dominio, token, dominio) :
               string.Format(@"{0}://www.duckdns.org/update?domains={1}&token={2}&verbose={3}",
              protocolo, dominio, token, verbose);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                html = reader.ReadToEnd();

            //UPDATED [UPDATED or NOCHANGE]

            switch (configuracionServidorDucDNS.modoDebug)
            {
                case 0://sin logear nada
                    if (html.StartsWith("KO", StringComparison.OrdinalIgnoreCase))
                    {
                        colaNotificaciones.encolar(new Notificacion()
                        {
                            tipoNotificacion = TipoNotificacion.ErrorIntentoCambioIP,
                            fecha = DateTime.Now,
                            asunto = Properties.Resources.errorIntentoActualizarDominio,
                            detalles = html
                        });

                        if (configuracionServidorDucDNS.modoDebug == 2)
                            log.Debug("Se encolo la notific. de error en el intento de actualizacion de la direccion IP");

                    }
                    break;

                case 1://logear solo si hay cambios
                    if (html.StartsWith("OK", StringComparison.OrdinalIgnoreCase))
                    {
                        if (html.Contains("UPDATED"))
                        {
                            log.Info(html);

                            colaNotificaciones.encolar(new Notificacion()
                            {
                                tipoNotificacion = TipoNotificacion.ActualizacionCorrectaDireccionIP,
                                fecha = DateTime.Now,
                                asunto = Properties.Resources.cambioCorrectoDireccionIP,
                                detalles = html
                            });

                            if (configuracionServidorDucDNS.modoDebug == 2)
                                log.Debug("Se encolo la notific. de cambio en la direccion IP");
                        }
                    }
                    else
                        if (html.StartsWith("KO", StringComparison.OrdinalIgnoreCase))
                    {
                        log.Error(html);
                        colaNotificaciones.encolar(new Notificacion()
                        {
                            tipoNotificacion = TipoNotificacion.ErrorIntentoCambioIP,
                            fecha = DateTime.Now,
                            asunto = Properties.Resources.errorIntentoActualizarDominio,
                            detalles = html
                        });

                        if (configuracionServidorDucDNS.modoDebug == 2)
                            log.Debug("Se encolo la notific. de error en el intento de actualizacion de la direccion IP");
                    }
                    break;

                case 2://logear de forma exhaustiva
                    log.Info(html);
                    if (html.StartsWith("KO", StringComparison.OrdinalIgnoreCase))
                    {
                        colaNotificaciones.encolar(new Notificacion()
                        {
                            tipoNotificacion = TipoNotificacion.ErrorIntentoCambioIP,
                            fecha = DateTime.Now,
                            asunto = Properties.Resources.errorIntentoActualizarDominio,
                            detalles = html
                        });

                        if (configuracionServidorDucDNS.modoDebug == 2)
                            log.Debug("Se encolo la notific. de error en el intento de actualizacion de la direccion IP");
                    }
                    break;
            }
        }

        public string[] enviarEmail(string smtp, int puerto, string emailFrom, string usuario, string clave, bool ssl,
            string[] destinatarios, string asunto, string mensaje, bool html)
        {
            var _mensajes = new Mensajes(smtp, puerto, emailFrom, usuario, clave, ssl);
            string[] result = new string[destinatarios.Length];
            int i = 0;
            foreach (var destinatario in destinatarios)
                result[i] = _mensajes.enviarEmail(destinatario, asunto, mensaje, html);
            return result;
        }

        private void Cola_nuevaTarea(object sender, EventoNuevaTareaParamArgs e)
        {
            var notificacion = e.param as Notificacion;
            try
            {
                if (!configuracionServidorDucDNS.servidorCorreoConfigurado)
                {
                    e.tareaProcesada = false;
                    return;
                }

                string servidorSMTP = "", emailEnvio = "", usuarioSMTP = "", claveUsuarioSMTP = "", emailsNotificaciones = "";
                if (!string.IsNullOrEmpty(configuracionServidorDucDNS.servidorSMTP))
                    servidorSMTP = Encriptacion.desencriptar(configuracionServidorDucDNS.servidorSMTP, passPhrase);

                if (!string.IsNullOrEmpty(configuracionServidorDucDNS.emailEnvio))
                    emailEnvio = Encriptacion.desencriptar(configuracionServidorDucDNS.emailEnvio, passPhrase);

                if (!string.IsNullOrEmpty(configuracionServidorDucDNS.usuarioSMTP))
                    usuarioSMTP = Encriptacion.desencriptar(configuracionServidorDucDNS.usuarioSMTP, passPhrase);

                if (!string.IsNullOrEmpty(configuracionServidorDucDNS.claveUsuarioSMTP))
                    claveUsuarioSMTP = Encriptacion.desencriptar(configuracionServidorDucDNS.claveUsuarioSMTP, passPhrase);

                if (!string.IsNullOrEmpty(configuracionServidorDucDNS.emailsNotificaciones))
                    emailsNotificaciones = Encriptacion.desencriptar(configuracionServidorDucDNS.emailsNotificaciones, passPhrase);

                bool tareaProcesada = false;
                enviarEmailNotificacion(notificacion, servidorSMTP, configuracionServidorDucDNS.puertoSMTP,
                    configuracionServidorDucDNS.SMTPSSL, emailEnvio, usuarioSMTP,
             claveUsuarioSMTP, emailsNotificaciones, out tareaProcesada);
                e.tareaProcesada = tareaProcesada;
            }
            catch (Exception ex) { log.Error(Utils.armarMensajeErrorExcepcion("Cola_nuevaTarea: ", ex)); }
        }

        public void enviarEmailNotificacion(Notificacion notificacion, string servidorSMTP , int puertoSMTP, bool ssl, string emailEnvio , string usuarioSMTP,
            string claveUsuarioSMTP , string emailsNotificaciones, out bool tareaProcesada)
        {
            tareaProcesada = false;
            string archivo = Path.Combine(path, @"Plantillas\notificacion.htm");

            string error = "";
            string template_archivo = Utils.FileToString(archivo, out error);
            if (!string.IsNullOrEmpty(error))
            {
                error = string.Format(Properties.Resources.errorIntentandoObtenerContenidoArchivo, error);
                log.Error(error);
                return;
            }

            template_archivo = template_archivo.Replace("<#tdescripcionEquipo#>", Properties.Resources.tdescripcionEquipo);
            template_archivo = template_archivo.Replace("<#ttipoNotificacion#>", Properties.Resources.ttipoNotificacion);
            template_archivo = template_archivo.Replace("<#tfecha#>", Properties.Resources.tfecha);
            template_archivo = template_archivo.Replace("<#tasunto#>", Properties.Resources.tasunto);
            template_archivo = template_archivo.Replace("<#tdetalles#>", Properties.Resources.tdetalles);

            template_archivo = template_archivo.Replace("<#descripcionEquipo#>", configuracionServidorDucDNS.descripcionEquipo);
            template_archivo = template_archivo.Replace("<#tipoNotificacion#>", notificacion.tipoNotificacion.ToString());
            template_archivo = template_archivo.Replace("<#fecha#>", notificacion.fecha.ToString());
            template_archivo = template_archivo.Replace("<#asunto#>", notificacion.asunto);
            template_archivo = template_archivo.Replace("<#detalles#>", notificacion.detalles);

            var destinatarios = emailsNotificaciones.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] result = enviarEmail(servidorSMTP, puertoSMTP, emailEnvio,
                   usuarioSMTP, claveUsuarioSMTP, ssl, destinatarios, "DucDNS", template_archivo, true);

            if (result != null)
            {
                if (result.Where(x => !string.IsNullOrEmpty(x)).Count() > 0)
                {
                    log.Error(Properties.Resources.erroresEnviandoCorreo);

                    for (int i = 0; i < destinatarios.Length; i++)
                        if (!string.IsNullOrEmpty(result[i]))
                            log.Error(string.Format("{0}: {1}", destinatarios[i], result[i]));

                    if (Utils.conexionAInternet)
                    {
                        log.Error(Properties.Resources.noExisteConexionInternet);
                        //algo paso con el envio de correo que no se pudo enviar y probablemente no se pueda. Algo con las cuentas x ej
                        tareaProcesada =/*  e.tareaProcesada =*/ true;
                    }
                    else
                    {
                        //alguno de los items no pudo ser enviado. Si no tenemos conexion a internet lo dejamos en la cola para intentar mas tarde
                    }
                }
                else
                {
                    //todo se proceso correctamente. Lo margamos como procesado para quitarlo de la cola
                    tareaProcesada = /* e.tareaProcesada =*/ true;

                    if (configuracionServidorDucDNS.modoDebug == 2)
                        log.Debug(Properties.Resources.seEnviaronNotificacionesADirecciones + " " + String.Join(",", destinatarios.ToArray()));
                }
            }       
        }

        #region destruccion

        ~NucleoDucDNS()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try
            {
                if (configuracionServidorDucDNS.modoDebug == 2)
                    log.Debug("Dispose: 1");
                colaNotificaciones.Dispose();

                if (timerMinuto != null)
                    timerMinuto.Enabled = false;
                timerMinuto = null;
                if (configuracionServidorDucDNS.modoDebug == 2)
                    log.Debug("Dispose: 2");
            }
            catch (Exception ex)
            {
                log.Error(Utils.armarMensajeErrorExcepcion("Dispose: ", ex));
            }
            _disposed = true;
        }

        #endregion
    }
}