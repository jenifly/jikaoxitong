namespace ZJDZXJK
{
    partial class Loading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.dSkinPictureBox1 = new DSkin.Controls.DSkinPictureBox();
            this.SuspendLayout();
            // 
            // dSkinPictureBox1
            // 
            this.dSkinPictureBox1.Image = global::ZJDZXJK.Properties.Resources.loading;
            this.dSkinPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ZJDZXJK.Properties.Resources.loading))};
            this.dSkinPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.dSkinPictureBox1.Name = "dSkinPictureBox1";
            this.dSkinPictureBox1.Size = new System.Drawing.Size(300, 141);
            this.dSkinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.dSkinPictureBox1.TabIndex = 0;
            this.dSkinPictureBox1.Text = "dSkinPictureBox1";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(300, 141);
            this.Controls.Add(this.dSkinPictureBox1);
            this.DoubleClickMaximized = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MoveMode = DSkin.Forms.MoveModes.None;
            this.Name = "Loading";
            this.ShadowColor = System.Drawing.Color.DimGray;
            this.ShadowWidth = 4;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowShadow = true;
            this.ShowSystemButtons = false;
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.Controls.DSkinPictureBox dSkinPictureBox1;
    }
}