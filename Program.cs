using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace TaskbarPerformance
{
    static class Program
    {
        private static PerformanceCounter perCounter = new PerformanceCounter() { CategoryName = "Processor", CounterName= "% Processor time", InstanceName = "_Total"};
        //private static Random rand = new();
        public static readonly PerformanceWindow pwin;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        static Program()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            pwin = new(val => val switch
            {
                100 => TaskbarProgressBarState.Error,
                > 75 => TaskbarProgressBarState.Paused,
                _ => TaskbarProgressBarState.Normal,
            }, () => (sbyte)perCounter.NextValue()
            )
            { 
                Prefix = "CPU: ",
                Suffix = " %"
            };
        }
        [STAThread]
        static void Main()
        {
        
            Application.Run(pwin);

        }
    }
}
