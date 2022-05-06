using Microsoft.VisualBasic.Devices;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarPerformance
{
    public class RamPerformance : AbstractPerformanceWindow
    {
        private const float GB = 0x3FFFFFFF + 1;
        private readonly ComputerInfo computerInfo = new();
        public RamPerformance(Timer timer) : base(timer) {}

        public override string FormatText(float value) => $"RAM: {value / GB:0.00} GB";

        public override TaskbarProgressBarState MapState(float value) => value switch
        {
            100 => TaskbarProgressBarState.Error,
            > 75 => TaskbarProgressBarState.Paused,
            _ => TaskbarProgressBarState.Normal,
        };

        public override float ProvideMaxValue() => computerInfo.TotalPhysicalMemory;

        public override float ProvideValue() => computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory;
    }
}
