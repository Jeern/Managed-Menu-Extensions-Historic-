using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagedMenuAddInManagerWinForm
{
    public partial class AddInManager : Form
    {
        public AddInManager()
        {
            InitializeComponent();
            ListViewItem item = new ListViewItem("Hej");
            AddInList.Items.Add(item); 
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            AddInManagerHelper.Refresh();
        }
    }
}
