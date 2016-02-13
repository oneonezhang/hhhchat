using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hhhchatServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool createNew;
            using (Mutex mutex = new Mutex(false, Application.ProductName, out createNew))
            {
                if(createNew)
                {
                    Application.Run(new FormServer());
                }
                else
                {
                    MessageBox.Show("The server has been started!");
                    System.Environment.Exit(1);
                }
            }

        }
    }
}
