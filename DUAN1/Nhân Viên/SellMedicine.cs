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
    public partial class SellMedicine : Form
    {
        private readonly QuanLyHieuThuocEntities1 _context;
        private int _id;
        public SellMedicine(QuanLyHieuThuocEntities1 context, int idd)
        {
            InitializeComponent();
            _context = context;
            _id = idd;
            
            ListProduct();
        }
        public void ListProduct()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("ID", 30);
            listView1.Columns.Add("Name", 170);
            listView1.Columns.Add("Price", 130);

            var list = (from a in _context.Products select a).ToList();
            foreach (var a in list)
            {
                ListViewItem item = new ListViewItem(a.ProductId.ToString());

                item.SubItems.Add(a.ProductName);

                item.SubItems.Add(a.ProductPrice.ToString());

                listView1.Items.Add(item);
            }
        }
        private void SellMedicine_Load(object sender, EventArgs e)
        {

        }
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem listViewItem = listView1.SelectedItems[0];
                IdPro.Text = listViewItem.Text;
                int id;
                int.TryParse(listViewItem.Text, out id);
                var ThongTinSp = _context.Products.FirstOrDefault(x => x.ProductId == id);
                NamePro.Text = ThongTinSp.ProductName;
                PricePro.Text = ThongTinSp.ProductPrice.ToString();
                DateTime HSDpro;
                DateTime.TryParse((ThongTinSp.ProductExpiry.ToString()), out HSDpro);
                HSD.Value = HSDpro;
                int sl;
                int.TryParse(txtSl.Text, out sl);
                if (ThongTinSp.ProductQuantity < sl)
                {
                    txtSl.BackColor = Color.Red;
                    danger.Visible = true;
                }
                else
                {
                    danger.Visible = false;
                }
                decimal gia = ThongTinSp.ProductPrice;
                decimal tong = sl * gia;
                txtGia.Text = tong.ToString();


            }
        }
        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtGia_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var ar = _context.Users.FirstOrDefault(x => x.IdTaiKhoan == _id);
            decimal Gia;
            decimal.TryParse((txtGia.Text), out Gia);
            var khachhang = _context.Customers.FirstOrDefault(x => x.SDT == SDTCus.Text);
            if (khachhang == null)
            {
                MessageBox.Show("khach hang nay chua duoc them vao data");
            }
            var HoaDon = _context.Bills.FirstOrDefault(x => x.IdCustomer == khachhang.IdCustomer);
            Detailedbill detailedbill = new Detailedbill
            {
                ProductId = Convert.ToInt32(IdPro.Text),
                BillId = Convert.ToInt32(HoaDon.BillId),
                Quantity = Convert.ToInt32(txtSl.Text),
                IdUser = ar.IdUser,
                Price = Gia,

            };
            _context.Detailedbills.Add(detailedbill);
            _context.SaveChanges();



        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            bool GioiTinh;
            if (Namradio.Checked)
            {
                GioiTinh = true;
            }
            else
            {
                GioiTinh = false;
            }
            Customer customer = new Customer
            {
                CustomerName = NameCus.Text,
                SDT = SDTCus.Text,
                CustomerAddress = DiaChiCus.Text,
                CustomerGender = GioiTinh,

            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            var IdHd = _context.Customers.FirstOrDefault(x => x.SDT == SDTCus.Text);
            Bill bill = new Bill
            {
                IdCustomer = IdHd.IdCustomer,
                PriceBill = 0,
                DateBill = DateTime.Now.Date,
                StatusId = 2,
            };
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }


        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}


