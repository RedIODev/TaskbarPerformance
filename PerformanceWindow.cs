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
    public partial class PerformanceWindow : Form
    {
        private static readonly TaskbarManager tbm = TaskbarManager.Instance;
        private readonly Timer timer = new() { Interval = 1000, Enabled = true };

        public delegate TaskbarProgressBarState StateHandler(sbyte value);
        public delegate sbyte StateProvider();
        public sbyte MaxValue { get; } = 100;
        public int UpdateInterval { get => timer.Interval; set => timer.Interval = value; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public StateHandler Handler { get; set; }
        public StateProvider Provider { get; set; }

        public TaskbarProgressBarState ProgressBarState { set {
                if (IsDisposed)
                    return;
                tbm.SetProgressState(value, Handle);
            }
        }

        public sbyte ProgressBarValue { set {
                ProgressBarState = Handler(value);
                tbm.SetProgressValue(value, MaxValue, Handle);
            }
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            sbyte value = Provider();
            ProgressBarValue = value;
            Text = $"{Prefix}{value}{Suffix}";
        }

        public PerformanceWindow(StateHandler handler, StateProvider provider)
        {
            InitializeComponent();
            Handler = handler;
            Provider = provider;
            timer.Tick += Timer_Tick;
        }

        
    }
}
