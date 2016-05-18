using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plat
{
    public partial class FormUserManager : Form
    {
        public FormUserManager()
        {
            InitializeComponent();
        }

        private void FormUserManager_Load(object sender, EventArgs e)
        {
            query();
        }
        public void query()
        {
            try
            {
                DataTable dt = DbAccess.ServiceDB.ExecuteSql("select * from bas_user");
                gridControl1.DataSource = dt;
                gridView1.PopulateColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormUserDetail detail = new FormUserDetail();
                if (detail.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    query();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                FormUserDetail detail = new FormUserDetail();


                DataTable dataSource = (gridControl1.DataSource as DataTable);
                DataTable dtDel = dataSource.Clone();
                dtDel.TableName = "bas_user";
                int[] rows = gridView1.GetSelectedRows();

                for (int i = 0; i < rows.Length; i++)
                {
                    dtDel.Rows.Add(dataSource.Rows[rows[i]].ItemArray);
                }

                detail.dtDetail = dtDel;


                if (detail.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                        query();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
 
               DataTable dataSource =  (gridControl1.DataSource as DataTable);
               DataTable dtDel = dataSource.Clone();
               dtDel.TableName = "bas_user";
               int[] rows = gridView1.GetSelectedRows();
              
               for (int i = 0; i < rows.Length; i++)
               {
                   dtDel.Rows.Add(dataSource.Rows[rows[i]].ItemArray);
               }
               if (DbAccess.ServiceDB.DeleteRow(dtDel, "uid") > 0)
               {
                   query();
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
