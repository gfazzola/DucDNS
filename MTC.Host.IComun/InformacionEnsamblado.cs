using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace MTC.Host.IComun
{
    public sealed class InformacionEnsamblado
    {
        private static volatile InformacionEnsamblado instance = null;
        private static readonly object padlock = new object();
        private static Dictionary<string, string> _informacion = null;
        private static delegate_logear2 _logear;
        public static InformacionEnsamblado Instance(Assembly ensamblado, delegate_logear2 logear)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InformacionEnsamblado();
                        _logear = logear;
                        _informacion = infoEnsamblado(ensamblado);
                    }
                }
            }
            return instance;
        }

        public static InformacionEnsamblado Instance()
        {
            if (instance == null)
                throw new Exception("InformacionEnsamblado:" + Properties.Resources.informacionEnsambladoSecuenciaLlamadoInvalida);
            return instance;
        }

        public Dictionary<string, string> informacion { get { return _informacion; } }

        public static Dictionary<string, string> infoEnsamblado(Assembly ensamblado)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string donde = ensamblado.Location;
            result.Add("Path", System.IO.Path.GetDirectoryName(donde));
            FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(donde);
            result.Add("ProductVersion", fvi.ProductVersion);
            result.Add("CompanyName", fvi.CompanyName);
            result.Add("ProductName", fvi.ProductName);
            result.Add("FileDescription", fvi.FileDescription);
            result.Add("LegalCopyright", fvi.LegalCopyright);
            // result.Add("Certificado", Certificados.generarCertificado3(true, System.Environment.MachineName, _logear));
            result.Add("HostName", Environment.MachineName);

            object[] attributes = ensamblado.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length == 1)
                result.Add("TituloAplicacion", ((AssemblyTitleAttribute)attributes[0]).Title);
            return result;
        }

        public string ProductVersion { get { return _informacion["ProductVersion"]; } }
        public string CompanyName { get { return _informacion["CompanyName"]; } }
        public string ProductName { get { return _informacion["ProductName"]; } }
        public string FileDescription { get { return _informacion["FileDescription"]; } }
        public string LegalCopyright { get { return _informacion["LegalCopyright"]; } }
        public string Certificado
        {
            get { return _informacion["Certificado"]; }
            set { _informacion["Certificado"] = value; }
        }
        public string HostName { get { return _informacion["HostName"]; } }
        public string TituloAplicacion { get { return _informacion.ContainsKey("TituloAplicacion") ? _informacion["TituloAplicacion"] : ""; } }

    }
}