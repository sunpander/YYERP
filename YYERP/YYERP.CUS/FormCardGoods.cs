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
    public partial class FormCardGoods : FormBase
    {
        public FormCardGoods()
        {
            InitializeComponent();
        }

        public void queryBrand()
        {
            
            gridCardBrand.DataSource = service.ServiceCardBrand.queryAll();
        }

        private void itemAddBrand_Click(object sender, EventArgs e)
        {
            try
            {
                FormCardBrandDetail frm = new FormCardBrandDetail();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    queryBrand();
                }
            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }

        private void itemUpdBrand_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }

        private void itemDelBrand_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }

        private void FormCardGoods_Load(object sender, EventArgs e)
        {
            queryBrand();
        }

        private void gridViewCardBrand_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
             queryCardCategory(  gridViewCardBrand.GetRowCellValue(e.RowHandle, "BrandId").ToString());
        }

        private void queryCardCategory(string brandId)
        {
            gridCardCategory.DataSource = ServiceCardCategory.queryByCol("brandId", brandId);
        }

        private void toolStripMenuItemAddCard_Click(object sender, EventArgs e)
        {
            try
            {
                string selBrandId = gridViewCardBrand.GetFocusedRowCellValue("BrandId").ToString();

                FormCardCategoryDetail frm = new FormCardCategoryDetail();
                frm.brandId = selBrandId;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    queryCardCategory(selBrandId);
                }
            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }

        private void gridViewCardCategory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            queryCardValue(gridViewCardCategory.GetRowCellValue(e.RowHandle, "CardId").ToString());
        }

        private void queryCardValue(string CardId)
        {
            gridCardValue.DataSource = ServiceCardValue.queryByCol("CardId", CardId);
            
        }

        private void toolStripMenuItemAddValue_Click(object sender, EventArgs e)
        {
            try
            {
                string selCardId = gridViewCardCategory.GetFocusedRowCellValue("CardId").ToString();

                FormCardValue frm = new FormCardValue();
                //  frm.brandId = selCardId;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    queryCardValue(selCardId);
                }
            }
            catch (Exception ex)
            {
                YYMessageBox.Show(ex);
            }
        }
    }
}
