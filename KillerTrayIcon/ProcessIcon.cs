using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KillerTrayIcon
{
    class ProcessIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;


        internal ProcessIcon()
        {
            _notifyIcon = new NotifyIcon();
        }

        void HandleMouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"HandleMouseClick: {e.Button}, {e.Clicks}");
            if (e.Clicks > 1 && e.Button == MouseButtons.Right)
            {
                Environment.Exit(1);
            }
        }

        internal void Display()
        {
            _notifyIcon.MouseDoubleClick += new MouseEventHandler(HandleMouseClick);
            _notifyIcon.Icon = Properties.Resources.TrayIcon;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "ProcessKiller. Right mouse double click to exit.";
            _notifyIcon.ContextMenuStrip = new ContextMenuStrip();

            ShowBalloonTip(3000, "Task Killer", "Task killer running", ToolTipIcon.Info);
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        internal void ShowBalloonTip(int timeout, string caption, string text, ToolTipIcon icon)
        {
            _notifyIcon.ShowBalloonTip(timeout, caption, text, icon);
        }
    }
}
