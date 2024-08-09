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
        private readonly QuanLyHieuThuocEntities6 _context;
        public ViewMedicine(QuanLyHieuThuocEntities6 context)
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
            var status = _context.ProductStatus.ToList();
            foreach (var item in status)
            {
                Statuspr.Items.Add(item.StatusName);
            }
            HienThiView();
        }

        public void HienThiView()
        {
            var View = (from a in _context.Units
                        join b in _context.Products on a.IdUnit equals b.IdUnit
                        join c in _context.Categories on b.IdCategory equals c.IdCategory
                        join d in _context.ProductStatus on b.StatusPr equals d.Idstatus
                        select new
                        {
                            MedicineId = b.ProductId,
                            MedicineName = b.ProductName,
                            Price = b.ProductPrice,
                            MedicineExpiry = b.ProductExpiry,
                            Quantity = b.ProductQuantity,
                            Category = c.CategoryName,
                            Unit = a.UnitName,
                            Status = d.StatusName
                        }).ToList();

            dataGridView1.DataSource = View;

        }
        private void ViewMedicine_Load(object sender, EventArgs e)
        {

        }

        private async void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var Ss = dataGridView1.Rows[e.RowIndex];
            var a = Convert.ToInt32(Ss.Cells["MedicineId"].Value.ToString());
            var check = (from r in _context.Products  select r).ToList();
             List<int> pr = new List<int>();
            foreach (var i in check)
            {
                if (i.ProductQuantity < 5 || i.ProductExpiry.Date < DateTime.Now|| i.StatusPr==2)
                {
                    pr.Add(i.ProductId);

                }
               
            }
            foreach(var r in pr)
            {
                if (r == a)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                }
               
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
                                   Price = a.ProductPrice,
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
            IdPro.Text = b.Cells["MedicineId"].Value.ToString();
            NamePro.Text = b.Cells["MedicineName"].Value.ToString();
            SLPro.Text = (b.Cells["Quantity"].Value.ToString());
            Giapro.Text = (b.Cells["Price"].Value.ToString());
            var timrole = _context.Products.FirstOrDefault(x => x.ProductId == a);
            CatePro.SelectedIndex = timrole.IdCategory - 1;
            UnitPro.SelectedIndex = timrole.IdUnit - 1;
            Statuspr.SelectedIndex = timrole.StatusPr - 1;

        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (a == 0)
            {
                MessageBox.Show("Bạn cần chọn sản phầm cần dừng bán");
            }
            else
            {
                var delete = _context.Products.FirstOrDefault(x => x.ProductId == a);
                if (delete == null)
                {
                    MessageBox.Show("Sản phẩm này  không tồn tại");
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn dừng bán sản phẩm này không", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        delete.StatusPr = 2;
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
