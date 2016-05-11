namespace YYERP.DbTable
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataSetBase1 = new YYERP.DbTable.DataSetBase();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.bas_brandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.brandIdTextBox = new System.Windows.Forms.TextBox();
            this.companyIdTextBox = new System.Windows.Forms.TextBox();
            this.orderNumTextBox = new System.Windows.Forms.TextBox();
            this.hidenTextBox = new System.Windows.Forms.TextBox();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBase1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bas_brandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSetBase1
            // 
            this.dataSetBase1.DataSetName = "DataSetBase";
            this.dataSetBase1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.brandIdTextBox);
            this.layoutControl1.Controls.Add(this.companyIdTextBox);
            this.layoutControl1.Controls.Add(this.orderNumTextBox);
            this.layoutControl1.Controls.Add(this.hidenTextBox);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(558, 367);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(558, 367);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // bas_brandBindingSource
            // 
            this.bas_brandBindingSource.DataMember = "bas_brand";
            this.bas_brandBindingSource.DataSource = this.dataSetBase1;
            // 
            // brandIdTextBox
            // 
            this.brandIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bas_brandBindingSource, "BrandId", true));
            this.brandIdTextBox.Location = new System.Drawing.Point(85, 12);
            this.brandIdTextBox.Name = "brandIdTextBox";
            this.brandIdTextBox.Size = new System.Drawing.Size(461, 20);
            this.brandIdTextBox.TabIndex = 5;
            // 
            // companyIdTextBox
            // 
            this.companyIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bas_brandBindingSource, "CompanyId", true));
            this.companyIdTextBox.Location = new System.Drawing.Point(85, 36);
            this.companyIdTextBox.Name = "companyIdTextBox";
            this.companyIdTextBox.Size = new System.Drawing.Size(461, 20);
            this.companyIdTextBox.TabIndex = 7;
            // 
            // orderNumTextBox
            // 
            this.orderNumTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bas_brandBindingSource, "OrderNum", true));
            this.orderNumTextBox.Location = new System.Drawing.Point(85, 60);
            this.orderNumTextBox.Name = "orderNumTextBox";
            this.orderNumTextBox.Size = new System.Drawing.Size(461, 20);
            this.orderNumTextBox.TabIndex = 9;
            // 
            // hidenTextBox
            // 
            this.hidenTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bas_brandBindingSource, "Hiden", true));
            this.hidenTextBox.Location = new System.Drawing.Point(85, 84);
            this.hidenTextBox.Name = "hidenTextBox";
            this.hidenTextBox.Size = new System.Drawing.Size(461, 20);
            this.hidenTextBox.TabIndex = 11;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.brandIdTextBox;
            this.layoutControlItem2.CustomizationFormText = "Brand Id:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(538, 24);
            this.layoutControlItem2.Text = "Brand Id:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.companyIdTextBox;
            this.layoutControlItem4.CustomizationFormText = "Company Id:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(538, 24);
            this.layoutControlItem4.Text = "Company Id:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.orderNumTextBox;
            this.layoutControlItem6.CustomizationFormText = "Order Num:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(538, 24);
            this.layoutControlItem6.Text = "Order Num:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.hidenTextBox;
            this.layoutControlItem8.CustomizationFormText = "Hiden:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(538, 275);
            this.layoutControlItem8.Text = "Hiden:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(69, 14);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 367);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBase1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bas_brandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataSetBase dataSetBase1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.TextBox brandIdTextBox;
        private System.Windows.Forms.BindingSource bas_brandBindingSource;
        private System.Windows.Forms.TextBox companyIdTextBox;
        private System.Windows.Forms.TextBox orderNumTextBox;
        private System.Windows.Forms.TextBox hidenTextBox;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}

