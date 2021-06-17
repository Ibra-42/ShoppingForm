using ShoppingFormApp.Helpers;
using ShoppingFormApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingFormApp
{
    public partial class RegisterForm : Form
    {

        ShoppingFormContext DB = new();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string[] enteredData = new string[] { fullname, email, password, confirmPassword };

            if (Utilities.IsFull(enteredData))
            {
                if (password == confirmPassword)
                {
                    lblError.Visible = false;

                    Worker myWorker = new()
                    {
                        Fullname = fullname,
                        Email = email,
                        Password = Utilities.HashPassword(password)
                    };

                    DB.Workers.Add(myWorker);
                    DB.SaveChanges();

                    MessageBox.Show("Worker registered successfully.");
                }
                else
                {
                    lblError.Text = "You entered two different passwords. Please try again.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please, fill in all the fields!";
                lblError.Visible = true;
            }
        }
    }
}
