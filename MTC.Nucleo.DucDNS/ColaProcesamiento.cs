using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace MTC.Nucleo.DucDNS
{
    public class EventoNuevaTareaParamArgs : EventArgs
    {
        bool _tareaProcesada = false;
        private readonly object _param;

        public EventoNuevaTareaParamArgs(object param)
        {
            _param = param;
        }

        public object param
        {
            get { return _param; }
        }

        public bool tareaProcesada
        {
            get { return _tareaProcesada; }
            set { _tareaProcesada = value; }
        }
    }

    public class ColaProcesamiento<T> : IDisposable
    {
        EventWaitHandle _wh = new AutoResetEvent(false);
        Thread _worker;
        readonly object _locker = new object();
        Queue<T> _tasks = new Queue<T>();
        bool _disposed = false, finalizado = false;
        public event EventHandler<EventoNuevaTareaParamArgs> nuevaTarea;

        public ColaProcesamiento()
        {
            _worker = new Thread(Work);
            _worker.Start();
        }

        public void encolar(T task)
        {
            lock (_locker)
            {
                finalizado = task == null;
                _tasks.Enqueue(task);
            }
            _wh.Set();
        }

        public void encolar(List<T> tasks)
        {
            lock (_locker)
                foreach (T task in tasks)
                    _tasks.Enqueue(task);
            _wh.Set();
        }

        #region destruccion

        ~ColaProcesamiento()
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
                encolar(default(T));     // Signal the consumer to exit.
                _worker.Join();         // Wait for the consumer's thread to finish.
                _wh.Close();            // Release any OS resources.
            }
            catch { }
            _disposed = true;
        }

        #endregion

        void Work()
        {
            while (!finalizado)
            {
                T task = default(T);
                lock (_locker)
                    if (_tasks.Count > 0)
                    {
                        task = _tasks.Peek();
                        if (task == null)
                            return;
                    }

                if (task != null)
                {
                    var evt = new EventoNuevaTareaParamArgs(task);
                    OnNuevaTarea(evt);
                    if (evt.tareaProcesada)
                        _tasks.Dequeue();
                    else
                        _wh.WaitOne();//Por algun motivo la tarea no se proceso. Por ejemplo no habia comunicacion al servidor central con lo cual naranjas!
                }
                else
                    _wh.WaitOne();         // No more tasks - wait for a signal
            }
        }

        protected void OnNuevaTarea(EventoNuevaTareaParamArgs e)
        {
            EventHandler<EventoNuevaTareaParamArgs> handler = nuevaTarea;
            if (handler != null)
                handler(this, e);
        }

        public int longitud { get { return _tasks.Count; } }

        public void procesarCola()
        {
            _wh.Set();
        }
    }
}
