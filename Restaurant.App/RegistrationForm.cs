using Restaurant.App.Data;
using System.Windows.Forms;

namespace Restaurant.App
{
    public partial class RegistrationForm : Form
    {
        private readonly AuthManager auth;
        public RegistrationForm()
        {
            auth = new AuthManager();
            InitializeComponent();
        }

        private async void button2_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            bool registrationResult =
                await auth.RegisterUserAsync(textBoxLogin.Text, textBoxPassword.Text);
            if (registrationResult)
            {
                MessageBox.Show("Вы успешно зарегистрировались!");
                Close();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.");
            }
        }
    }
}
