using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YYERP.Common.UI
{
    public  partial class FormList : FormBase
    {
        private  String ListDatableName = "bas_user";
        //public abstract String getTableName();
        
        public FormList()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                query();
            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex.Message);
            }
        }



        public virtual void query()
        {
            try
            {
                DataTable dt = getTestDatable();
                gridControl1.DataSource = dt;
                gridView1.PopulateColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public DataTable getTestDatable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("CLASS");
            dt.Columns.Add("AGE");
            for (int i = new Random().Next(20)+3; i >0; i--)
            {
                dt.Rows.Add("ID" + i, "NAME" + i,"班级"+i/5,i*3);
            }
            return dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormPopup detail = new FormPopup();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (gridControl1.DataSource as DataTable);
                DataTable dtSelected = dataSource.Clone();
                dtSelected.TableName = ListDatableName;
                int[] rows = gridView1.GetSelectedRows();
                if (rows.Length ==0)
                {
                    YYMessageBox.Show("选择要修改的行.");
                    return;
                }
                if (rows.Length >1)
                {
                    YYMessageBox.Show("只能选择一行进行修改.");
                    return;
                }
                 dtSelected.Rows.Add(dataSource.Rows[rows[0]].ItemArray);

                 doUpdate(dtSelected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public virtual void doUpdate(DataTable dtSelectData){
            FormPopup detail = new FormPopup();
            if (detail.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                query();
            }
        }
        public virtual int doDelete(DataTable dtSelectData)
        {
            return 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (gridControl1.DataSource as DataTable);
                DataTable dtDel = dataSource.Clone();
                dtDel.TableName = ListDatableName;
                int[] rows = gridView1.GetSelectedRows();

                for (int i = 0; i < rows.Length; i++)
                {
                    dtDel.Rows.Add(dataSource.Rows[rows[i]].ItemArray);
                }
                if (doDelete(dtDel) > 0)
                {
                    query();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dataSource = (gridControl1.DataSource as DataTable);
                DataTable dtSelected = dataSource.Clone();
                dtSelected.TableName = "bas_user";

                DataRow row = gridView1.GetFocusedDataRow();
                dtSelected.Rows.Add(row.ItemArray);

                doUpdate(dtSelected);
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
