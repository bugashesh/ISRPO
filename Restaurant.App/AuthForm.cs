using Restaurant.App.Data;
using System;
using System.Windows.Forms;

namespace Restaurant.App
{
    public partial class AuthForm : Form
    {
        private readonly AuthManager auth;

        public AuthForm()
        {
            auth = new AuthManager();
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new RegistrationForm();
            form.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            bool authResult = await auth.AuthorizeUserAsync(textBoxLogin.Text, textBoxPassword.Text);
            if (authResult)
            {
                MenuForm form = new MenuForm();
                form.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль.");
            }
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseManager.Instance.Dispose();
        }
    }
}
