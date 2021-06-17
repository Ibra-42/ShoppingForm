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
    public partial class FormOrders : Form
    {
        ShoppingFormContext DB = new();
        public FormOrders()
        {
            InitializeComponent();
        }

        private void GetOrdersListGrid()
        {
            dtgOrders.DataSource = DB.Orders.Select(ord => new
            {
                ord.Id,
                ProductId = ord.ProductId,
                WorkerId = ord.WorkerId,
                Count = ord.Count,
                Date = ord.Date,
                TotalPrice = ord.TotalPrice,
            }).ToList();

            dtgOrders.Columns[0].Visible = false;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            GetOrdersListGrid();
        }
    }
}
