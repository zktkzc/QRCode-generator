using QRCoder;

namespace 二维码生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 获取输入的网址
            string url = this.textBox1.Text.Trim();

            CreateQRCode(url, "");
            FileHelper.WriteLog("网址：" + this.textBox1.Text.Trim() + "的二维码创建成功");
        }

        public void CreateQRCode(string url, string filePath)
        {
            if (url != "")
            {
                // 生成二维码
                string strCode = url;
                // 创建二维码对象
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);

                // 通过qRCode创建出一个二维码图片
                Bitmap qrCodeImage = qRCode.GetGraphic(5, Color.Black, Color.White, !string.IsNullOrEmpty(filePath) ? new Bitmap(filePath) : null, 15, 6, true);
                // 将bitmap类型的对象转成图片保存起来
                string path = $"{Directory.GetCurrentDirectory()}/Images";
                if (!Directory.Exists(path))
                {
                    // 文件夹不存在
                    Directory.CreateDirectory(path);
                }
                // 保存图片
                string fileName = path + $"/{DateTime.Now:ffff}qrcode.png";
                qrCodeImage.Save(fileName);
                // 在控件上显示图片
                this.pictureBox1.Image = Image.FromFile(fileName);
                // 设置图片平铺
                this.pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                MessageBox.Show("请输入网址！");
                FileHelper.WriteLog("二维码创建失败，为输入网址");
            }
        }

        /// <summary>
        /// 上传Logo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // 设置打开对话框的初始目录，默认路径为exe运行文件所在的目录
            ofd.InitialDirectory = Application.StartupPath;
            // 设置打开对话框的标题
            ofd.Title = "请选择要打开的文件：";
            // 设置打开对话框只能单选
            ofd.Multiselect = false;
            // 设置对话框打开的文件类型
            ofd.Filter = "图片文件|*.jpg|所有文件|*.*";
            // 设置文件对话框当前选定的筛选器的索引
            ofd.FilterIndex = 2;
            // 设置对话框是否记忆之前打开的目录
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件完整路径
                string filePath = ofd.FileName;
                CreateQRCode(this.textBox1.Text.Trim(), filePath);
                FileHelper.WriteLog("网址：" + this.textBox1.Text.Trim() + "带logo的二维码创建成功");
            }
        }
    }
}