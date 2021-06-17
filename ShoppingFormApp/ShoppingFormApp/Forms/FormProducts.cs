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
    public partial class FormProducts : Form
    {
        ShoppingFormContext DB = new();
        Product selectedProduct = null;

        public FormProducts()
        {
            InitializeComponent();
        }

        private void GetCategoriesCombo()
        {
            cmbCategory.Items.AddRange(DB.Categories.Select(x => x.Name).ToArray());
        }

        private void GetProductsListGrid()
        {
            dtgProducts.DataSource = DB.Products.Select(pr => new
            {
                pr.Id,
                Product = pr.Name,
                Category = pr.CategoryId,
                Price = pr.Price,
                Quantity = pr.Quantity,
            }).ToList();

            dtgProducts.Columns[0].Visible = false;
        }

        public void HideOrShowAddBtn(string b)
        {
            if (b == "add")
            {
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProduct.Text;
            string category = cmbCategory.Text;
            decimal price = numPrice.Value;
            int quantity = (int)numQuantity.Value;

            if (!string.IsNullOrWhiteSpace(productName) && !string.IsNullOrWhiteSpace(category))
            {
                Product newProduct = new()
                {
                    Name = productName,
                    CategoryId = DB.Categories.FirstOrDefault(x => x.Name == category).Id,
                    Price = price,
                    Quantity = quantity
                };

                DB.Products.Add(newProduct);
                DB.SaveChanges();

                MessageBox.Show("Product added successfully!");
                txtProduct.Text = "";
                cmbCategory.Items.Clear();
                numPrice.Value = 0;
                numQuantity.Value = 1;
                GetCategoriesCombo();
                GetProductsListGrid();
            }
            else
            {
                MessageBox.Show("Incorrect product or category name!");
            }
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {
            GetCategoriesCombo();
            GetProductsListGrid();
        }

        private void dtgProducts_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int productId = (int)dtgProducts.Rows[e.RowIndex].Cells[0].Value;

            selectedProduct = DB.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);

            txtProduct.Text = selectedProduct.Name;
            numPrice.Value = (decimal)selectedProduct.Price;
            cmbCategory.Text = selectedProduct.Category.Name;
            numQuantity.Value = (int)selectedProduct.Quantity;

            HideOrShowAddBtn("edit");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            selectedProduct.Name = txtProduct.Text;
            selectedProduct.Price = numPrice.Value;
            selectedProduct.CategoryId = DB.Categories.FirstOrDefault(x => x.Name == cmbCategory.Text).Id;
            selectedProduct.Quantity = (int)numQuantity.Value;

            DB.SaveChanges();
            GetProductsListGrid();

            HideOrShowAddBtn("add");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var question = MessageBox.Show("Do you really want to delete this product?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (question == DialogResult.Yes)
            {
                DB.Products.Remove(selectedProduct);
                DB.SaveChanges();
                GetProductsListGrid();

                MessageBox.Show("The product is deleted!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            HideOrShowAddBtn("add");
        }
    }
}
