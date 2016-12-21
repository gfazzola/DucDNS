using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Configuration;
using System.ServiceProcess;
using System.Configuration.Install;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Net;
using System.Windows.Forms;
using MTC.Host.IComun;
using System.Linq;

namespace MTC.Host
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            //process.Account = ServiceAccount.NetworkService;
            service = new ServiceInstaller();
            Installers.Add(process);
            Installers.Add(service);
        }

        private void RemoveIfExists(string serviceName)
        {
            //http://www.codeproject.com/Tips/855152/Windows-Self-installing-Named-Services
            if (ServiceController.GetServices().Any(s => s.ServiceName.ToLower() == serviceName.ToLower()))
                Uninstall(null);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {

            /*string userName = this.Context.Parameters["cuenta"];
            if (userName == null)
            {
                throw new InstallException("Missing parameter 'UserName'");
            }

            string password = this.Context.Parameters["clave"];
            if (password == null)
            {
                throw new InstallException("Missing parameter 'Password'");
            }*/

            string appHost = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string _path = System.IO.Path.GetDirectoryName(appHost);

            var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(_path, "MTC.Host.exe.config");
            if (!File.Exists(fileMap.ExeConfigFilename))
                throw new Exception(string.Format(Properties.Resources.archivoConfiguracionNoExiste, fileMap.ExeConfigFilename));
            _config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            string seccion = "ConfigServicio";
            var _configServicio = (ConfiguracionServicio)_config.GetSection(seccion);

            if (_configServicio == null)
                throw new Exception(string.Format(Properties.Resources.seccionNoEncontrada, seccion));

            process.Account = _configServicio.cuentaServicio;

            service.ServiceName = _configServicio.nombre;
            service.Description = _configServicio.descripcion;
            service.StartType = ServiceStartMode.Automatic;

            if (!string.IsNullOrEmpty(_configServicio.serviciosDependientes))
                service.ServicesDependedOn = _configServicio.serviciosDependientes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            else
                service.ServicesDependedOn = new string[] { "Winmgmt" };

            RemoveIfExists(service.ServiceName);
            base.OnBeforeInstall(savedState);

            if (1 == 2)
                try
                {
                    appHost = string.Format("\"{0}\" /w", appHost);
                    Utils.setAppAlIniciarWindows(service.ServiceName, appHost, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        public void Uninstall(string serviceName)
        {
            service.ServiceName = serviceName;
            base.Uninstall(null);
        }
    }
}
