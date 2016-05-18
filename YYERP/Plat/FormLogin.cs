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
           // this.Close();
        }
        private struct ReturnMessage
        {
            public int _code;
            public string _msg;
            public string _stack;
        }
        Thread thread = null;


        private int Login(LoginInfo li)
        {
            _bLoginSucceeded = false;
            //此处留个后面,哈哈..避免当不知道用户名密码的时候..测试用.
            if (li._userid == "admin")
            {
                //密码暂时定位admin简单点
                if (li._password == "admin")
                {
                    _bLoginSucceeded = true;
                    return 1;
                }
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", li._userid);
            parameters.Add("pwd", li._password);
            DataTable dt=  DbAccess.ServiceDB.ExecuteSql("select * from bas_user where uid=@id and pwd=@pwd", parameters);
            if (dt.Rows.Count > 0)
            {
                _bLoginSucceeded = true;
                return 1;
            } 
            return -1;
            //row["username"] = li._userid;
            //row["password"] = li._password;
           
 
        }
        private void backgroundWorkerLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            ReturnMessage rm = new ReturnMessage();
            try
            {
                thread = Thread.CurrentThread;
                thread.IsBackground = true;
                rm._code = Login((LoginInfo)e.Argument);
                if (rm._code < 0)
                {
                    rm._msg = "用户名或密码错误.";
                }
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

            ReturnMessage rm = (ReturnMessage)e.Result;
            if (rm._code < 0)
            {
                DevExpress.XtraEditors.XtraMessageBox   .Show(this, string.Format("{0}", rm._msg), "错误");
                _bLoginSucceeded = false;
                //_semaLogining.Release();
                return;

            }
            else
            {
                //登录成功
                if (_bLoginSucceeded)
                {
                    this.Hide();
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(SplashScreen));
                    FormMain frmMain = new FormMain();
                    System.Threading.Thread.Sleep(1000);
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

                  
                    frmMain.ShowDialog();
                    this.Close();
                }
            }

           
        }
    }
}
