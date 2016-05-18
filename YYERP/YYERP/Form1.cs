using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace YYERP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //YYERP.Dao.UserDataAccess.ExecuteDataSet("select * from bas_brand");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            for (int i = 0; i < 20; i++)
            {
                dt.Rows.Add(i, i * 3 + "Name");
            }

            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlConnectionStringBuilder s = new MySqlConnectionStringBuilder();
                s.Server = "127.0.0.1";
                s.Port = 3306; //mysql端口号
                s.Database = "yyerp";
                s.UserID = "root";
                s.Password = "";
                s.CharacterSet = "latin1";
                MySqlConnection mcon = new MySqlConnection(s.ConnectionString);
                mcon.Open();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
