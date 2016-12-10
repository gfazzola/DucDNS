using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace MTC.Host
{

    public static class SingleInstanceClass
    {
        // Add "global\" in front of the EventName, then only one instance permitido(a) on the
        // whole system, including other users. But the application can not be brought
        // into view, of course. 
        private static String SingleAppComEventName = null;
        private static BackgroundWorker singleAppComThread = null;
        private static EventWaitHandle threadComEvent = null;
        private static CleanupCode cleanupCode = null;

        private class CleanupCode
        {
            ~CleanupCode()
            {
                SingleInstanceClass.Cleanup();
            }
        }
        /// <summary>
        /// Cleanup the sources that were allocated in CheckForOtherApp().
        /// </summary>
        static private void Cleanup()
        {
            // stop the thread.
            if (singleAppComThread != null)
                singleAppComThread.CancelAsync();

            // cloe the event so another instance can start again.
            if (threadComEvent != null)
                threadComEvent.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns>True is another instance is already running. False otherwise</returns>
        static public Boolean CheckForOtherApp(String uniqueName)
        {
            SingleAppComEventName = uniqueName;
            try
            {
                // another instance is already running
                threadComEvent = EventWaitHandle.OpenExisting(SingleAppComEventName);
                threadComEvent.Set();  // signal the other instance.
                threadComEvent.Close();
                return true;    // return immediatly.
            }
            catch { /* don't care about errors */     }
            // Create the Event handle
            threadComEvent = new EventWaitHandle(false, EventResetMode.AutoReset, SingleAppComEventName);
            // make sure the resources are cleaned up afterwards.
            cleanupCode = new CleanupCode();
            CreateInterAppComThread();

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        static private void CreateInterAppComThread()
        {
            singleAppComThread = new BackgroundWorker();
            singleAppComThread.WorkerReportsProgress = false;
            singleAppComThread.WorkerSupportsCancellation = true;
            singleAppComThread.DoWork += new DoWorkEventHandler(singleAppComThread_DoWork);
            singleAppComThread.RunWorkerAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void singleAppComThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            WaitHandle[] waitHandles = new WaitHandle[] { threadComEvent };

            while (!worker.CancellationPending)
            {
                // check every second for a signal.
                if (WaitHandle.WaitAny(waitHandles, 1000) == 0)
                {
                    // The user tried to start another instance. We can't allow that, 
                    // so bring the other instance back into view and enable that one. 
                    // That form is created in another thread, so we need some thread sync magic.
                    if (Application.OpenForms.Count > 0)
                    {
                        Form mainForm = Application.OpenForms[0];
                        mainForm.Invoke(new SetFormVisableDelegate(ThreadFormVisable), mainForm);
                    }
                }
            }
        }
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// When this method is called using a Invoke then this runs in the thread
        /// that created to form, which is nice. 
        /// </summary>
        /// <param name="frm"></param>
        private delegate void SetFormVisableDelegate(Form frm);
        static private void ThreadFormVisable(Form frm)
        {
            if (frm != null)
            {
                // display the form and bring to foreground.
                frm.Visible = true;
                frm.WindowState = FormWindowState.Normal;
                frm.Show();
                SetForegroundWindow(frm.Handle);
            }
        }
    }
}
