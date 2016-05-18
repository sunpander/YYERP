using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YYERP.Common.UI;
using YYERP.CUS.service;

namespace YYERP.CUS
{
    public partial class FormCardBrandDetail : FormPopup
    {
    
        public FormCardBrandDetail()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.brandIdTextBox.Text))
                {
                    //新增
                    ServiceCardBrand.insert(this.brandNameTextBox.Text.Trim());
                }
                else
                {
                    //新增
                    ServiceCardBrand.update(this.brandIdTextBox.Text, this.brandNameTextBox.Text.Trim());
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }
 

        private void FormCardBrandDetail_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“yyerpDataSet.bas_brand”中。您可以根据需要移动或删除它。
     
 

        }

        private void bas_brandBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
     

        }
    }
}
