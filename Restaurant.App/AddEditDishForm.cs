using Restaurant.App.Data;
using Restaurant.App.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Restaurant.App
{
    public delegate void ComboBoxAction(object dataSource);
    public partial class AddEditDishForm : Form
    {
        private readonly DishesManager manager;
        private readonly Dish dish;

        public AddEditDishForm(Dish dish = null)
        {
            manager = new DishesManager();
            this.dish = dish ?? new Dish();
            InitializeComponent();
        }

        private void MapDishToFields()
        {
            textBoxName.Text = dish.Name;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
            upDownPortion.Value = dish.ServingSize ?? 0;
            upDownCost.Value = dish.Cost ?? 0;
            upDownTime.Value = dish.CookingTime.HasValue
                ? (int)dish.CookingTime.Value.TotalMinutes
                : 0;
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IEnumerable<Ingredient> ingredients
                = await manager.GetIngredientsAsync();
            SetComboBoxDataSource(ingredients);
        }

        private void SetComboBoxDataSource(object dataSource)
        {
            if (comboBoxIngredient.InvokeRequired)
            {
                ComboBoxAction action = new ComboBoxAction(SetComboBoxDataSource);
                this.Invoke(action, new object[] { dataSource });
            }
            else
            {
                comboBoxIngredient.DataSource = dataSource;
                comboBoxIngredient.DisplayMember = "Name";
                comboBoxIngredient.ValueMember = "Id";
                comboBoxIngredient.SelectedIndex = 0;
                if (dish.Id.HasValue && dish.IngredientId.HasValue)
                {
                    comboBoxIngredient.SelectedValue = dish.IngredientId.Value;
                }
            }
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            if (dish.Id.HasValue)
            {
                bool result = await manager.UpdateDishAsync(
                        dish.Id.Value,
                        (int)comboBoxIngredient.SelectedValue,
                        textBoxName.Text,
                        upDownPortion.Value == 0 ? null : (int?)upDownPortion.Value,
                        upDownCost.Value == 0 ? null : (int?)upDownCost.Value,
                        upDownTime.Value == 0 ? null : (int?)upDownTime.Value
                    );

                if (result)
                {
                    MessageBox.Show("Блюдо успешно отредактировано.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так...");
                    return;
                }
            }
            else
            {
                bool result = await manager.CreateDishAsync(
                        (int)comboBoxIngredient.SelectedValue,
                        textBoxName.Text,
                        upDownPortion.Value == 0 ? null : (int?)upDownPortion.Value,
                        upDownCost.Value == 0 ? null : (int?)upDownCost.Value,
                        upDownTime.Value == 0 ? null : (int?)upDownTime.Value
                    );

                if (result)
                {
                    MessageBox.Show("Блюдо успешно добавлено в меню.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так...");
                    return;
                }
            }
        }

        private void AddEditDishForm_Load(object sender, System.EventArgs e)
        {
            MapDishToFields();
        }
    }
}
