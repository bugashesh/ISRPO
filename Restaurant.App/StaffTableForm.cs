using Restaurant.App.Data;
using Restaurant.App.Data.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Restaurant.App
{
    public delegate void DataSourceAction(object dataSource);
    public partial class StaffTableForm : Form
    {
        private readonly StaffMembersManager manager;
        private BindingList<StaffMember> members;

        public StaffTableForm()
        {
            manager = new StaffMembersManager();
            InitializeComponent();
        }

        private void StaffTableForm_Load(object sender, EventArgs e)
        {
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var list = await manager.GetStaffMembersAsync();
            members = new BindingList<StaffMember>(list);
            SetGridDataSource(members);
        }

        private void SetGridDataSource(object dataSource)
        {
            if (grid.InvokeRequired)
            {
                DataSourceAction action = new DataSourceAction(SetGridDataSource);
                this.Invoke(action, new object[] { dataSource });
            }
            else
            {
                grid.DataSource = dataSource;
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private async void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите одного сотрудника для удаления.");
                return;
            }

            int selectedIndex = grid.Rows.IndexOf(grid.SelectedRows[0]);
            bool result = await manager.DeleteStaffMemberAsync(members[selectedIndex].Id.Value);
            if (result)
            {
                members.RemoveAt(selectedIndex);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите одного сотрудника для редактирования.");
                return;
            }

            int selectedIndex = grid.Rows.IndexOf(grid.SelectedRows[0]);
            AddEditStaffMemberForm form = new AddEditStaffMemberForm(members[selectedIndex]);
            form.ShowDialog();
        }
    }
}
