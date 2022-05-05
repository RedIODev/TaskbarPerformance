using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarPerformance
{
    class AppContext : ApplicationContext
    {
        public AppContext(List<PerformanceWindow> performanceWindows)
        {
            foreach (var window in performanceWindows)
                window.FormClosed += OnFormClosed;
            foreach (var window in performanceWindows)
                window.Show();
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                ExitThread();
        }
    }
}
