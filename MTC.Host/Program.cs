using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.ServiceProcess;
using System.Windows.Forms;
using MTC.Host.IComun;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Configuration.Install;
using System.Diagnostics;
using System.Drawing;
namespace MTC.Host
{ 
    public class MTCHost : System.ServiceProcess.ServiceBase
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(uint dwProcessId);

        private enum QueHaceConLosArgumentos { Nada = 0, Instala = 1, Desinstala = 2, EjecutaComoConsola = 3, EjecutaComoVentana = 4 }

        static Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
        static string appHost = ensamblado.Location, cultura="";
        static string path = System.IO.Path.GetDirectoryName(appHost);
        static FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(appHost);
        INucleo _nucleo = null;
       static Icon icono = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public MTCHost(bool notificaCambioDeSesion)
        {
            InitializeComponent();
            CanHandleSessionChangeEvent = notificaCambioDeSesion;
            _nucleo = crearInstancia(true);
        }

        public MTCHost()
            : this(true)
        {
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            if (_nucleo != null)
                _nucleo.cambioDeSesion(changeDescription);
        }

        private static QueHaceConLosArgumentos analizarArgumentos(string[] args)
        {
            QueHaceConLosArgumentos result = QueHaceConLosArgumentos.Nada;

            if (args == null || args.Length == 0)
                goto salida;

            foreach (string arg in args)
            {
                if (arg.Length > 1 && (arg[0] == '-' || arg[0] == '/'))

                    switch (arg.Substring(1).ToLower())
                    {
                        default:
                            break;
                        case "install":
                        case "i":
                            result = QueHaceConLosArgumentos.Instala;
                            break;
                        case "uninstall":
                        case "u":
                            result = QueHaceConLosArgumentos.Desinstala;
                            break;
                        case "consola":
                        case "c":
                            result = QueHaceConLosArgumentos.EjecutaComoConsola;
                            break;
                        case "windows":
                        case "w":
                            result = QueHaceConLosArgumentos.EjecutaComoVentana;
                            break;
                    }
                if (result != QueHaceConLosArgumentos.Nada)
                    break;
            }

        salida:
            return result;
        }

        static void Main(string[] args)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar;

            cultura = System.Configuration.ConfigurationManager.AppSettings.Get("cultura");
            Thread.CurrentThread.CurrentUICulture = string.IsNullOrEmpty(cultura) ? Thread.CurrentThread.CurrentCulture : new CultureInfo(cultura);

            var queHace = QueHaceConLosArgumentos.Nada;
            try
            {
                queHace = analizarArgumentos(args);

                if (queHace != QueHaceConLosArgumentos.Nada)
                    procesarArgumentoSobreServicio(queHace, args);
                else
                    ejecutarComoServicio();
            }
            catch (Exception ex)
            {
                string msgError = Utils.armarMensajeErrorExcepcion(ex);
                if (queHace == QueHaceConLosArgumentos.EjecutaComoVentana)
                    MessageBox.Show(msgError, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string sSource;
                    string sLog;

                    sSource = "MTC.Host";
                    sLog = "Application";

                    if (!EventLog.SourceExists(sSource))
                        EventLog.CreateEventSource(sSource, sLog);

                    //EventLog.WriteEntry(sSource, sEvent);
                    EventLog.WriteEntry(sSource, msgError, EventLogEntryType.Error, 234);
                }
            }
        }

        private static void procesarArgumentoSobreServicio(QueHaceConLosArgumentos queHace, string[] args)
        {
            switch (queHace)
            {
                case QueHaceConLosArgumentos.Instala:
                    try
                    {
                        /*                         
                         * Si lo invoco unicamente con /i, args tiene longitud = 1 y en la posicion cero viene /i
                         * Si lo invoco con /i /cuenta=guillermo /clave=11111 ejemplo, viene con 3 parametros y en la pos cero sigue estando el /i
                         * Entonces lo que vamos a pasar como parametro al installer es args reemplazando el valor de la posicion cero por 
                         * Assembly.GetExecutingAssembly().Location
                         */

                        args[0] = Assembly.GetExecutingAssembly().Location;
                        ManagedInstallerClass.InstallHelper(args);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Utils.armarMensajeErrorExcepcion(ex), Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case QueHaceConLosArgumentos.Desinstala:

                    string binpath = Assembly.GetExecutingAssembly().Location;
                    var toBeRemoved = ServiceController.GetServices().Where(s => GetImagePath(s.ServiceName) == binpath).Select(x => x.ServiceName);
                    var installer = new ProjectInstaller();
                    installer.Context = new InstallContext();
                    foreach (var sname in toBeRemoved)
                        try
                        {
                            installer.Uninstall(sname);
                        }
                        catch { }
                    break;

                case QueHaceConLosArgumentos.EjecutaComoConsola:
                    ejecutarComoConsola();
                    break;

                case QueHaceConLosArgumentos.EjecutaComoVentana:
                    ejecutarComoVentana();
                    break;

            }
        }
      
        private static void ejecutarComoServicio()
        {
            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            var ServicesToRun = new System.ServiceProcess.ServiceBase[] { new MTCHost() };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        private static void ejecutarComoVentana()
        {
            string nombreApp = configServicio.nombre + "-App";
            if (SingleInstanceClass.CheckForOtherApp(nombreApp))
            {
                MessageBox.Show(Properties.Resources.aplicacionEnEjecucion + nombreApp, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            INucleo _instance = crearInstancia(false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Dictionary<string, object> parametros = new Dictionary<string, object>(){
            {"nucleo", _instance},
            {"nombreApp", nombreApp},
                //{"icono", icono},
                {"nombreServicio", configServicio.nombre},
                {"appHost", appHost}};

            Application.Run(_instance.contextoAplicacion(new object[1] { parametros }));
        }

        /// <summary>
        /// como la app es winform la consola como tal (readline y writeline ) no andan. este patch hace que si ande
        /// </summary>
        private static void ejecutarComoConsola()
        {
            //http://stackoverflow.com/questions/29947305/how-to-be-dynamically-either-console-application-or-windows-application
            bool madeConsole = false;

            if (!AttachToConsole())
            {
                AllocConsole();
                madeConsole = true;
            }

            try
            {
                INucleo _instance = crearInstancia(true);
                string error = "";
                if (_instance.iniciar(out error))
                {
                    Console.WriteLine(Properties.Resources.presioneTeclaParaDetenerServicio);
                    Console.ReadLine();
                    _instance.detener();
                }
                else
                {
                    Console.WriteLine(Properties.Resources.erroresInicioServicio + error);
                    Console.ReadLine();
                }
                if (madeConsole)
                    FreeConsole();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static ConfiguracionServicio _configServicio = null;

        private static ConfiguracionServicio configServicio
        {
            get
            {

                if (_configServicio == null)
                {
                    string seccion = "ConfigServicio";
                    _configServicio = (ConfiguracionServicio)config.GetSection(seccion);

                    if (_configServicio == null)
                        throw new Exception(string.Format(Properties.Resources.seccionNoEncontrada, seccion));
                }
                return _configServicio;
            }
        }

        private static Configuration _config = null;

        private static Configuration config
        {
            get
            {
                if (_config == null)
                {
                    _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = Path.Combine(path, "MTC.Host.exe.config");
                    if (!File.Exists(fileMap.ExeConfigFilename))
                        throw new Exception(string.Format(Properties.Resources.archivoConfiguracionNoExiste, fileMap.ExeConfigFilename));
                    _config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                }
                return _config;
            }
        }

        private static ConfiguracionNucleo _configuracionNucleo = null;
        static ConfiguracionNucleo configuracionNucleo
        {
            get
            {
                if (_configuracionNucleo == null)
                {

                    string seccion = "ConfigNucleo";
                    _configuracionNucleo = (ConfiguracionNucleo)config.GetSection(seccion);
                    if (_configuracionNucleo == null)
                        throw new Exception(string.Format(Properties.Resources .seccionNoEncontrada, seccion));
                }
                return _configuracionNucleo;
            }
        }

        private static INucleo crearInstancia(bool haceUsoDeLog)
        {
            var parametrosNucleo = new Dictionary<string, object>() {
            { "haceUsoDeLog", haceUsoDeLog },
            { "versionHostContenedor", fvi.ProductVersion },
            {"configServicioHost", configServicio},
                {"cultura", cultura },
                { "icono", icono } };
            return (INucleo)Activator.CreateInstance(Type.GetType(configuracionNucleo.tipoProveedor), new object[1] { parametrosNucleo });
        }
        
        public static bool AttachToConsole()
        {
            const uint ParentProcess = 0xFFFFFFFF;
            if (!AttachConsole(ParentProcess))
                return false;

            Console.Clear();
            Console.WriteLine("Attached to console!");
            return true;
        }

        private void InitializeComponent()
        {
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = true;
            this.ServiceName = "MTCHost";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            string error = "";
            _nucleo.iniciar(out error);
        }

        protected override void OnStop()
        {
            if (_nucleo != null)
                _nucleo.detener();
        }

        protected override void OnShutdown()
        {
            /*cargador.detener();
            cargador = null;*/
            base.OnShutdown();
        }

        static Regex pathrx = new Regex("(?<=\").+(?=\")");

        private static string GetImagePath(string servicename)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + servicename;
            RegistryKey keyHKLM = Registry.LocalMachine;
            RegistryKey key;
            key = keyHKLM.OpenSubKey(registryPath);
            string value = key.GetValue("ImagePath").ToString();
            key.Close();
            var result = pathrx.Match(value);
            if (result.Success)
                return result.Value;
            return value;
        }
    }
}
