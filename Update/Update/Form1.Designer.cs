namespace Update
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dSkinPanel1 = new DSkin.Controls.DSkinPanel();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.dSkinPictureBox1 = new DSkin.Controls.DSkinPictureBox();
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel2 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel3 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel4 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel5 = new DSkin.Controls.DSkinLabel();
            this.dSkinPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dSkinPanel1
            // 
            this.dSkinPanel1.BackColor = System.Drawing.Color.Azure;
            this.dSkinPanel1.Borders.AllColor = System.Drawing.Color.Gray;
            this.dSkinPanel1.Borders.BottomColor = System.Drawing.Color.Gray;
            this.dSkinPanel1.Borders.LeftColor = System.Drawing.Color.Gray;
            this.dSkinPanel1.Borders.RightColor = System.Drawing.Color.Gray;
            this.dSkinPanel1.Borders.TopColor = System.Drawing.Color.Gray;
            this.dSkinPanel1.Controls.Add(this.dSkinLabel5);
            this.dSkinPanel1.Location = new System.Drawing.Point(35, 126);
            this.dSkinPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dSkinPanel1.Name = "dSkinPanel1";
            this.dSkinPanel1.RightBottom = ((System.Drawing.Image)(resources.GetObject("dSkinPanel1.RightBottom")));
            this.dSkinPanel1.Size = new System.Drawing.Size(480, 24);
            this.dSkinPanel1.TabIndex = 0;
            this.dSkinPanel1.Text = "dSkinPanel1";
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.AdaptImage = true;
            this.dSkinButton1.BaseColor = System.Drawing.Color.DarkCyan;
            this.dSkinButton1.ButtonBorderColor = System.Drawing.Color.Silver;
            this.dSkinButton1.ButtonBorderWidth = 0;
            this.dSkinButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinButton1.ForeColor = System.Drawing.Color.White;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.IsPureColor = false;
            this.dSkinButton1.Location = new System.Drawing.Point(155, 199);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 12;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(240, 35);
            this.dSkinButton1.TabIndex = 23;
            this.dSkinButton1.Text = "开始下载";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            // 
            // dSkinPictureBox1
            // 
            this.dSkinPictureBox1.Image = global::Update.Properties.Resources.icon;
            this.dSkinPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::Update.Properties.Resources.icon))};
            this.dSkinPictureBox1.Location = new System.Drawing.Point(6, 5);
            this.dSkinPictureBox1.Name = "dSkinPictureBox1";
            this.dSkinPictureBox1.Size = new System.Drawing.Size(24, 24);
            this.dSkinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dSkinPictureBox1.TabIndex = 31;
            this.dSkinPictureBox1.Text = "dSkinPictureBox1";
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel1.Location = new System.Drawing.Point(37, 7);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(198, 21);
            this.dSkinLabel1.TabIndex = 30;
            this.dSkinLabel1.Text = "株机段2017岗前培训答题系统";
            // 
            // dSkinLabel2
            // 
            this.dSkinLabel2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel2.Location = new System.Drawing.Point(228, 48);
            this.dSkinLabel2.Name = "dSkinLabel2";
            this.dSkinLabel2.Size = new System.Drawing.Size(94, 31);
            this.dSkinLabel2.TabIndex = 32;
            this.dSkinLabel2.Text = "系统更新";
            // 
            // dSkinLabel3
            // 
            this.dSkinLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel3.Location = new System.Drawing.Point(37, 102);
            this.dSkinLabel3.Name = "dSkinLabel3";
            this.dSkinLabel3.Size = new System.Drawing.Size(88, 21);
            this.dSkinLabel3.TabIndex = 33;
            this.dSkinLabel3.Text = "系统更新中...";
            // 
            // dSkinLabel4
            // 
            this.dSkinLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel4.Location = new System.Drawing.Point(248, 153);
            this.dSkinLabel4.Name = "dSkinLabel4";
            this.dSkinLabel4.Size = new System.Drawing.Size(55, 21);
            this.dSkinLabel4.TabIndex = 34;
            this.dSkinLabel4.Text = "00.00%";
            // 
            // dSkinLabel5
            // 
            this.dSkinLabel5.AutoSize = false;
            this.dSkinLabel5.BackColor = System.Drawing.Color.CadetBlue;
            this.dSkinLabel5.Location = new System.Drawing.Point(0, 0);
            this.dSkinLabel5.Name = "dSkinLabel5";
            this.dSkinLabel5.Size = new System.Drawing.Size(1, 24);
            this.dSkinLabel5.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(550, 258);
            this.Controls.Add(this.dSkinLabel4);
            this.Controls.Add(this.dSkinLabel3);
            this.Controls.Add(this.dSkinLabel2);
            this.Controls.Add(this.dSkinPictureBox1);
            this.Controls.Add(this.dSkinLabel1);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.dSkinPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MoveMode = DSkin.Forms.MoveModes.Title;
            this.Name = "Form1";
            this.ShadowColor = System.Drawing.Color.DimGray;
            this.ShadowWidth = 4;
            this.ShowShadow = true;
            this.Text = "";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.dSkinPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinPanel dSkinPanel1;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.Controls.DSkinPictureBox dSkinPictureBox1;
        private DSkin.Controls.DSkinLabel dSkinLabel1;
        private DSkin.Controls.DSkinLabel dSkinLabel2;
        private DSkin.Controls.DSkinLabel dSkinLabel3;
        private DSkin.Controls.DSkinLabel dSkinLabel4;
        private DSkin.Controls.DSkinLabel dSkinLabel5;
    }
}

