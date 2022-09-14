using Restaurant.App.Data;
using Restaurant.App.Data.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Restaurant.App
{
    public partial class DishesTableForm : Form
    {
        private readonly DishesManager manager;
        private BindingList<Dish> dishes;

        public DishesTableForm()
        {
            manager = new DishesManager();
            InitializeComponent();
        }

        private void DishesTable_Load(object sender, EventArgs e)
        {
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var list = await manager.GetDishesAsync();
            dishes = new BindingList<Dish>(list);
            SetGridDataSource(dishes);
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
                MessageBox.Show("Пожалуйста, выберите одно блюдо для удаления.");
                return;
            }

            int selectedIndex = grid.Rows.IndexOf(grid.SelectedRows[0]);
            bool result = await manager.DeleteDishAsync(dishes[selectedIndex].Id.Value);
            if (result)
            {
                dishes.RemoveAt(selectedIndex);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count != 1)
            {
                MessageBox.Show("Пожалуйста, выберите одно блюдо для редактирования.");
                return;
            }

            int selectedIndex = grid.Rows.IndexOf(grid.SelectedRows[0]);
            AddEditDishForm form = new AddEditDishForm(dishes[selectedIndex]);
            form.ShowDialog();
        }
    }
}
