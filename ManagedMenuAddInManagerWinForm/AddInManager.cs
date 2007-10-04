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
            RefreshList();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveList();
        }

        private void RefreshList()
        {
            AddInList.Items.Clear();
            ListToListView(AddInManagerHelper.Refresh());
        }

        private void SaveList()
        {
            AddInManagerHelper.Save(ListViewToList());
        }

        private void ListToListView(List<AddIn> list)
        {
            DisableItemCheckedEvent();
            foreach (AddIn addIn in list)
            {
                AddInList.Items.Add(new ListViewItem(addIn.Name) { Checked = addIn.Enabled, Tag = addIn });   
            }
            EnableItemCheckedEvent();
        }

        private List<AddIn> ListViewToList()
        {
            List<AddIn> list = new List<AddIn>();
            foreach (ListViewItem item in AddInList.Items)
            {
                if (((AddIn)item.Tag).Changed)
                {
                    list.Add((AddIn)item.Tag);
                }
            }
            return list;
        }

        private bool m_ItemCheckedEventEnabled = false;
        
        private void DisableItemCheckedEvent()
        {
            m_ItemCheckedEventEnabled = false;
        }

        private void EnableItemCheckedEvent()
        {
            m_ItemCheckedEventEnabled = true;
        }

        private void AddInList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ((AddIn)e.Item.Tag).Enabled = e.Item.Checked;
        }

    }
}
