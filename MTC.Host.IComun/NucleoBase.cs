using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using NLog;
using System.Threading;
using System.Globalization;
using System.Drawing;
namespace MTC.Host.IComun
{
    public class NucleoBase
    {
        public string _cultura = "";
        protected bool _iniciado = false;
        public Configuration config;
        protected static string _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        protected static string _nombre;
        protected static Dictionary<string, object> _parametros;
        
        public NucleoBase(Dictionary<string, object> parametros)
        {
            _parametros = parametros;
            if (!_path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                _path += Path.DirectorySeparatorChar;

            log = LogManager.GetLogger(GetType().FullName);

            _cultura = (string)parametros["cultura"];

            if (parametros.ContainsKey("icono"))
                icono = parametros["icono"] as Icon;

            Thread.CurrentThread.CurrentUICulture = string.IsNullOrEmpty(cultura) ? Thread.CurrentThread.CurrentCulture : new CultureInfo(cultura);
        }

        protected void inicializar(string archivoConfig)
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(_path, archivoConfig);
            if (!File.Exists(fileMap.ExeConfigFilename))
                throw new Exception(string.Format(Properties.Resources.archivoConfiguracionNoExiste, fileMap.ExeConfigFilename));
            config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        #region propiedades
        public string cultura { get { return _cultura; } }

        public string path { get { return _path; } }

        public string nombre { get { return _nombre; } }

        public Logger log { get; private set; }

        public string productVersion
        {
            get
            {
                return InformacionEnsamblado.Instance().ProductVersion;
            }
        }

        public string host { get { return InformacionEnsamblado.Instance().HostName; } }

        public string productVersionHostContenedor
        {
            get
            {
                return _parametros["versionHostContenedor"] as string;
            }
        }

        public Dictionary<string, string> infoEnsamblado { get { return InformacionEnsamblado.Instance().informacion; } }

        public ConfiguracionServicio configServicioHost
        {
            get
            {
                return _parametros["configServicioHost"] as ConfiguracionServicio;
            }
        }

        public bool iniciado { get { return _iniciado; } }

        public Icon icono { get; set; }
        #endregion

        public void buscarInfoEnsamblado(System.Reflection.Assembly ensamblado)
        {
            InformacionEnsamblado.Instance(ensamblado, null);
        }

        public void logear2(TipoLog tipo, string msg)
        {
            switch (tipo)
            {
                case TipoLog.INFO:
                    log.Info(msg);
                    break;
                case TipoLog.ERROR:
                    log.Error(msg);
                    break;
                case TipoLog.ATENCION:
                    log.Warn(msg);
                    break;
            }
        }
    }
}