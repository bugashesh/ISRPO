using System;
using System.Windows.Forms;

namespace Restaurant.App
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEditStaffMemberForm form = new AddEditStaffMemberForm();
            form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffTableForm form = new StaffTableForm();
            form.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddEditDishForm form = new AddEditDishForm();
            form.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DishesTableForm form = new DishesTableForm();
            form.ShowDialog(this);
        }
    }
}
