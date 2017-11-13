using DSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZJDZXJK.View
{
    public partial class Admin_ItemView : DSkinListItem
    {
        public Admin_ItemView()
        {
            InitializeComponent();
            DBaseControl.InnerDuiControl.MouseEnter += new EventHandler<MouseEventArgs>(Control_MouseEnter);
            DBaseControl.InnerDuiControl.MouseLeave += new EventHandler(Control_MouseLeave);
            DBaseControl.InnerDuiControl.TextChanged += new EventHandler(this.Control_TextChanged);

        }

        public void ItemLoad(string name)
        {
            duiLabel1.Text = name;
        }
        private void Control_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!DBaseControl.InnerDuiControl.Text.Equals("0"))
                CoreBaseControl.BackColor = Color.FromArgb(20, 0, 0, 0);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (!DBaseControl.InnerDuiControl.Text.Equals("0"))
                CoreBaseControl.BackColor = Color.Transparent;
        }
        private void Control_TextChanged(object sender, EventArgs e)
        {
            if(DBaseControl.InnerDuiControl.Text.Equals("0"))
               CoreBaseControl.BackColor = Color.Transparent;
        }
    }
}
