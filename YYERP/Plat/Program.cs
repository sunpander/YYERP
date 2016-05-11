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
                FormLogin.Startup();
            }
            catch (Exception e)
            {
               

                return  ;
            }
        }
    }
}
