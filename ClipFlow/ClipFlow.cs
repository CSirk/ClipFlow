using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ClipFlow
{
    public partial class ClipFlow : Form
    {
        public ClipFlow()
        {
            InitializeComponent();
        }

        public class MyApplicationContext : ApplicationContext
        {
            NotifyIcon notifyIcon;

            public MyApplicationContext()
            {
                notifyIcon = new NotifyIcon();
                notifyIcon.Click += notifyIcon_SingleClick;
                notifyIcon.ContextMenu = new ContextMenu();
                notifyIcon.Icon = new Icon(@"clipflow.ico");

                notifyIcon.Visible = true;
            }


            public void notifyIcon_SingleClick(object sender, EventArgs e)
            {
                MouseEventArgs mouse = (MouseEventArgs) e;

                if(mouse.Button == MouseButtons.Left)
                {
                    if (!String.IsNullOrEmpty(Clipboard.GetText()))
                    {
                        var menuItem = new MenuItem();
                        menuItem.Text = Clipboard.GetText();
                        menuItem.Click += new System.EventHandler(this.menuItem_SingleClick);
                        notifyIcon.ContextMenu.MenuItems.Add(menuItem);
                    }
                }


            }

            public void menuItem_SingleClick(object sender, EventArgs e)
            {
                var menuItem = (MenuItem)sender;
                var index = menuItem.Index;

                if(Control.ModifierKeys == Keys.Control)
                {
                    this.notifyIcon.ContextMenu.MenuItems.RemoveAt(index);
                }
                else if(Control.ModifierKeys == Keys.Shift)
                {
                    Application.Exit();
                }
                else
                {
                    Clipboard.SetText(menuItem.Text);
                }
            }
        }
    }
}
