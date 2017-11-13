namespace ZJDZXJK.View
{
    partial class Admin_ItemView
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
            this.CoreBaseControl = new DSkin.DirectUI.DuiBaseControl();
            this.duiLabel1 = new DSkin.DirectUI.DuiLabel();
            this.SuspendLayout();
            // 
            // DBaseControl
            // 
            this.DBaseControl.BackColor = System.Drawing.Color.Transparent;
            this.DBaseControl.DUIControls.Add(this.CoreBaseControl);
            this.DBaseControl.Size = new System.Drawing.Size(200, 46);
            this.DBaseControl.Text = "##";
            // 
            // CoreBaseControl
            // 
            this.CoreBaseControl.AutoSize = false;
            this.CoreBaseControl.BackgroundRender.BorderWidth = 0;
            this.CoreBaseControl.Controls.Add(this.duiLabel1);
            this.CoreBaseControl.DesignModeCanMove = false;
            this.CoreBaseControl.DesignModeCanResize = false;
            this.CoreBaseControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CoreBaseControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CoreBaseControl.Location = new System.Drawing.Point(0, 0);
            this.CoreBaseControl.Name = "CoreBaseControl";
            this.CoreBaseControl.Size = new System.Drawing.Size(200, 46);
            // 
            // duiLabel1
            // 
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(0, 0);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(200, 46);
            this.duiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Admin_ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "Admin_ItemView";
            this.Size = new System.Drawing.Size(200, 80);
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiBaseControl CoreBaseControl;
        private DSkin.DirectUI.DuiLabel duiLabel1;

    }
}