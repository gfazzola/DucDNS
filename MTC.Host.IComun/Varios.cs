using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System.Configuration;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace MTC.Host.IComun
{
    public enum TipoArchivoLog : byte { Actividad = 0, Errores = 1 }

    public static class Utils
    {
        public static int mascaraCheckList(this CheckedListBox lista, int desplazamiento = 0)
        {
            IEnumerator myEnumerator;
            myEnumerator = lista.CheckedIndices.GetEnumerator();
            int y = 0, mascara = 0;
            while (myEnumerator.MoveNext() != false)
            {
                y = (int)myEnumerator.Current;
                mascara += (int)Math.Pow(2, y + desplazamiento);
            }
            return mascara;
        }

        #region archivos
        public static string FileToString(string filePath, out string error)
        {
            return _FileToString(filePath, out error);
        }

        private static string _FileToString(string filePath, out string error)
        {
            string result = "";
            error = "";
            try
            {
                StreamReader loStream = new StreamReader(filePath, Encoding.UTF8);
                result = loStream.ReadToEnd();
                loStream.Close();
            }
            catch (Exception ex)
            {
                error = armarMensajeErrorExcepcion("_FileToString", ex);
            }
            return result;
        }
        #endregion

        #region conexion a internet

        public static bool conexionAInternet
        {
            get
            {
                bool result = false;
                try
                {
                    // result = new Ping().Send("www.google.com").Status == IPStatus.Success;
                    //1.0.0.2 se cambio por el mecanismo anterior de ping
                    using (var client = new WebClient())
                    using (var stream = client.OpenRead("http://www.google.com"))
                        result = true;
                }
                catch { }
                return result;
            }
        }

        #endregion
       
        #region ip publica
        public static string ipPublica(out string error)
        {
            return _ipPublica(out error);
        }

        private static string _ipPublica(out string error)
        {
            error = "";

            string result = "?";

            try
            {
                using (WebClient wc = new WebClient())
                {
                    result = wc.DownloadString("http://icanhazip.com/");
                    result = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(result)[0].ToString();
                }
            }
            catch (Exception ex)
            {
                error = Utils.armarMensajeErrorExcepcion("ipPublica icanhazip: ", ex);
            }

            if (result == "?")
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        result = wc.DownloadString("http://checkip.dyndns.org/");
                        result = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(result)[0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = Utils.armarMensajeErrorExcepcion("ipPublica dyndns: ", ex);
                }
            return result;
        }
        #endregion

        #region nlog
        /// <summary>
        /// Devuelve el nombre del log actual para el target dado
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string getLogFileName(string targetName, out string error)
        {
            string fileName = null;
            error = "";
            if (LogManager.Configuration != null && LogManager.Configuration.ConfiguredNamedTargets.Count != 0)
            {
                #region las cosas pintan bien...
                Target target = LogManager.Configuration.FindTargetByName(targetName);
                if (target == null)
                {
                    error = string.Format(Properties.Resources.logMaganerNoSeEncontroTarget, targetName);
                    goto salida;
                }

                FileTarget fileTarget = null;
                WrapperTargetBase wrapperTarget = target as WrapperTargetBase;

                // Unwrap the target if necessary.
                if (wrapperTarget == null)
                {
                    fileTarget = target as FileTarget;
                }
                else
                {
                    fileTarget = wrapperTarget.WrappedTarget as FileTarget;
                }

                if (fileTarget == null)
                {
                    error = string.Format( Properties.Resources.logManagerNoSeEncontroFileTarget, target.GetType());
                    goto salida;
                }

                var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
                fileName = fileTarget.FileName.Render(logEventInfo);
                #endregion
            }
            else
            {
                error = Properties.Resources.logManagerNoContieneTarget;
                goto salida;
            }       
        salida:
            return fileName;
        }
        #endregion

        #region excepciones
        public static string armarMensajeErrorExcepcion(Exception ex)
        {
            return armarMensajeErrorExcepcion("", ex);
        }

        public static string armarMensajeErrorExcepcion(string prefijo, Exception ex)
        {
            return armarMensajeErrorExcepcion(prefijo, ex, "Informacion adicional: ");
        }

        public static string armarMensajeErrorExcepcion(string prefijo, Exception ex, string infoAdicional)
        {
            return prefijo + ex.Message + (ex.InnerException == null ? "" : (Environment.NewLine + infoAdicional + ex.InnerException.Message));
        }
        #endregion

        #region procesos        

        public static bool ejecutarProceso(string archivo, string argumentos, string directorioTrabajo, bool esperarProceso, bool muestraError, out string error)
        {
            return _ejecutarProceso(archivo, argumentos, directorioTrabajo, esperarProceso, muestraError, out error);
        }

        private static bool _ejecutarProceso(string archivo, string argumentos, string directorioTrabajo, bool esperarProceso, bool muestraError, out string error)
        {
            bool result = false;
            error = "";
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = archivo;
                if (!File.Exists(psi.FileName))
                {
                    error = string.Format(Properties.Resources.archivoNoExiste, psi.FileName);
                    if (muestraError)
                        MessageBox.Show(error, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto salida;
                }
                
                if (!string.IsNullOrEmpty(argumentos))
                    psi.Arguments = argumentos;
                psi.UseShellExecute = true;
                if (string.IsNullOrEmpty(directorioTrabajo))
                    psi.WorkingDirectory = Application.StartupPath;
                else
                    psi.WorkingDirectory = directorioTrabajo;
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
                if (esperarProceso)
                    p.WaitForExit();

                result = true;
            }
            catch (Exception ex)
            {
                error = armarMensajeErrorExcepcion(ex);
                if (muestraError)
                    MessageBox.Show(error, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        salida:
            return result;
        }

        public static void ejecutarProceso(string archivo, bool esperarProceso)
        {
            _ejecutarProceso(archivo, esperarProceso);
        }

        private static void _ejecutarProceso(string archivo, bool esperarProceso)
        {
            try
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(archivo);
                if (esperarProceso)
                    p.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ejecutarProceso2(bool verificaExistencia, string archivo, string argumentos,
            string directorioTrabajo, bool esperarProceso, bool muestraMensajeError, out string error)
        {
            _ejecutarProceso2(verificaExistencia, archivo, argumentos,
             directorioTrabajo, esperarProceso, muestraMensajeError, out error);
        }

        private static void _ejecutarProceso2(bool verificaExistencia, string archivo, string argumentos,
            string directorioTrabajo, bool esperarProceso, bool muestraMensajeError, out string error)
        {
            error = "";
            if (verificaExistencia && !File.Exists(archivo))
            {
                error = string.Format(Properties.Resources.archivoNoExiste, archivo);
                if (muestraMensajeError)
                    MessageBox.Show(error, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = archivo;
                p.StartInfo.Arguments = argumentos;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WorkingDirectory = directorioTrabajo;
                p.Start();
                if (esperarProceso)
                    p.WaitForExit();
            }
            catch (Exception ex)
            {
                error = armarMensajeErrorExcepcion(ex);
                if (muestraMensajeError)
                    MessageBox.Show(error, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public static void setAppAlIniciarWindows(string nombre, string path, bool instala)
        {
            _setAppAlIniciarWindows(nombre, path, instala);
        }

        private static void _setAppAlIniciarWindows(string nombre, string path, bool instala)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (instala)
                rk.SetValue(nombre, path);
            else
                rk.DeleteValue(nombre, false);
        }
        #endregion
    }

    public class NotifyIconFix
    {
        public static void SetNotifyIconText(NotifyIcon ni, string text)
        {
            if (text.Length >= 128)
                throw new ArgumentOutOfRangeException("El limite del texto es de 127 caracteres");
            Type t = typeof(NotifyIcon);
            BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden).SetValue(ni, text);
            if ((bool)t.GetField("added", hidden).GetValue(ni))
                t.GetMethod("UpdateIcon", hidden).Invoke(ni, new object[] { true });
        }
    }

    public class ConfiguracionServicio : ConfigurationSection
    {
        [ConfigurationProperty("nombre")]
        public string nombre
        {
            get { return (string)base["nombre"]; }
            set { base["nombre"] = value; }
        }

        [ConfigurationProperty("descripcion")]
        public string descripcion
        {
            get { return (string)base["descripcion"]; }
            set { base["descripcion"] = value; }
        }

        [ConfigurationProperty("serviciosDependientes")]
        public string serviciosDependientes
        {
            get { return (string)base["serviciosDependientes"]; }
            set { base["serviciosDependientes"] = value; }
        }

        [ConfigurationProperty("segundosEsperaInicio")]
        public int segundosEsperaInicio
        {
            get { return (int)base["segundosEsperaInicio"]; }
            set { base["segundosEsperaInicio"] = value; }
        }

        [ConfigurationProperty("cuentaServicio")]
        public ServiceAccount cuentaServicio
        {
            get { return (ServiceAccount)base["cuentaServicio"]; }
            set { base["cuentaServicio"] = value; }
        }
    }

    public class ConfiguracionNucleo : ConfigurationSection
    {
        [ConfigurationProperty("tipoProveedor")]
        public string tipoProveedor
        {
            get { return (string)base["tipoProveedor"]; }
            set { base["tipoProveedor"] = value; }
        }

       
    }

    public enum TipoLog : byte { ERROR = 0, INFO = 1, ATENCION = 2/*, VACIO=3*/ }
    public delegate void delegate_logear2(TipoLog tipo, string msg);
    public delegate void delegate_ponerEstadoServicio();
}
