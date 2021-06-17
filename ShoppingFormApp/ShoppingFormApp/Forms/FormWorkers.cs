using Microsoft.EntityFrameworkCore;
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

namespace ShoppingFormApp.Forms
{
    public partial class FormWorkers : Form
    {
        ShoppingFormContext DB = new();
        Worker selectedWorker = null;

        public FormWorkers()
        {
            InitializeComponent();
        }

        private void GetWorkersListGrid()
        {
            dtgWorkers.DataSource = DB.Workers.Select(wk => new
            {
                wk.Id,
                Fullname = wk.Fullname,
                Email = wk.Email,
                Password = wk.Password,
            }).ToList();

            dtgWorkers.Columns[0].Visible = false;
        }

        private void FormWorkers_Load(object sender, EventArgs e)
        {
            GetWorkersListGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var question = MessageBox.Show("Do you really want to delete this worker?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (question == DialogResult.Yes)
            {
                DB.Workers.Remove(selectedWorker);
                DB.SaveChanges();
                GetWorkersListGrid();

                MessageBox.Show("The worker is deleted!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgWorkers_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int workerId = (int)dtgWorkers.Rows[e.RowIndex].Cells[0].Value;

            selectedWorker = DB.Workers.Include(x => x.Orders).FirstOrDefault(x => x.Id == workerId);
        }
    }
}
