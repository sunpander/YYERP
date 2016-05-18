using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Plat
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        private MainLogoPanel efPanel1;
        public FormMain()
        {
            InitializeComponent();
            //efPanel1 = new MainLogoPanel();
            //this.Container.Add(efPanel1);
            //efPanel1.Dock = DockStyle.Fill;

            this.efPanel1 = new MainLogoPanel(this.components);
            this.Controls.Add(efPanel1);
            efPanel1.Dock = DockStyle.Fill;
            this.efPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.efPanel1.FireScrollEventOnMouseWheel = true;
            efPanel1.Visible = true;

            this.xtraTabbedMdiManager1.MouseUp += new MouseEventHandler(xtraTabbedMdiManager1_Event);
            this.xtraTabbedMdiManager1.SelectedPageChanged += new System.EventHandler(this.EPMDIShell_MdiChildActivate);
            this.xtraTabbedMdiManager1.PageAdded += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageAdded);
            this.xtraTabbedMdiManager1.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageRemoved);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //设置时间
            timer1.Start();
        }
 

        private void itemMainMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                String formName = e.Item.Name;
                Form frmChild = null;
                if (formName == "zhangTaoAdmin")
                {
                   frmChild = new YYERP.ERP.FormZhangtao();
                }
                else if (formName == "zhangTaoChange")
                {
                    frmChild = new YYERP.ERP.FormZhangtaoChange();
                }
                else if (formName == "userManager")
                {
                    frmChild = new FormUserManager();
                }
                else if (formName == "companyInfo")
                {
                    frmChild = new FormCompany();
                }
                frmChild.MdiParent = null;
                frmChild.MdiParent = this;
                frmChild.Visible = true;
                frmChild.Show();
            }
            catch (Exception ex)
            {
                
            }
        }



        private void xtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (this.efPanel1.Visible)
            {
                this.efPanel1.Visible = false;
            }

            //if (e.Page.MdiChild is EF.EFFormMain)
            //{
            //    ((EF.EFFormMain)e.Page.MdiChild).EFMsgInfoChanged += new EF.EFFormMain.EFLogEvent(EPMDIShell2_EFMsgInfoChanged);
            //    //修改提示信息图标事件
            //    ((EF.EFFormMain)e.Page.MdiChild).EFMsgIconChanged += new EF.EFFormMain.EFLogEvent(EPMainFrame_EFMsgIconChanged);
            //}
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            //if (e.Page.MdiChild is EF.EFFormMain)
            //{
            //    removeBarButtonEnableEnvet(e); //去掉按钮是否可用事件
            //    ((EF.EFFormMain)e.Page.MdiChild).EFMsgInfoChanged -= new EF.EFFormMain.EFLogEvent(EPMDIShell2_EFMsgInfoChanged);
            //    ((EF.EFFormMain)e.Page.MdiChild).EFMsgIconChanged -= new EF.EFFormMain.EFLogEvent(EPMainFrame_EFMsgIconChanged);
            //}

            if (xtraTabbedMdiManager1.Pages.Count == 0)
            {
                this.efPanel1.Visible = true;
            }
        }
        /// <summary>
        /// Tab页上的右键菜单
        /// </summary>
        void xtraTabbedMdiManager1_Event(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && ActiveMdiChild != null)
            //{
            //    barButtonItemClose.Visibility = BarItemVisibility.Always;
            //    barButtonItemCloseOthers.Visibility = BarItemVisibility.Always;
            //    barButtonItemFloat.Visibility = BarItemVisibility.Always;
            //    barButtonItemFull.Visibility = BarItemVisibility.Always;
            //    if (MdiChildren.Length > 1)
            //    {
            //        barButtonItemCloseOthers.Visibility = BarItemVisibility.Always;
            //    }
            //    else
            //    {
            //        barButtonItemCloseOthers.Visibility = BarItemVisibility.Never;
            //    }

            //    BaseTabHitInfo hi = xtraTabbedMdiManager1.CalcHitInfo(e.Location);
            //    if (hi.IsValid && hi.Page != null && !hi.InPageCloseButton)
            //    {
            //        favFormName = this.ActiveMdiChild.Tag.ToString();
            //        SetPopupMenu(favFormName);
            //        this.popupMenu1.ShowPopup(Control.MousePosition);
            //    }
            //}
        }

        private void EPMDIShell_MdiChildActivate(object sender, EventArgs e)
        {
            ////设置工具条是否可见
            ////this.SetNewBarVisible();
            //if (this.ActiveMdiChild != null && this.ActiveMdiChild is EF.EFFormMain)
            //{
            //    setToolBarButtonEnable();  //设置工具条按钮是否可用
            //    this.barStaticItem1.Caption = ((EF.EFFormMain)this.ActiveMdiChild).EFMsgInfo;
            //    this.Text = EC.UserConfig.Instance.CurrentCulture.ProjectFullName + "-" + ((EF.EFFormMain)this.ActiveMdiChild).Text;
            //    try
            //    {
            //        EF.EFMsgIcons iconType = ((EF.EFFormMain)this.ActiveMdiChild).EFMsgIcon;
            //        ChangeEFMsgIcon(iconType); //修改提示信息图标
            //    }
            //    catch { }
            //}
            //else
            //{
            //    this.barStaticItem1.Caption = EP.EPEP.EPEPC0000015/*就绪*/;
            //}
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime srvTimeNow = System.DateTime.Now  ;
            this.barStaticItemServerTime.Caption = srvTimeNow.ToString("yyyy-MM-dd hh:mm:ss");
        }




    }
}
