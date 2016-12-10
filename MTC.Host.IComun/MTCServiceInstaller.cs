using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Win32;
using System.ComponentModel;
namespace MTC.Host.IComun
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceManagerRights
    {
        /// <summary>
        /// 
        /// </summary>
        Connect = 0x0001,
        /// <summary>
        /// 
        /// </summary>
        CreateService = 0x0002,
        /// <summary>
        /// 
        /// </summary>
        EnumerateService = 0x0004,
        /// <summary>
        /// 
        /// </summary>
        Lock = 0x0008,
        /// <summary>
        /// 
        /// </summary>
        QueryLockStatus = 0x0010,
        /// <summary>
        /// 
        /// </summary>
        ModifyBootConfig = 0x0020,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | Connect | CreateService |
        EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceRights
    {
        /// <summary>
        /// 
        /// </summary>
        QueryConfig = 0x1,
        /// <summary>
        /// 
        /// </summary>
        ChangeConfig = 0x2,
        /// <summary>
        /// 
        /// </summary>
        QueryStatus = 0x4,
        /// <summary>
        /// 
        /// </summary>
        EnumerateDependants = 0x8,
        /// <summary>
        /// 
        /// </summary>
        Start = 0x10,
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x20,
        /// <summary>
        /// 
        /// </summary>
        PauseContinue = 0x40,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x80,
        /// <summary>
        /// 
        /// </summary>
        UserDefinedControl = 0x100,
        /// <summary>
        /// 
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
        QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
        Interrogate | UserDefinedControl)
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceBootFlag
    {
        /// <summary>
        /// 
        /// </summary>
        Start = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        SystemStart = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        AutoStart = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        DemandStart = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Disabled = 0x00000004
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1, // The state cannot be (has not been) retrieved.
        /// <summary>
        /// 
        /// </summary>
        NotFound = 0, // The service is not known on the host server.
        /// <summary>
        /// 
        /// </summary>
        Stop = 1, // The service is NET stopped.
        /// <summary>
        /// 
        /// </summary>
        Run = 2, // The service is NET started.
        /// <summary>
        /// 
        /// </summary>
        Stopping = 3,
        /// <summary>
        /// 
        /// </summary>
        Starting = 4,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Pause = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Continue = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        Shutdown = 0x00000005,
        /// <summary>
        /// 
        /// </summary>
        ParamChange = 0x00000006,
        /// <summary>
        /// 
        /// </summary>
        NetBindAdd = 0x00000007,
        /// <summary>
        /// 
        /// </summary>
        NetBindRemove = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        NetBindEnable = 0x00000009,
        /// <summary>
        /// 
        /// </summary>
        NetBindDisable = 0x0000000A
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceError
    {
        /// <summary>
        /// 
        /// </summary>
        Ignore = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        Normal = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Severe = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Critical = 0x00000003
    }

    /// <summary>
    /// Installs and provides functionality for handling windows services
    /// </summary>
    public class MTCServiceInstaller
    {
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        private const System.Int32 SERVICE_CONFIG_DESCRIPTION = 1;


        private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
        private const uint SERVICE_QUERY_CONFIG = 0x00000001;
        private const uint SERVICE_CHANGE_CONFIG = 0x00000002;
        private const uint SC_MANAGER_ALL_ACCESS = 0x000F003F;

        [StructLayout(LayoutKind.Sequential)]
        private class SERVICE_STATUS
        {
            public int dwServiceType = 0;
            public ServiceState dwCurrentState = 0;
            public int dwControlsAccepted = 0;
            public int dwWin32ExitCode = 0;
            public int dwServiceSpecificExitCode = 0;
            public int dwCheckPoint = 0;
            public int dwWaitHint = 0;
        }

        internal struct SERVICE_DESCRIPTION
        {
            internal System.IntPtr lpDescription;
        }

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerA")]
        private static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceManagerRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceA", CharSet = CharSet.Ansi)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "CreateServiceA")]
        private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceRights dwDesiredAccess, int
        dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);
        [DllImport("advapi32.dll")]
        private static extern int CloseServiceHandle(IntPtr hSCObject);
        [DllImport("advapi32.dll")]
        private static extern int QueryServiceStatus(IntPtr hService,
        SERVICE_STATUS lpServiceStatus);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int DeleteService(IntPtr hService);
        [DllImport("advapi32.dll")]
        private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);
        [DllImport("advapi32.dll", EntryPoint = "StartServiceA")]
        private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

        [DllImport("Advapi32", SetLastError = true, EntryPoint = "ChangeServiceConfig2")]
        private static extern bool ChangeServiceConfig2(System.IntPtr hService, int dwInfoLevel, System.IntPtr lpInfo);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Boolean ChangeServiceConfig(
            IntPtr hService,
            UInt32 nServiceType,
            UInt32 nStartType,
            UInt32 nErrorControl,
            String lpBinaryPathName,
            String lpLoadOrderGroup,
            IntPtr lpdwTagId,
            [In] char[] lpDependencies,
            String lpServiceStartName,
            String lpPassword,
            String lpDisplayName);


        /// <summary>
        /// 
        /// </summary>
        public MTCServiceInstaller()
        {
        }

        /// <summary>
        /// Takes a service name and tries to stop and then uninstall the windows serviceError
        /// </summary>
        /// <param name="ServiceName">The windows service name to uninstall</param>
        public static void Uninstall(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.StandardRightsRequired | ServiceRights.Stop |
                ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    throw new Exception(Properties.Resources.servicioNoInstalado);
                }
                try
                {
                    StopService(service);
                    int ret = DeleteService(service);
                    if (ret == 0)
                    {
                        int error = Marshal.GetLastWin32Error();
                        throw new Exception(string.Format(Properties.Resources.servicioNoPudoSerEliminado,  error));
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Accepts a service name and returns true if the service with that service name exists
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for existence</param>
        /// <returns>True if that service exists false otherwise</returns>
        public static bool ServiceIsInstalled(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.QueryStatus);
                if (service == IntPtr.Zero) return false;
                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the windows service.
        /// </summary>
        /// <param name="ServiceName">The service name that this service will have</param>
        /// <param name="DisplayName">The display name that this service will have</param>
        /// <param name="FileName">The path to the executable of the service</param>
        public unsafe static bool Install(string ServiceName, string DisplayName, string FileName, bool start, ref string error)
        {
            bool result = false;
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect | ServiceManagerRights.CreateService);
            error = "";
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.QueryStatus | ServiceRights.Start);
                if (service == IntPtr.Zero)
                {
                    service = CreateService(scman, ServiceName, DisplayName,
                    ServiceRights.QueryStatus | ServiceRights.Start, SERVICE_WIN32_OWN_PROCESS,
                    ServiceBootFlag.AutoStart, ServiceError.Normal, FileName, null, IntPtr.Zero,
                    null, null, null);
                }

                if (service == IntPtr.Zero)
                {
                    error = Properties.Resources.servicioNoPudoSerInstalado;
                    throw new Exception(error);
                }

                result = true;
                
                try
                {
                    if (start)
                        StartService(service);

                }
                finally
                {
                    CloseServiceHandle(service);
                }

            }
            finally
            {
                CloseServiceHandle(scman);
            }
            return result;
        }

        public unsafe static bool SetServiceDescription(string ServiceName, string Description, ref string error)
        {
            bool result = false;
            byte* temp = stackalloc byte[Description.Length + 1];

            for (int runner = 0; runner < Description.Length; runner++)
            {
                temp[runner] = (byte)Description[runner];
            }
            temp[Description.Length] = 0;

            SERVICE_DESCRIPTION Info;
            Info.lpDescription = new System.IntPtr(temp);

            IntPtr scman = OpenSCManager(ServiceManagerRights.AllAccess);
            error = "";
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.ChangeConfig);
                try
                {
                    if (service != IntPtr.Zero)
                    {
                        result = ChangeServiceConfig2(service, SERVICE_CONFIG_DESCRIPTION, new System.IntPtr(&Info));
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }

            return (result);
        }

        /// <summary>
        /// Takes a service name and starts it
        /// </summary>
        /// <param name="Name">The service name</param>
        public static void StartService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus |
                ServiceRights.Start);
                if (hService == IntPtr.Zero)
                {
                    throw new Exception(Properties.Resources.servicioNoPudoSerAbierto);
                }
                try
                {
                    StartService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="Name">The service name that will be stopped</param>
        public static void StopService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus |
                ServiceRights.Stop);
                if (hService == IntPtr.Zero)
                {
                    throw new Exception(Properties.Resources.servicioNoPudoSerAbierto);
                }
                try
                {
                    StopService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stars the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StartService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            StartService(hService, 0, 0);
            WaitForServiceStatus(hService, ServiceState.Starting, ServiceState.Run);
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StopService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            ControlService(hService, ServiceControl.Stop, status);
            WaitForServiceStatus(hService, ServiceState.Stopping, ServiceState.Stop);
        }

        /// <summary>
        /// Takes a service name and returns the <code>ServiceState</code> of the corresponding service
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for his <code>ServiceState</code></param>
        /// <returns>The ServiceState of the service we wanted to check</returns>
        public static ServiceState GetServiceStatus(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, ServiceName,
                ServiceRights.QueryStatus);
                if (hService == IntPtr.Zero)
                {
                    return ServiceState.NotFound;
                }
                try
                {
                    return GetServiceStatus(hService);
                }
                finally
                {
                    CloseServiceHandle(scman);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Gets the service state by using the handle of the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <returns>The <code>ServiceState</code> of the service</returns>
        private static ServiceState GetServiceStatus(IntPtr hService)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            if (QueryServiceStatus(hService, ssStatus) == 0)
            {
                throw new Exception(Properties.Resources.errorInterrogandoEstadoServicio);
            }
            return ssStatus.dwCurrentState;
        }

        /// <summary>
        /// Returns true when the service status has been changes from wait status to desired status
        /// ,this method waits around 10 seconds for this operation.
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <param name="WaitStatus">The current state of the service</param>
        /// <param name="DesiredStatus">The desired state of the service</param>
        /// <returns>bool if the service has successfully changed states within the allowed timeline</returns>
        private static bool WaitForServiceStatus(IntPtr hService, ServiceState WaitStatus, ServiceState DesiredStatus)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            int dwOldCheckPoint;
            int dwStartTickCount;

            QueryServiceStatus(hService, ssStatus);
            if (ssStatus.dwCurrentState == DesiredStatus) return true;
            dwStartTickCount = Environment.TickCount;
            dwOldCheckPoint = ssStatus.dwCheckPoint;

            while (ssStatus.dwCurrentState == WaitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.

                int dwWaitTime = ssStatus.dwWaitHint / 10;

                if (dwWaitTime < 1000)
                    dwWaitTime = 1000;
                else
                    if (dwWaitTime > 10000) dwWaitTime = 10000;

                System.Threading.Thread.Sleep(dwWaitTime);

                // Check the status again.

                if (QueryServiceStatus(hService, ssStatus) == 0) break;

                if (ssStatus.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = ssStatus.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > ssStatus.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }
            return (ssStatus.dwCurrentState == DesiredStatus);
        }

        /// <summary>
        /// Opens the service manager
        /// </summary>
        /// <param name="Rights">The service manager rights</param>
        /// <returns>the handle to the service manager</returns>
        private static IntPtr OpenSCManager(ServiceManagerRights Rights)
        {
            IntPtr scman = OpenSCManager(null, null, Rights);
            if (scman == IntPtr.Zero)
            {
                throw new Exception(Properties.Resources.noSePudoConectarControlManager);
            }
            return scman;
        }

        public static ServiceControllerStatus GetServiceStatus2(string ServiceName)
        {
            ServiceController sc = new ServiceController(ServiceName);
            return sc.Status;
        }

        public static void StartService2(string ServiceName, out string error)
        {
            error = "";
            var sc = new ServiceController(ServiceName);
            if (sc.Status == ServiceControllerStatus.Running)
                return;

            try
            {
                // Start the service, and wait until its status is "Running".
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        public static bool StartService3(string ServiceName, out string error)
        {
            error = "";
            var sc = new ServiceController(ServiceName);
            if (sc.Status == ServiceControllerStatus.Running)
                return false;

            bool result = true;
            try
            {
                // Start the service, and wait until its status is "Running".
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// devuelve true si el servicio estaba en ejecucion y se pudo detener
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool StopService2(string ServiceName, out string error)
        {
            bool result = false;
            error = "";
            var sc = new ServiceController(ServiceName);
            if (sc.Status == ServiceControllerStatus.Stopped)
                goto salida;

            try
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                result = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

        salida:
            return result;
        }

        public static string ubicacionServicio(string nombre)
        {
            string result = "";

            Microsoft.Win32.RegistryKey services = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services");
            if (services != null)
                result = (string)services.OpenSubKey(nombre).GetValue("ImagePath");

            /*
           * en el caso del vnc puede venir de esta forma y estamos al horno: "C:\Program Files\TightVNC\tvnserver.exe" -service
          */
            if (result.StartsWith("\""))
            {
                result = result.Substring(1, result.Length - 1);
                int pos = result.IndexOf('"');
                result = result.Substring(0, pos);
            }

            return result;
        }

        /// <summary>
        /// Devuelve un array de string con sextuplas de la forma Nombre, Estado, modoInicio, Ubicacion, Descripcion, Error
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string[] servicios()
        {
            ServiceController[] services = ServiceController.GetServices();
            List<string> xresult = new List<string>();
            string nombre, estado, path, descripcion, modoInicio;
            object info = null;
            foreach (ServiceController service in services)
            {
                modoInicio = nombre = estado = path = descripcion = "";
                try
                {
                    nombre = service.ServiceName;
                    int estadoComoInt = (int)service.Status;
                    estado = estadoComoInt.ToString();

                    RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + service.ServiceName);

                    info = regKey1.GetValue("ImagePath");
                    path = info == null ? "" : info.ToString();
                    info = regKey1.GetValue("Description");
                    descripcion = info == null ? "" : info.ToString();

                    info = regKey1.GetValue("Start");
                    modoInicio = info == null ? "" : info.ToString();
                    regKey1.Close();

                    xresult.Add(nombre);
                    xresult.Add(estado);
                    xresult.Add(modoInicio);
                    xresult.Add(path);
                    xresult.Add(descripcion);
                    xresult.Add("");
                }
                catch (Exception ex)
                {
                    xresult.Add(nombre);
                    xresult.Add(estado);
                    xresult.Add(modoInicio);
                    xresult.Add(path);
                    xresult.Add(descripcion);
                    xresult.Add(Utils.armarMensajeErrorExcepcion(ex));
                }
            }
            //xresult.Insert(0, "6"); //indico en la primera posicion que son sextuplas
            return xresult.ToArray();
        }

        public static bool ChangeStartMode(string nombre, ServiceStartMode mode, out string error)
        {
            bool result = false;
           
            IntPtr scman = OpenSCManager(ServiceManagerRights.AllAccess);
            error = "";
            try
            {
                IntPtr service = OpenService(scman, nombre, ServiceRights.ChangeConfig);
                try
                {
                    if (service != IntPtr.Zero)
                    {
                        result = ChangeServiceConfig(service, SERVICE_NO_CHANGE, (uint)mode, SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null);

                        if (!result)
                        {
                            int nError = Marshal.GetLastWin32Error();
                            var win32Exception = new Win32Exception(nError);
                            error = win32Exception.Message;
                        }
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }

            return result;
        }
    }
}