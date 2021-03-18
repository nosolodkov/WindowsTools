using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Killer
{
    public class TaskKiller
    {
        public event EventHandler<ProcessEventArgs> OnNewProcessKilled;

        private void RaiseNewProcessKilled(int id, string name, DateTime dateKilled)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} kill process: {name} id = {id}");

            OnNewProcessKilled?.Invoke(this, new ProcessEventArgs(name, id, dateKilled));
        }

        /// <summary>
        /// A list of names of processes that need to be killed.
        /// </summary>
        private static readonly List<string> _processes = new List<string>
        {
            "software_reporter_tool"
        };

        public void Run()
        {
            while (true)
            {
                foreach (var processName in _processes)
                {
                    foreach (var proc in Process.GetProcessesByName(processName))
                    {
                        try
                        {
                            var id = proc.Id;
                            var name = proc.ProcessName;
                            var time = DateTime.Now;

                            proc.Kill();

                            RaiseNewProcessKilled(id, name, time);
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            Console.ResetColor();
                        }
                    }
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
