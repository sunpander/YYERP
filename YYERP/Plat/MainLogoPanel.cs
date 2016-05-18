using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;

namespace Plat
{
    public partial class MainLogoPanel : DevExpress.XtraEditors.PanelControl
    {
        private Image _image;
        private ColorMatrix _clrMatrix;
        private ImageAttributes _imgAttributes;
        private Rectangle _desRect;

        public MainLogoPanel()
        {
            InitializeComponent();

            Init();
        }

        public MainLogoPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Init();
        }

        public void Init()
        {
            if (this.DesignMode)
                return;

            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

            if (assembly == null)
                return;
 
            string direc = System.IO.Path.GetDirectoryName(assembly.Location);


            string  imagePath = System.IO.Path.Combine(direc, "mainLogo.jpg");
            if (System.IO.File.Exists(imagePath))
            {
                _image = Image.FromFile(imagePath);
            }
            else
            {
                Console.WriteLine(String.Format("文件{0}不存在。", imagePath));
            }
            //_image = System.IO.Path.Combine(direc, "MainIcon.png"); ;

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            _clrMatrix = new ColorMatrix();
            _clrMatrix.Matrix00 = _clrMatrix.Matrix11 = _clrMatrix.Matrix22 = 1;
            _clrMatrix.Matrix33 = 0.666f;
            // Create an ImageAttributes object
            _imgAttributes =
              new ImageAttributes();

            _imgAttributes.SetColorMatrix(_clrMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            _desRect = new Rectangle();

        }

        private void MainLogoPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            if (_image == null)
                return;

            float srcRatio = _image.Width / (float)_image.Height;
            float desRatio = this.Width / (float)this.Height;

            if (srcRatio > desRatio)
            {
                _desRect.Width = this.Width;
                _desRect.Height = (int)(this.Width / srcRatio);
                _desRect.Y = (this.Height - _desRect.Height) / 2;
            }
            else
            {
                _desRect.Height = this.Height;
                _desRect.Width = (int)(this.Height * srcRatio);
                _desRect.X = (this.Width - _desRect.Width) / 2;
            }
            //rect.Top = this.Height

            e.Graphics.DrawImage(_image, _desRect
             , 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, _imgAttributes);

        }
    }
}
