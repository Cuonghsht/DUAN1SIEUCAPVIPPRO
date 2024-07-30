using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1.Nhân_Viên
{
    public partial class ViewMedicine : Form
    {
        private readonly QuanLyHieuThuocEntities4 _context;
        public ViewMedicine(QuanLyHieuThuocEntities4 context)
        {
            _context = context;
            InitializeComponent();
            
            var cate = (from c in _context.Categories select c).ToList();
            var unit = (from a in _context.Units select a).ToList();
            foreach (var i in unit)
            {
                UnitPro.Items.Add(i.UnitName);
            }
            foreach (var item in cate)
            {
                CatePro.Items.Add(item.CategoryName);
            }
            HienThiView();
        }

        public void HienThiView()
        {
            var View = (from a in _context.Units
                        join b in _context.Products on a.IdUnit equals b.IdUnit
                        join c in _context.Categories on b.IdCategory equals c.IdCategory
                        select new
                        {
                            MedicineId = b.ProductId,
                            MedicineName = b.ProductName,
                            MedicineExpiry = b.ProductExpiry,
                            Quantity = b.ProductQuantity,
                            Category = c.CategoryName,
                            Unit = a.UnitName,
                        }).ToList();

            dataGridView1.DataSource = View;

        }
        private void ViewMedicine_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var Ss = dataGridView1.Rows[e.RowIndex];
            var b = Ss.Cells["MedicineExpiry"].Value.ToString();
            DateTime datevalue;
            DateTime.TryParse(b, out datevalue);
            var HSD = DateTime.Now.Month - datevalue.Month;
            var a = Convert.ToInt32(Ss.Cells["Quantity"].Value.ToString());
            if (a < 5)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
            }
        }

        private void TxtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (TxtTimkiem.Text == "")
            {
                HienThiView();
            }
            else
            {
                var timkiem = (from b in _context.Categories
                               join a in _context.Products on b.IdCategory equals a.IdCategory
                               join c in _context.Units
                               on a.IdUnit equals c.IdUnit
                               where (a.ProductName.ToLower().Contains(TxtTimkiem.Text) || b.CategoryName.ToLower().Contains(TxtTimkiem.Text))
                               select new
                               {
                                   MedicineId = a.ProductId,
                                   MedicineName = a.ProductName,
                                   MedicineExpiry = a.ProductExpiry,
                                   Quantity = a.ProductQuantity,
                                   Category = b.CategoryName,
                                   Unit = c.UnitName,
                               }).ToList();
                dataGridView1.DataSource = timkiem;
            }
        }
        int a;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var b = dataGridView1.Rows[e.RowIndex];
            a = Convert.ToInt32(b.Cells["MedicineId"].Value.ToString());
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (a == 0)
            {
                MessageBox.Show(" Ban can chon nguoi can xoa");
            }
            else
            {
                var delete = _context.Products.FirstOrDefault(x => x.ProductId == a);
                if (delete == null)
                {
                    MessageBox.Show(" San pham nay khong ton tai");
                }
                else
                {
                    if (MessageBox.Show("Ban co chac chan muon xoa san pham nay ra khoi he thong khon", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _context.Products.Remove(delete);
                        _context.SaveChanges();
                        HienThiView();
                        a = 0;
                    }

                }
            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
