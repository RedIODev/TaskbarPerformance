using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace TaskbarPerformance
{
    static class Program
    {
        private static readonly Timer timer;
        public static readonly List<AbstractPerformanceWindow> performanceWindows;
        static Program()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            timer = new() { Interval = 1000, Enabled = true };
            performanceWindows = new() { new CpuPerformance(timer), new RamPerformance(timer) };
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new AppContext(performanceWindows));
        }
    }
}
