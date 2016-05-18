using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plat
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                Application.EnableVisualStyles();
                FormLogin epLogin = new FormLogin();
                Application.Run(epLogin);
               

            }
            catch (Exception e)
            {
               

                return  ;
            }
        }
    }
}
