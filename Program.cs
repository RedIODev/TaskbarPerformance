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
        private static readonly Timer timer;
        public static readonly PerformanceWindow cpuPerformance;
        public static readonly PerformanceWindow ramPerformance;
        static Program()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            timer = new() { Interval = 1000, Enabled = true };
            cpuPerformance = new(CpuStateMapper, CpuValueProvider, "CPU: {0} %", timer);
            ramPerformance = new(RamStateMapper, RamValueProvider, "RAM: {0} GB", timer);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new AppContext(new() { cpuPerformance ,ramPerformance}));
        }

        #region CPU
        private static readonly PerformanceCounter perCounter = new()
        {
            CategoryName = "Processor",
            CounterName = "% Processor time",
            InstanceName = "_Total"
        };

        private static (float,float) CpuValueProvider() => ((float)Math.Floor(perCounter.NextValue()), 100);

        private static TaskbarProgressBarState CpuStateMapper(float value) => value switch
        {
            100 => TaskbarProgressBarState.Error,
            > 75 => TaskbarProgressBarState.Paused,
            _ => TaskbarProgressBarState.Normal,
        };
        #endregion

        #region RAM
        //RAM not acually implemented right
        private static (float,float) RamValueProvider() => (perCounter.NextValue(), 100);

        private static TaskbarProgressBarState RamStateMapper(float value) => value switch
        {
            100 => TaskbarProgressBarState.Error,
            > 75 => TaskbarProgressBarState.Paused,
            _ => TaskbarProgressBarState.Normal,
        };
        #endregion
    }
}
