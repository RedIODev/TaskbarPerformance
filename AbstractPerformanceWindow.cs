using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace TaskbarPerformance
{
    public abstract partial class AbstractPerformanceWindow : Form
    {
        public AbstractPerformanceWindow(Timer timer)
        {
            InitializeComponent();
            timer.Tick += ChangeValue;
        }
        public bool StateFixed { get; set; }
        public abstract TaskbarProgressBarState MapState(float value);
        public abstract float ProvideValue();
        public abstract float ProvideMaxValue();
        public abstract string FormatText(float value);
        public void ChangeState(TaskbarProgressBarState state)
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;
            TaskbarManager.Instance.SetProgressState(state, Handle);
        }

        private void ChangeValue(object sender, EventArgs e)
        {
            var value = ProvideValue();
            var maxValue = ProvideMaxValue();
            float scaledValue = value * int.MaxValue / maxValue;
            int scaledInt = scaledValue < int.MaxValue ? (int)scaledValue : int.MaxValue;
            Text = FormatText(value);
            if (!TaskbarManager.IsPlatformSupported)
                return;
            var tbm = TaskbarManager.Instance;
            tbm.SetProgressValue(scaledInt, int.MaxValue, Handle);
            if (!StateFixed)
                tbm.SetProgressState(MapState(value), Handle);
        }
    }
}
