using Restaurant.App.Data;
using Restaurant.App.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Restaurant.App
{
    public partial class AddEditStaffMemberForm : Form
    {
        private readonly StaffMember member;
        private readonly StaffMembersManager manager;
        public AddEditStaffMemberForm(StaffMember member = null)
        {
            this.member = member ?? new StaffMember();
            manager = new StaffMembersManager();
            InitializeComponent();
        }

        private void MapMemberToFields()
        {
            textBoxFullName.Text = member.FullName;
            backgroundWorker.DoWork += FillPositionsComboBox;
            backgroundWorker.RunWorkerAsync();
            upDownPassport.Value = member.Passport ?? 0;
            upDownAge.Value = member.Age ?? 0;
            upDownFlat.Value = member.Flat ?? 0;
            textBoxCity.Text = member.City;
            textBoxStreet.Text = member.Street;
            textBoxHouse.Text = member.House;
        }

        private async void FillPositionsComboBox(object sender, DoWorkEventArgs e)
        {
            IEnumerable<Position> positions = await manager.GetPositionsAsync();
            SetComboBoxDataSource(positions);
        }

        private void SetComboBoxDataSource(object dataSource)
        {
            if (comboBoxPosition.InvokeRequired)
            {
                ComboBoxAction action = new ComboBoxAction(SetComboBoxDataSource);
                this.Invoke(action, new object[] { dataSource });
            }
            else
            {
                comboBoxPosition.DataSource = dataSource;
                comboBoxPosition.DisplayMember = "Name";
                comboBoxPosition.ValueMember = "Id";
                comboBoxPosition.SelectedIndex = 0;
                if (member.Id.HasValue && member.PositionId.HasValue)
                {
                    comboBoxPosition.SelectedValue = member.PositionId.Value;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (member.Id.HasValue)
            {
                bool result = await manager.EditStaffMemberAsync(
                    member.Id.Value,
                    (int)comboBoxPosition.SelectedValue,
                    textBoxFullName.Text,
                    upDownPassport.Value == 0 ? null : (int?)upDownPassport.Value,
                    textBoxCity.Text,
                    textBoxStreet.Text,
                    textBoxHouse.Text,
                    upDownFlat.Value == 0 ? null : (int?)upDownFlat.Value,
                    upDownAge.Value == 0 ? null : (int?)upDownAge.Value
                );

                if (result)
                {
                    MessageBox.Show("Сотрудник успешно изменён!");
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
                bool result = await manager.AddStaffMemberAsync(
                    (int?)comboBoxPosition.SelectedValue,
                    textBoxFullName.Text,
                    upDownPassport.Value == 0 ? null : (int?)upDownPassport.Value,
                    textBoxCity.Text,
                    textBoxStreet.Text,
                    textBoxHouse.Text,
                    upDownFlat.Value == 0 ? null : (int?)upDownFlat.Value,
                    upDownAge.Value == 0 ? null : (int?)upDownAge.Value
                    );

                if (result)
                {
                    MessageBox.Show("Сотрудник успешно добавлен!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так...");
                    return;
                }
            }
        }

        private void AddEditStaffMemberForm_Load(object sender, EventArgs e)
        {
            MapMemberToFields();
        }
    }
}
