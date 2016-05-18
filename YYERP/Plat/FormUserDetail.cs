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
    public partial class FormUserDetail : Form
    {
        public FormUserDetail()
        {
            InitializeComponent();
        }
        public  DataTable dtDetail = new DataTable();
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtDetail.Rows.Count == 0)
                {
                    vw_sec_userBindingSource.EndEdit();
                    DataTable dtAdd = dataSetBase.bas_user.AsDataView().ToTable();
                    DbAccess.ServiceDB.InsertRow(dtAdd);
                }
                else
                {
                    dtDetail = vw_sec_userBindingSource.DataSource as DataTable;

                    DbAccess.ServiceDB.UpdateRow(dtDetail, "uid");
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               // DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
     
        }

        private void FormUserDetail_Load(object sender, EventArgs e)
        {
            try
            {

                if (dtDetail.Rows.Count == 0)
                {
                    vw_sec_userBindingSource.AddNew();
                }
                else
                {
                    vw_sec_userBindingSource.DataSource = dtDetail;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
