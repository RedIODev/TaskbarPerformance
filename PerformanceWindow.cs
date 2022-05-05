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
    public delegate TaskbarProgressBarState StateMapper(float value); 
    public delegate (float,float) ValueProvider();

    public partial class PerformanceWindow : Form
    {
        public bool StateFixed { get; set; }

        private readonly StateMapper stateMapper;
        private readonly ValueProvider valueProvider;
        private readonly string format;

        public PerformanceWindow(StateMapper stateMapper, ValueProvider valueProvider, string format, Timer timer)
        {
            InitializeComponent();
            this.stateMapper = stateMapper;
            this.valueProvider = valueProvider;
            this.format = format;
            timer.Tick += ChangeValue;
        }

        public void ChangeState(TaskbarProgressBarState state)
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;
            TaskbarManager.Instance.SetProgressState(state, Handle);
        }

        private void ChangeValue(object sender, EventArgs e)
        {
            var (value, maxValue) = valueProvider();
            Text = string.Format(format, value);
            if (!TaskbarManager.IsPlatformSupported)
                return;
            var tbm = TaskbarManager.Instance;
            tbm.SetProgressValue(Convert.ToInt32(value), Convert.ToInt32(maxValue), Handle);
            if(!StateFixed)
                tbm.SetProgressState(stateMapper(value), Handle);
        }
    }
}
