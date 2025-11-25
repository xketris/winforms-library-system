using System;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class LoginControl : UserControl
    {
        public event Action<string, string> OnLoginAttempt;
        public event Action OnReturnToMain;

        public LoginControl()
        {
            InitializeComponent();
        }

        // Login Button
        private void button1_Click(object sender, EventArgs e)
        {
            OnLoginAttempt?.Invoke(textBox1.Text, textBox2.Text);
        }

        // Return Button
        private void button2_Click(object sender, EventArgs e)
        {
            OnReturnToMain?.Invoke();
        }
    }
}