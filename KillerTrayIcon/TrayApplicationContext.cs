using Killer;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillerTrayIcon
{
    class TrayApplicationContext : ApplicationContext
    {
        private readonly ProcessIcon _processIcon;

        public TrayApplicationContext()
        {
            _processIcon = new ProcessIcon();
            _processIcon.Display();

            Task.Factory.StartNew(() =>
            {
                var killer = new TaskKiller();
                killer.OnNewProcessKilled += Killer_OnNewProcessKilled;
                killer.Run();
            });
        }

        private void Killer_OnNewProcessKilled(object sender, ProcessEventArgs e)
        {
            _processIcon.ShowBalloonTip(1000, "Process Killer", e.ToString(), ToolTipIcon.Warning);
        }

        protected override void Dispose(bool disposing)
        {
            _processIcon.Dispose();
            base.Dispose(disposing);
        }
    }
}
