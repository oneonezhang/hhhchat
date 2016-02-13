using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hhhchatClient
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
            Form1 form1 = new Form1();
            form1.ShowDialog();
            if (form1.DialogResult == DialogResult.OK)
            {
                form1.Close();
                Application.Run(new Form2(form1.userName, form1.clientSocket));
            }
        }
    }
}
