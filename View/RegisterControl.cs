using System;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class RegisterControl : UserControl
    {
        // 13 argumentów: FirstName, LastName, Password, Email, Phone, Username, BirthDate, Province, City, Street, House, Apt, Zip
        public event Action<string, string, string, string, string, string, DateTime, string, string, string, string, string, string> OnRegisterAttempt;
        public event Action OnReturnToMain;

        public RegisterControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                string zipCombined = $"{textBox12.Text}-{textBox13.Text}";

                OnRegisterAttempt?.Invoke(
                    textBox1.Text, // FirstName
                    textBox2.Text, // LastName
                    textBox4.Text, // Password
                    textBox5.Text, // Email
                    textBox6.Text, // Phone
                    textBox3.Text, // Username (Poprawiono z dateTimePicker1.Text)
                    dateTimePicker1.Value, // BirthDate
                    textBox7.Text, // Province
                    textBox8.Text, // City
                    textBox9.Text, // Street
                    textBox10.Text, // HouseNum
                    textBox11.Text, // AptNum
                    zipCombined     // Zip
                );
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.");
            }
        }

        private void textBox12_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBox13_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnReturnToMain?.Invoke();
        }
    }
}