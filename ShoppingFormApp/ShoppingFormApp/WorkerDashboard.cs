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
    public partial class WorkerDashboard : Form
    {
        ShoppingFormContext DB = new();
        Product selectedProduct;
        Worker activeWorker;
        public WorkerDashboard(Worker worker)
        {
            activeWorker = worker;
            InitializeComponent();
        }

        public void FillCatecoryCombo()
        {
            cmbCategories.Items.AddRange(DB.Categories.Select(x => x.Name).ToArray());
        }

        private void LoadDataGrid()
        {
            dtgTable.DataSource = DB.Products.Select(x => new
            {
                x.Name,
                Category = x.Category.Name,
                x.Price,
                x.Quantity,
                x.ProductionDate,
            }).ToList();
        }

        private void WorkerDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + activeWorker.Fullname + "!";
            FillCatecoryCombo();
            LoadDataGrid();
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProducts.Items.Clear();
            int categoryId = DB.Categories.FirstOrDefault(x => x.Name == cmbCategories.Text).Id;
            cmbProducts.Items.AddRange(DB.Products.Where(x => x.CategoryId == categoryId).Select(x => x.Name).ToArray());
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProduct = DB.Products.FirstOrDefault(x => x.Name == cmbProducts.Text);
            lblTotalPrice.Text = selectedProduct.Price + " AZN";
            lblTotalPrice.Visible = true;
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            numQuantity.Maximum = selectedProduct.Quantity;
            lblTotalPrice.Text = (decimal)(selectedProduct.Price * numQuantity.Value) + " AZN";
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            Order order = new()
            {
                WorkerId = activeWorker.Id,
                ProductId = selectedProduct.Id,
                Date = DateTime.Now,
                Count = (int)numQuantity.Value,
                TotalPrice = selectedProduct.Price * numQuantity.Value,
            };

            DB.Orders.Add(order);
            DB.SaveChanges();

            MessageBox.Show("The operation completed successfully!", "Success");
        }
    }
}
