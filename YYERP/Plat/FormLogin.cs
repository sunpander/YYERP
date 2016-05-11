using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Plat
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        public static int Startup( )
        {
              Application.EnableVisualStyles();
           
             FormLogin epLogin = new FormLogin( );
             
              Application.Run(epLogin);
              return 0;
        }
        private struct LoginInfo
        {
            public string _appname;
            public string _userid;
            public string _password;
            public string _companycode;
            public bool _onlylogin;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginInfo li = new LoginInfo();
            li._userid = txtUserName.Text;
            li._password = txtPwd.Text;
            this.btnLogin.Enabled = false;
            
            backgroundWorkerLogin.RunWorkerAsync(li);

        
        }
        private struct ReturnMessage
        {
            public int _code;
            public string _msg;
            public string _stack;
        }
        Thread thread = null;
        private void backgroundWorkerLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            ReturnMessage rm = new ReturnMessage();
            try
            {
                thread = Thread.CurrentThread;
                thread.IsBackground = true;
                //rm._code = Login((LoginInfo)e.Argument);
            }
            catch (Exception ex)
            {
                rm._code = -1;
                rm._msg = ex.Message;
            }

            e.Result = rm;
        }
        public bool _bLoginSucceeded = false;
        private void backgroundWorkerLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnLogin.Enabled = true;
            this.Hide();
            FormMain frmMain = new FormMain();
            this.Visible = false;

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm( typeof(SplashScreen) );

            System.Threading.Thread.Sleep(1000);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
            frmMain.ShowDialog();
             this.Close();
        }
    }
}
