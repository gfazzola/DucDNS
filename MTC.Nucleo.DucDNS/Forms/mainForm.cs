using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using MTC.Host.IComun;
using System.Windows.Forms;

namespace MTC.Nucleo.DucDNS
{
    public partial class mainForm : formBase
    {
        private readonly Subscriber<int> suscriberConectadoAInternet;
        private readonly Subscriber<string> suscriberIpPublica, suscriberTickMinuto;

        Dictionary<string, bool> dominios = new Dictionary<string, bool>();
        NucleoDucDNS nucleo;
        public mainForm()
        {
            InitializeComponent();
        }

        public mainForm(NucleoDucDNS nucleo)
        {
            InitializeComponent();
            #region internacionalizacion
            labelToken.Text = Properties.Resources.labelToken;
            labelIntervaloActualizacionMinutos.Text = Properties.Resources.labelIntervaloActualizacionMinutos;
            labelModoDebug.Text = Properties.Resources.labelModoDebug;
            checkSimple.Text = Properties.Resources.checkSimple;
            checkVerbose.Text = Properties.Resources.checkVerbose;
            checkHttps.Text = Properties.Resources.checkHttps;

            tabDominios.Text = Properties.Resources.tabDominios;
            tabPrincipal.Text = Properties.Resources.tabPrincipal;
            tabNotificaciones.Text = Properties.Resources.tabNotificaciones;
            tabSeguridad.Text = Properties.Resources.tabSeguridad;

            lDominios.Columns[0].Text = Properties.Resources.labelColumnaNombre;

            checkRequierePassword.Text = Properties.Resources.checkRequierePassword;

            grupoSMTP.Text = Properties.Resources.grupoSMTP;

            labelServidorSMTP.Text = Properties.Resources.labelServidorSMTP;
            labelUsuarioSMTP.Text = Properties.Resources.labelUsuarioSMTP;
            labelClaveSMTP.Text = Properties.Resources.labelClaveSMTP;
            labelPuertoSMTP.Text = Properties.Resources.labelPuertoSMTP;
            checkSSL.Text = Properties.Resources.checkSSL;

            bProbarEmail.Text = Properties.Resources.bProbarEmail;

            grupoNotificaciones.Text = Properties.Resources.grupoNotificaciones;
            labelEmailsANotificar.Text = Properties.Resources.labelEmailsANotificar;

            labelNotificar.Text = Properties.Resources.labelNotificar;

            itemSistema.Text = Properties.Resources.itemSistema;
            itemGuardar.Text = Properties.Resources.itemGuardar;
            itemGuardarYSalir.Text = Properties.Resources.itemGuardarYSalir;

            itemSalir.Text = Properties.Resources.itemSalir;

            itemAyuda.Text = Properties.Resources.itemAyuda;
            itemAcercaDe.Text = Properties.Resources.itemAcercaDe;

            itemAgregar.Text = Properties.Resources.agregar;
            itemEditar.Text = Properties.Resources.editar;
            itemEliminar.Text = Properties.Resources.eliminar;

            bOpciones.Text = Properties.Resources.opciones;
            itemIniciarDetenerActualizacion.Text = Properties.Resources.detenerActualizacion;
            itemActualizarAhora.Text = Properties.Resources.actualizarAhora;

            comboLogs.Items.Clear();
            comboLogs.Items.Add(Properties.Resources.informacion);
            comboLogs.Items.Add(Properties.Resources.errores);
            comboLogs.Items.Add(Properties.Resources.depuracion);

            itemActualizarLogs.Text = Properties.Resources.actualizar;

            labelDescripcionEquipo.Text = Properties.Resources.labelDescripcionEquipo;

            #endregion

            labelEstadoConexion.Image = imageList1.Images[nucleo.conectadoAInternet];
            labelEstadoConexion.Text = nucleo.ipPublica;
            labelStatus.Text = string.Format("{0} {1}. {2}", Properties.Resources.procesoActualizacion, nucleo.iniciado ? Properties.Resources.iniciado :
                Properties.Resources.servicioDetenido, DateTime.Now.ToString());

            this.nucleo = nucleo;
            suscriberConectadoAInternet = new Subscriber<int>(nucleo.publicadorConectadoAInternet);
            suscriberConectadoAInternet.Publisher.DataPublisher += Publisher_DataPublisher_CambioConectadoAInternet;
            suscriberIpPublica = new Subscriber<string>(nucleo.publicadorIpPublica);
            suscriberIpPublica.Publisher.DataPublisher += Publisher_DataPublisher_CambioIpPublica;

            suscriberTickMinuto = new Subscriber<string>(nucleo.publicadorTickMinuto);
            suscriberTickMinuto.Publisher.DataPublisher += Publisher_DataPublisher;
            
            comboLogs.SelectedIndex = 1;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Text += " V " + productVersion;
            try
            {
                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.token))
                    editToken.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.token, nucleo.passPhrase);

                checkSimple.Checked = nucleo.configuracionServidorDucDNS.simple;
                checkVerbose.Checked = nucleo.configuracionServidorDucDNS.verbose;
                checkHttps.Checked = nucleo.configuracionServidorDucDNS.https;

                editDescripcionEquipo.Text = nucleo.configuracionServidorDucDNS.descripcionEquipo;
                if (string.IsNullOrEmpty(editDescripcionEquipo.Text))
                    editDescripcionEquipo.Text = Environment.MachineName;

                string sMinutos = nucleo.configuracionServidorDucDNS.minutosActualizacion.ToString();
                for (int i = 0; i < comboMinutosActualizacion.Items.Count; i++)
                    if ((string)comboMinutosActualizacion.Items[i] == sMinutos)
                    {
                        comboMinutosActualizacion.SelectedIndex = i;
                        break;
                    }

                if (comboMinutosActualizacion.SelectedIndex == -1)
                    comboMinutosActualizacion.SelectedIndex = 0;

                var sdominios = nucleo.configuracionServidorDucDNS.dominios;
                if (!string.IsNullOrEmpty(sdominios))
                {
                    sdominios = Encriptacion.desencriptar(sdominios, nucleo.passPhrase);
                    foreach (var d in sdominios.Split(';').Where(x => !string.IsNullOrEmpty(x)))
                    {
                        string[] entradaDominio = d.Split('|');
                        string dominio = entradaDominio[0];
                        bool actualiza = Int32.Parse(entradaDominio[1]) > 0;

                        if (!dominios.ContainsKey(dominio))
                            dominios.Add(dominio, actualiza);
                    }
                }

                foreach (var d in dominios.Keys)
                {
                    var item = lDominios.Items.Add(d);
                    item.Checked = dominios[d];
                }

                if (nucleo.configuracionServidorDucDNS.modoDebug < 0 || nucleo.configuracionServidorDucDNS.modoDebug > 2)
                    editModoDebug.Value = 0;

                checkRequierePassword.Checked = nucleo.configuracionServidorDucDNS.adminRequierePassword;
                editClave.Text = nucleo.configuracionServidorDucDNS.passwordAdmin;

                editModoDebug.Value = (decimal)nucleo.configuracionServidorDucDNS.modoDebug;

                #region email

                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.servidorSMTP))
                    editServidorSMTP.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.servidorSMTP, nucleo.passPhrase);

                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.emailEnvio))
                    editEmail.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.emailEnvio, nucleo.passPhrase);

                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.usuarioSMTP))
                    editUsuarioSMTP.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.usuarioSMTP, nucleo.passPhrase);

                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.claveUsuarioSMTP))
                    editClaveSMTP.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.claveUsuarioSMTP, nucleo.passPhrase);

                if (!string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.emailsNotificaciones))
                    editEmailDestinatarios.Text = Encriptacion.desencriptar(nucleo.configuracionServidorDucDNS.emailsNotificaciones, nucleo.passPhrase);
                editPuertoSMTP.Value = (decimal)nucleo.configuracionServidorDucDNS.puertoSMTP;
                checkSSL.Checked = nucleo.configuracionServidorDucDNS.SMTPSSL;
                #endregion

                #region que notifica
                checkLNotificaciones.Items.Clear();
                checkLNotificaciones.Items.Add(Properties.Resources.notificaOtrosErrores, nucleo.configuracionServidorDucDNS.notificaOtrosErrores);
                checkLNotificaciones.Items.Add(Properties.Resources.notificaCambioIp, nucleo.configuracionServidorDucDNS.notificaCambioIp);
                checkLNotificaciones.Items.Add(Properties.Resources.notificaErrorCambioIp, nucleo.configuracionServidorDucDNS.notificaErrorCambioIp);
                checkLNotificaciones.Items.Add(Properties.Resources.notificaPerdidaConexionInternet, nucleo.configuracionServidorDucDNS.notificaPerdidaConexionInternet);
                checkLNotificaciones.Items.Add(Properties.Resources.notificaReestablecimientoConexionInternet, nucleo.configuracionServidorDucDNS.notificaReestablecimientoConexionInternet);
                checkLNotificaciones.Items.Add(Properties.Resources.notificaActualizacionCorrectaIP, nucleo.configuracionServidorDucDNS.notificaActualizacionCorrectaIP);

                #endregion

                verSiHabilitoEnvioCorreo(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {

        }

        private void agregarEditarDominio(string dominio = null)
        {
            var form = new formAdminDominio(dominio, agregarDominio);
            form.ShowDialog();
        }

        private object agregarDominio(object sender)
        {
            var dominio = sender as string;
            if (dominios.ContainsKey(dominio))
            {
                MessageBox.Show(string.Format(Properties.Resources.dominioYaExiste, dominio), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            dominios.Add(dominio, true);

            var item = lDominios.Items.Add(dominio);
            item.Checked = true;

            return true;
        }

        private void lDominios_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void itemAgregar_Click(object sender, EventArgs e)
        {
            agregarEditarDominio();
        }

        private void itemEditar_Click(object sender, EventArgs e)
        {
            if (lDominios.SelectedItems.Count == 0)
                return;
            agregarEditarDominio(lDominios.SelectedItems[0].Text);
        }

        private void itemEliminar_Click(object sender, EventArgs e)
        {
            if (lDominios.SelectedItems.Count == 0)
                return;
            var dominio = lDominios.SelectedItems[0].Text;

            dominios.Remove(dominio);
            lDominios.Items.Remove(lDominios.SelectedItems[0]);

        }

        private void itemGuardar_Click(object sender, EventArgs e)
        {
            if (guardarConfiguracion())
            {
                MessageBox.Show(Properties.Resources.configGuardadaConExito, Properties.Resources.procesoCompletadoConExito, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Properties.Resources.configGuardadaConErrores, Properties.Resources.procesoCompletadoConErrores, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itemGuardarYSalir_Click(object sender, EventArgs e)
        {
            if (guardarConfiguracion())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private string encriptar(string cadena)
        {
            return cadena.Length > 0 ? Encriptacion.encriptar(cadena, nucleo.passPhrase) : "";
        }

        private bool guardarConfiguracion()
        {
            bool result = false;
            try
            {
                string sdominios = "";
                foreach (ListViewItem item in lDominios.Items)
                    sdominios += item.Text + "|" + (item.Checked ? "1" : "0") + ";";

                nucleo.configuracionServidorDucDNS.dominios = sdominios.Length > 0 ? encriptar(sdominios.Substring(0, sdominios.Length - 1)) : "";

                nucleo.configuracionServidorDucDNS.token = encriptar(editToken.Text.Trim());
                nucleo.configuracionServidorDucDNS.verbose = checkVerbose.Checked;
                nucleo.configuracionServidorDucDNS.https = checkHttps.Checked;
                nucleo.configuracionServidorDucDNS.simple = checkSimple.Checked;
                nucleo.configuracionServidorDucDNS.minutosActualizacion = Int32.Parse(comboMinutosActualizacion.Text);
                nucleo.configuracionServidorDucDNS.modoDebug = (int)editModoDebug.Value;
                nucleo.configuracionServidorDucDNS.passwordAdmin = editClave.Text.Length > 0 ? Encriptacion.getMd5Hash(editClave.Text) : "";
                nucleo.configuracionServidorDucDNS.adminRequierePassword = checkRequierePassword.Checked;

                nucleo.configuracionServidorDucDNS.puertoSMTP = (int)editPuertoSMTP.Value;
                nucleo.configuracionServidorDucDNS.servidorSMTP = encriptar(editServidorSMTP.Text.Trim());
                nucleo.configuracionServidorDucDNS.emailEnvio = encriptar(editEmail.Text.Trim());
                nucleo.configuracionServidorDucDNS.usuarioSMTP = encriptar(editUsuarioSMTP.Text.Trim());
                nucleo.configuracionServidorDucDNS.claveUsuarioSMTP = encriptar(editClaveSMTP.Text.Trim());
                nucleo.configuracionServidorDucDNS.emailsNotificaciones = encriptar(editEmailDestinatarios.Text.Trim());
                nucleo.configuracionServidorDucDNS.mascaraNotificaciones = checkLNotificaciones.mascaraCheckList();

                nucleo.configuracionServidorDucDNS.descripcionEquipo = editDescripcionEquipo.Text.Trim();
                if (string.IsNullOrEmpty(nucleo.configuracionServidorDucDNS.descripcionEquipo))
                    nucleo.configuracionServidorDucDNS.descripcionEquipo = Environment.MachineName;

                nucleo.guardarConfiguracion();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void checkRequierePassword_CheckedChanged(object sender, EventArgs e)
        {
            editClave.Enabled = checkRequierePassword.Checked;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Publisher_DataPublisher(object sender, MessageArgument<string> e)
        {
            labelStatus.Text = string.Format("{0} {1}. {2}", Properties.Resources.procesoActualizacion, nucleo.iniciado ? Properties.Resources.iniciado :
               Properties.Resources.servicioDetenido, DateTime.Now.ToString());
        }

        private void Publisher_DataPublisher_CambioIpPublica(object sender, MessageArgument<string> e)
        {
            labelEstadoConexion.Text = e.Message;
        }

        private void bProbarEmail_Click(object sender, EventArgs e)
        {
            var notificacion = new Notificacion()
            {
                tipoNotificacion = TipoNotificacion.Varios,
                fecha = DateTime.Now,
                asunto = Properties.Resources.correoDePrueba,
                detalles = Properties.Resources.correoDePrueba
            };

            bool tareaProcesada = false;
            nucleo.enviarEmailNotificacion(notificacion, editServidorSMTP.Text.Trim(), (int)editPuertoSMTP.Value, checkSSL.Checked,
                editEmail.Text.Trim(), editUsuarioSMTP.Text.Trim(), editClaveSMTP.Text.Trim(), editEmailDestinatarios.Text.Trim(), out tareaProcesada);


            MessageBox.Show(Properties.Resources.correoDePruebaEnviado, Properties.Resources.procesoCompletadoConExito,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void verSiHabilitoEnvioCorreo(object sender, EventArgs e)
        {
            bProbarEmail.Enabled = editEmail.Text.Trim().Length > 0 && editEmailDestinatarios.Text.Trim().Length > 0 &&
            editClaveSMTP.Text.Trim().Length > 0 && editUsuarioSMTP.Text.Trim().Length > 0 && editPuertoSMTP.Value > 0;
        }

        private void itemAcercaDe_Click(object sender, EventArgs e)
        {
            new formAcercaDe().ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void bOpciones_Click(object sender, EventArgs e)
        {
            menuOpciones.Show(bOpciones, new Point(0, bOpciones.Height));
        }

        private void itemPausarActualizacion_Click(object sender, EventArgs e)
        {
            if (itemIniciarDetenerActualizacion.Text == Properties.Resources.detenerActualizacion)
            {
                nucleo.actualizacionDetenida = true;
                itemIniciarDetenerActualizacion.Text = Properties.Resources.iniciarActualizacion;
            }
            else
            {
                nucleo.actualizacionDetenida = false;
                itemIniciarDetenerActualizacion.Text = Properties.Resources.detenerActualizacion;
            }
        }

        private void itemActualizarAhora_Click(object sender, EventArgs e)
        {
            nucleo.actualizarDominios();
        }

        private void comboLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizarLogs();
        }

        private void actualizarLogs()
        {
            string target = "infoLog";
            switch (comboLogs.SelectedIndex)
            {
                case 1: target = "errorLog"; break;
                case 2: target = "debugLog"; break;
            }

            string error = "";
            string nArchivo = Utils.getLogFileName(target, out error);
            if (!string.IsNullOrEmpty(error))
                richTextBox1.Text = error;
            else
                if (File.Exists(nArchivo))
                try
                {
                    richTextBox1.LoadFile(nArchivo, RichTextBoxStreamType.PlainText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
                richTextBox1.Text = Properties.Resources.archivoAuditoriaNoExiste;
        }

        private void itemActualizarLogs_Click(object sender, EventArgs e)
        {
            actualizarLogs();
        }

        private void Publisher_DataPublisher_CambioConectadoAInternet(object sender, MessageArgument<int> e)
        {
            labelEstadoConexion.Image = imageList1.Images[e.Message];
        }

    }
}