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
    public partial class FormCardCategoryDetail : FormPopup
    {
        public string brandId = "";
        public FormCardCategoryDetail()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.cardIdTextBox.Text))
                {
                    if (this.lookUpEditBrand.EditValue == null)
                    {
                        YYMessageBox.Show("品牌不能为空.");
                        return;
                    }
                    //新增
                     ServiceCardCategory.insert(this.lookUpEditBrand.EditValue.ToString(),this.cardNameTextBox.Text.Trim(),this.hideInSalesTextBox.Text);
                }
                else
                {
                    //新增
                    //ServiceCardBrand.update(this.brandIdTextBox.Text, this.brandNameTextBox.Text.Trim());
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }

        private void FormCardCategoryDetail_Load(object sender, EventArgs e)
        {
           
           
        }

        private void FormCardCategoryDetail_Shown(object sender, EventArgs e)
        {
            DataTable dt = ServiceCardBrand.queryAll();
            lookUpEditBrand.Properties.DataSource = dt;
            lookUpEditBrand.Properties.DisplayMember = "BrandName";
            lookUpEditBrand.Properties.ValueMember = "BrandId";

            lookUpEditBrand.EditValue = brandId;
            lookUpEditBrand.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
        }
    }
}
