using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DUAN1.Nhân_Viên
{
    public partial class SellMedicine : Form
    {
        private readonly QuanLyHieuThuocEntities1 _context;
        private int _id;
        private List<Customer> khachhang;
        public SellMedicine(QuanLyHieuThuocEntities1 context, int idd)
        {
            InitializeComponent();
            _context = context;
            _id = idd;
            txtGia.Enabled = false;
            ListProduct();

            PricePro.Enabled = false;
            IdPro.Enabled = false;
            NamePro.Enabled = false;
            HSD.Enabled = false;

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
        int id;

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem listViewItem = listView1.SelectedItems[0];
                IdPro.Text = listViewItem.Text;

                int.TryParse(listViewItem.Text, out id);
                var abc = _context.Products.FirstOrDefault(x => x.ProductId == id);
                NamePro.Text = abc.ProductName;
                PricePro.Text = abc.ProductPrice.ToString();
                DateTime HSDpro;
                DateTime.TryParse((abc.ProductExpiry.ToString()), out HSDpro);
                HSD.Value = HSDpro;
                int abcd;
                int.TryParse(txtSl.Text, out abcd);
                if (abc.ProductQuantity < abcd)
                {
                    txtSl.BackColor = Color.Red;
                    danger.Visible = true;
                }
                else
                {
                    danger.Visible = false;
                }



            }
        }
        private void txtGia_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtGia_MouseClick(object sender, MouseEventArgs e)
        {
        }
        public void clear()
        {
            txtGia.Text = "";
            txtSl.Text = "";
            IdPro.Text = "";
            NamePro.Text = "";
            HSD.Value = DateTime.Now;
            txtGia.Text = "";
            PricePro.Text = "";
        }


        int idhoadon;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (IdPro.Text == "")
            {
                MessageBox.Show("Vui long chon san pham ban can them vao gio hang");
            }
            else
            {
                int number;
                if (!int.TryParse(txtSl.Text, out number))
                {
                    MessageBox.Show("Hay nhap 1 so nguyen vao day");
                }

                else
                {
                    var sl = Convert.ToInt32(txtSl.Text);
                    if (txtSl.Text == "" || !txtSl.Text.All(char.IsDigit) || sl <= 0)
                    {
                        MessageBox.Show("ban ca nhap so luong dung cach");
                    }
                    else
                    {
                        var ar = _context.Users.FirstOrDefault(x => x.IdTaiKhoan == _id);
                        decimal Gia;
                        decimal.TryParse((txtGia.Text), out Gia);
                        var khachhang = _context.Customers.Where(X => X.SDT == SDTCus.Text).FirstOrDefault();
                        if (khachhang == null)
                        {
                            MessageBox.Show("khach hang nay chua duoc them vao data");
                        }
                        else
                        {
                            int ssl = Convert.ToInt32(txtSl.Text);
                            var HoaDon = _context.Bills.Where(x => x.IdCustomer == khachhang.IdCustomer).OrderByDescending(x => x.BillId).FirstOrDefault();
                            idhoadon = HoaDon.BillId;

                            if (!txtSl.Text.All(char.IsDigit))
                            {

                                MessageBox.Show("so luong phair laf 1 so nguyen duong");
                            }

                            else if (ssl <= 0)
                            {
                                MessageBox.Show(" So luon phai la 1 so lon hon 0");
                            }
                            else
                            {
                                var v = Convert.ToInt32(IdPro.Text);
                                var c = Convert.ToInt32(txtSl.Text);
                                var kho = _context.Products.FirstOrDefault(x => x.ProductId == v);
                                if (kho.ProductQuantity < c)
                                {
                                    MessageBox.Show(" so luong hang trong kho khongo con du");
                                }
                                else
                                {
                                    Detailedbill detailedbill = new Detailedbill
                                    {
                                        ProductId = v,
                                        BillId = Convert.ToInt32(HoaDon.BillId),
                                        Quantity = c,
                                        IdUser = ar.IdUser,
                                        Price = Gia,

                                    };
                                    _context.Detailedbills.Add(detailedbill);
                                    kho.ProductQuantity = kho.ProductQuantity - v;
                                    HoaDon.PriceBill = HoaDon.PriceBill + Gia;
                                    _context.SaveChanges();
                                    hienthiview();
                                    clear();
                                    listView1_Click(sender, e);
                                    var tongtien = _context.Detailedbills.Where(x => x.BillId == HoaDon.BillId).Select(x => x.Price).ToList();
                                    txttien.Text = tongtien.Sum().ToString();
                                }
                            }
                        }
                    }
                }

            }
        }
        public void hienthiview()
        {
            var khachhang = _context.Customers.Where(X => X.SDT == SDTCus.Text).FirstOrDefault();
            var HoaDon = _context.Bills.Where(x => x.IdCustomer == khachhang.IdCustomer).OrderByDescending(x => x.BillId).FirstOrDefault();
            var ds = (from a in _context.Products
                      join b in _context.Detailedbills on a.ProductId equals b.ProductId
                      join c in _context.Users on b.IdUser equals c.IdUser
                      where b.BillId == HoaDon.BillId
                      select new
                      {
                          ID = b.DetailedbillsId,
                          ProductName = a.ProductName,
                          NameUser = c.UserName,
                          Price = b.Price,
                          QuanTiTy = b.Quantity,
                      }).ToList();
            view.DataSource = ds;

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (SDTCus.Text == "")
            {
                MessageBox.Show(" o SDT khong duoc de trong");
            }
            else
            {
                if (SDTCus.TextLength != 10 || SDTCus.Text.Substring(0, 1) != "0" || !SDTCus.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Dinh danh Sdt chua chinh xac");
                }
                else
                {
                    if (MessageBox.Show("Ban co chac chan muon them hoa don cho khach hang nay khong", " Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        var checkTonTai = _context.Customers.FirstOrDefault(x => x.SDT == SDTCus.Text);
                        if (checkTonTai == null)
                        {
                            _context.Customers.Add(customer);
                            _context.SaveChanges();
                            ThemHoaDon();
                        }
                        else
                        {
                            var b = _context.Customers.FirstOrDefault(x => x.SDT == SDTCus.Text);
                            NameCus.Text = b.CustomerName;
                            DiaChiCus.Text = b.CustomerAddress;
                            if (b.CustomerGender == true)
                            {
                                Namradio.Checked = true;
                            }
                            else
                            {
                                Nuradio.Checked = true;
                            }
                            MessageBox.Show("Khach hang nay da tung mua san pham o day");
                            ThemHoaDon();
                        }
                    }
                }
            }
        }



        public void ThemHoaDon()
        {
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

        private void txtSl_TextChanged(object sender, EventArgs e)
        {
            var tongtien = _context.Products.FirstOrDefault(x => x.ProductId == id);
            decimal gia = tongtien.ProductPrice;
            int sl;
            int.TryParse(txtSl.Text, out sl);
            decimal tong = sl * gia;
            txtGia.Text = tong.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        int Idddd;
        private void view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = view.Rows[e.RowIndex];
            Idddd = Convert.ToInt32(a.Cells["ID"].Value.ToString());

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var delete = _context.Detailedbills.FirstOrDefault(x => x.DetailedbillsId == Idddd);
            if (delete!=null) {
                if (MessageBox.Show("Ban chac chan muon xoa san phan nay ra khoi hoa don khong", "Xac Nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _context.Detailedbills.Remove(delete);
                    _context.SaveChanges();
                    decimal canxoa = delete.Price;
                    decimal tien;
                    decimal.TryParse(txttien.Text, out tien);
                    txttien.Text = (tien - canxoa).ToString();
                    hienthiview();
                }
            }
            else
            {
                MessageBox.Show("Ban can chon san phan cam xoa ");
            }

        }

        private void txtkhachdua_TextChanged(object sender, EventArgs e)
        {
            decimal tiendua;
            decimal tienHd;

            decimal.TryParse(txttien.Text, out tienHd);
            decimal.TryParse(txtkhachdua.Text, out tiendua);
            decimal tienthua = tiendua - tienHd;
            txtThua.Text = tienthua.ToString();
            if (tiendua <tienHd)
            {
                meme.Visible = true;
            }
            else
            {
                meme.Visible = false;
            }
        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            if (txttimkiem.Text == "")
            {
                listView1.Items.Clear();
                var list = (from a in _context.Products select a).ToList();
                foreach (var a in list)
                {
                    ListViewItem item = new ListViewItem(a.ProductId.ToString());

                    item.SubItems.Add(a.ProductName);

                    item.SubItems.Add(a.ProductPrice.ToString());

                    listView1.Items.Add(item);
                }
            }
            else
            {
                foreach (ListViewItem a in listView1.Items)
                {
                    bool match = a.SubItems[1].Text.ToLower().Contains(txttimkiem.Text);
                    if (match)
                    {
                        a.BackColor = System.Drawing.Color.Yellow;  
                        a.ForeColor = System.Drawing.Color.Black;   
                    }
                    else
                    {
                        a.BackColor = System.Drawing.Color.White;  
                        a.ForeColor = System.Drawing.Color.Gray;    
                    }
                }
               
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            decimal tiendua;
            decimal tienthua;
            decimal.TryParse(txtThua.Text, out tienthua);
            decimal.TryParse(txtkhachdua.Text, out tiendua);

            if (txtkhachdua.Text == "" || tiendua <= 0 || tienthua < 0)
            {
                MessageBox.Show("vui long kiem tra lai tien");
            }
            else
            {
                decimal TienCuoi;
                decimal.TryParse(txttien.Text, out TienCuoi);
                var a = _context.Bills.FirstOrDefault(x => x.BillId == idhoadon);
                a.StatusId = 1;
                a.PriceBill = TienCuoi;
                _context.SaveChanges();
                MessageBox.Show("Da thanh toan thnah cong");
            }
        }
    }
}

