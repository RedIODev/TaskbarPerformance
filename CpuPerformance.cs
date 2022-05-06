using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarPerformance
{
    public class CpuPerformance : AbstractPerformanceWindow
    {
        private readonly PerformanceCounter perCounter = new()
        {
            CategoryName = "Processor",
            CounterName = "% Processor time",
            InstanceName = "_Total"
        };
        public CpuPerformance(Timer timer) :base(timer) {}

        public override string FormatText(float value) => $"CPU: {Math.Floor(value)} %";

        public override TaskbarProgressBarState MapState(float value) => value switch
        {
            100 => TaskbarProgressBarState.Error,
            > 75 => TaskbarProgressBarState.Paused,
            _ => TaskbarProgressBarState.Normal,
        };

        public override float ProvideMaxValue() => 100;

        public override float ProvideValue() => perCounter.NextValue();
    }
}
