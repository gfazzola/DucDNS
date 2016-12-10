using System;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Collections.Generic;
namespace MTC.Host.IComun
{
    public interface INucleo : IDisposable
    {
        bool iniciar(out string error);
        void detener();
        ApplicationContext contextoAplicacion(object[] args);
        string path { get; }
        void buscarInfoEnsamblado(System.Reflection.Assembly ensamblado);
        string productVersion { get; }
        string productVersionHostContenedor { get; }
        string nombre { get; }
        void configurar();
        void cambioDeSesion(SessionChangeDescription changeDescription);
        Dictionary<string, string> infoEnsamblado { get; }
        ConfiguracionServicio configServicioHost { get; }
        bool iniciado { get; }
        string cultura { get; }
    }

}