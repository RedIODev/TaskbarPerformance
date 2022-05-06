using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarPerformance
{
    public class BatteryPerformance : AbstractPerformanceWindow
    {
        private readonly PowerStatus powerStatus = SystemInformation.PowerStatus;
        public BatteryPerformance(Timer timer) :base(timer) {}
        public override string FormatText(float value) => $"Battery: {Math.Floor(value)} %";

        public override TaskbarProgressBarState MapState(float value) => powerStatus.BatteryChargeStatus switch
        {
            BatteryChargeStatus.Charging => TaskbarProgressBarState.Indeterminate,
            BatteryChargeStatus.Critical => TaskbarProgressBarState.Error,
            BatteryChargeStatus.Low => TaskbarProgressBarState.Paused,
            BatteryChargeStatus.NoSystemBattery => TaskbarProgressBarState.NoProgress,
            _ => TaskbarProgressBarState.Normal
        };

        public override float ProvideMaxValue()
        {
            throw new NotImplementedException();
        }

        public override float ProvideValue()
        {
            throw new NotImplementedException();
        }
    }
}
