using System;
using System.Threading;
using System.Windows.Threading;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public static class ThreadHelper {
        public static void DoInThread(Dispatcher dispatcher, Action action) {
            if(dispatcher.CheckAccess()) {
                action();
            } else {
                AutoResetEvent done = new AutoResetEvent(false);
                dispatcher.BeginInvoke((Action)delegate() {
                    action();
                    done.Set();
                });
                done.WaitOne();
            }
        }
    }
}
