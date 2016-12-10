using System;
using System.Management;

namespace MTC.Host.IComun
{
    /// <summary>
    /// Idea tomada de aca 
    /// http://dotnetcodr.com/2014/12/02/getting-notified-by-a-windows-service-status-change-in-c-net/
    /// </summary>
    public class MonitorEstadoServicio : IDisposable
    {
        bool _disposed = false;
        ManagementEventWatcher watcher = null;
        string nombreServicio;
        public event EventHandler<EventoCambioEstadoServicioParamArgs> eventoCambioEstadoServicio;
        public MonitorEstadoServicio(string nombreServicio)
        {
            this.nombreServicio = nombreServicio;
            var eventQuery = new EventQuery();
            eventQuery.QueryString = "SELECT * FROM __InstanceModificationEvent within 2 WHERE targetinstance isa 'Win32_Service'";
            watcher = new ManagementEventWatcher(eventQuery);
            watcher.EventArrived += watcher_EventArrived;
        }

        public void iniciar()
        {
            watcher.Start();
        }

        void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject evento = e.NewEvent;
            ManagementBaseObject targetInstance = ((ManagementBaseObject)evento["targetinstance"]);
            PropertyDataCollection props = targetInstance.Properties;
            foreach (PropertyData prop in props)
                if (string.Compare(prop.Name, "Name", true) == 0 && string.Compare((string)prop.Value, nombreServicio, true) == 0)
                {
                    OnCambioEstadoServicio(new EventoCambioEstadoServicioParamArgs(nombreServicio));
                    break;
                }
        }

        protected void OnCambioEstadoServicio(EventoCambioEstadoServicioParamArgs e)
        {
            EventHandler<EventoCambioEstadoServicioParamArgs> handler = eventoCambioEstadoServicio;
            if (handler != null)
                handler(this, e);
        }

        #region destruccion

        ~MonitorEstadoServicio()
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

            if (watcher != null)
            {
                watcher.Stop();
                watcher.EventArrived -= watcher_EventArrived;
                watcher = null;
            }
            _disposed = true;
        }

        #endregion
    }

    public class EventoCambioEstadoServicioParamArgs : EventArgs
    {
        // private readonly NivelLog _nivel;
        private readonly string _nombreServicio;
        public EventoCambioEstadoServicioParamArgs(string nombreServicio)
        {
            _nombreServicio = nombreServicio;
        }

        public string nombreServicio
        {
            get { return _nombreServicio; }
        }
    }
}
