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
    public partial class LoginForm : Form
    {
        ShoppingFormContext DB = new();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (Utilities.IsFull(new string[] { email, password }))
            {
                if (email == "admin" && password == "admin")
                {
                    AdminForm AF = new();
                    AF.ShowDialog();
                }
                else
                {
                    Worker selectedWorker = DB.Workers.FirstOrDefault(x => x.Email == email);

                    if (selectedWorker != null)
                    {
                        if (selectedWorker.Password == Utilities.HashPassword(password))
                        {
                            lblError.Visible = false;

                            WorkerDashboard WD = new(selectedWorker);
                            WD.ShowDialog();
                        }
                        else
                        {
                            lblError.Text = "Password isn't correct.";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Email isn't correct.";
                        lblError.Visible = true;
                    }
                }

            }
            else
            {
                lblError.Text = "Please, fill up all the fields!";
                lblError.Visible = true;
            }
        }
    }
}
