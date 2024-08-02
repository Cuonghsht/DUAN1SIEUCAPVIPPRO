using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
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
        private readonly QuanLyHieuThuocEntities5 _context;
        private int iid;
        private int iddd;
        public SellMedicine(QuanLyHieuThuocEntities5 context, int idd, int ids)
        {
            InitializeComponent();
            _context = context;
            iid = idd;
            txtGia.Enabled = false;
            ListProduct();
            iddd = ids;
            PricePro.Enabled = false;
            IdPro.Enabled = false;
            NamePro.Enabled = false;
            HSD.Enabled = false;
            if (iddd != 0)
            {
                var HienThiBenKiaQua = (from m in _context.Customers
                                        join n in _context.Bills on m.IdCustomer equals n.IdCustomer
                                        join b in _context.Detailedbills on n.BillId equals b.BillId
                                        join v in _context.Products on b.ProductId equals v.ProductId
                                        join e in _context.Vouchers on n.IdVoucher equals e.IdVoucher
                                        where n.BillId == iddd
                                        select new
                                        {
                                            Cus = m,
                                            Bil = n,
                                            Del = b,
                                            pro = v,
                                            vou = e
                                        }).ToList();
                var checkhoadon = _context.Bills.FirstOrDefault(x => x.BillId == iddd);
                if (checkhoadon.StatusId == 1)
                {
                    themgio.Enabled = false;
                    xoa.Enabled = false;
                    thanhtona.Enabled = false;
                }
                SDTCus.Enabled = false;
                NameCus.Enabled = false;
                DiaChiCus.Enabled = false;
                IdHD.Enabled = false;
                Namradio.Enabled = false;
                Nuradio.Enabled = false;
                var idhds = HienThiBenKiaQua.FirstOrDefault();
                NameCus.Text = idhds.Cus.CustomerName;
                SDTCus.Text = idhds.Cus.SDT;
                IdHD.Text = idhds.Bil.BillId.ToString();
                var voucheri = _context.Vouchers.FirstOrDefault(x=>x.IdVoucher==idhds.Bil.IdVoucher);
                phamtramgiam.Text=voucheri.VoucherValue.ToString();
                hienthiview();
                themadd.Enabled = false;
                datavoucher.Enabled = false;
                TinhToan();

            }
            var voucher = (from v in _context.Vouchers
                           where (v.HSD > DateTime.Now && v.Quantity > 0 && v.idtt == 1)
                           select new
                           {
                               ID = v.IdVoucher,
                               Name = v.NameVoucher
                           }).ToList();
             datavoucher.DataSource = voucher;
            txttien.Enabled = false;
            TxtVoucher.Enabled = false;
            TxtGiaCuoiCung.Enabled = false;
            phamtramgiam.Enabled = false;

        }
        public void ListProduct()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("ID", 30);
            listView1.Columns.Add("Name", 120);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Quantity", 40);
            var list = (from a in _context.Products select a).ToList();
            foreach (var a in list)
            {
                ListViewItem item = new ListViewItem(a.ProductId.ToString());

                item.SubItems.Add(a.ProductName);
                item.SubItems.Add(a.ProductPrice.ToString());
                item.SubItems.Add(a.ProductQuantity.ToString());
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

        int idhdss;
        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (IdPro.Text == "")
            {
                MessageBox.Show("Vui lòng chọn sản phẩn cần thêm");
            }
            else
            {
                int number;
                if (!int.TryParse(txtSl.Text, out number))
                {
                    MessageBox.Show("Số lượng phải là 1 số nguyên dương");
                }

                else
                {
                    var sl = Convert.ToInt32(txtSl.Text);
                    if (txtSl.Text == "" || !txtSl.Text.All(char.IsDigit) || sl <= 0)
                    {
                        MessageBox.Show("Nhập lại phần số lượng ");
                    }
                    else
                    {
                        var ar = _context.Users.FirstOrDefault(x => x.IdTaiKhoan == iid);
                        decimal Gia;
                        decimal.TryParse((txtGia.Text), out Gia);
                        var khachhang = _context.Customers.Where(X => X.SDT == SDTCus.Text).FirstOrDefault();
                        if (khachhang == null)
                        {
                            MessageBox.Show("Khách hàng và hóa đơn này không tồn tại hãy chọn thêm hóa đơn ");
                        }
                        else
                        {
                            int ssl = Convert.ToInt32(txtSl.Text);
                            var HoaDon = _context.Bills.Where(x => x.IdCustomer == khachhang.IdCustomer).OrderByDescending(x => x.BillId).FirstOrDefault();

                            if (!txtSl.Text.All(char.IsDigit))
                            {

                                MessageBox.Show("Số lượng phải là 1 số tự nhiên lớn hơn 0");
                            }

                            else if (ssl <= 0)
                            {
                                MessageBox.Show("Số lượng phải là 1 số tự nhiên lớn hơn 0");
                            }
                            else
                            {
                                var v = Convert.ToInt32(IdPro.Text);
                                var c = Convert.ToInt32(txtSl.Text);
                                var kho = _context.Products.FirstOrDefault(x => x.ProductId == v);
                                if (kho.ProductQuantity < c)
                                {
                                    MessageBox.Show("Số lượng sản phẩm trong kho không đủ");
                                }
                                else
                                {
                                    decimal a;
                                    decimal.TryParse(txttien.Text, out a);
                                    decimal b;
                                    decimal.TryParse(txtGia.Text, out b);
                                    decimal r = a + b;
                                    decimal d;
                                    decimal.TryParse(TxtGiaCuoiCung.Text, out d);
                                    txttien.Text = r.ToString();
                                    if (iddd != 0)
                                    {
                                        var idhd1 = Convert.ToInt32(IdHD.Text);
                                        var tien = _context.Bills.FirstOrDefault(x => x.BillId == idhd1);
                                        Detailedbill aa = new Detailedbill
                                        {
                                            ProductId = v,
                                            BillId = Convert.ToInt32(IdHD.Text),
                                            Quantity = c,
                                            IdUser = ar.IdUser,
                                            Price = Gia,
                                        };
                                        _context.Detailedbills.Add(aa);
                                        tien.PriceBill = d;
                                        _context.SaveChanges();
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
                                        HoaDon.PriceBill = d;
                                        _context.SaveChanges();
                                        clear();
                                        listView1_Click(sender, e);
                                    }
                                    kho.ProductQuantity = kho.ProductQuantity - c;
                                    TinhToan();
                                    hienthiview();
                                    listView1.Clear();
                                    ListProduct();
                                }
                            }
                        }
                    }
                }
            }

        }
        public void hienthiview()
        {
            if (iddd != 0)
            {
                var listSp = (from a in _context.Products
                              join b in _context.Detailedbills on a.ProductId equals b.ProductId
                              join c in _context.Users on b.IdUser equals c.IdUser
                              where b.BillId == iddd
                              select new
                              {
                                  ID = b.DetailedbillsId,
                                  ProductName = a.ProductName,
                                  NameUser = c.UserName,
                                  Price = b.Price,
                                  QuanTiTy = b.Quantity,
                              }).ToList(); ;
                view.DataSource = listSp;
                var tinhtien = _context.Detailedbills.Where(x => x.BillId == iddd).Select(x => x.Price).ToList();
                txttien.Text = tinhtien.Sum().ToString();
            }
            else
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
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (SDTCus.Text == "")
            {
                MessageBox.Show("Bạn cần nhập vào đây số điện thoại của  khách hàng ");
            }
            else
            {
                if (SDTCus.TextLength != 10 || SDTCus.Text.Substring(0, 1) != "0" || !SDTCus.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Định danh số điện thoại chưa chính xác ");
                }
                else
                {

                    var voucher = _context.Vouchers.Where(x => x.HSD > DateTime.Now && x.Quantity > 0 && x.idtt == 1).ToList();
                    if (voucher != null && idvouchers==null)
                    {
                        if(MessageBox.Show("Trong kho vẫn còn voucher bạn có muốn thêm voucher cho hóa đơn này không","Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                        }
                        else
                        {
                            AddbillAndVoucher();
                        }
                    }
                    else
                    {
                        AddbillAndVoucher();
                    }

          //          var khachhang = _context.Customers.Where(X => X.SDT == SDTCus.Text).FirstOrDefault();
      //              var HoaDon = _context.Bills.Where(x => x.IdCustomer == khachhang.IdCustomer).OrderByDescending(x => x.BillId).FirstOrDefault();
   //                 idhdss = HoaDon.BillId;
                }

            }
        }
        public void TinhToan()
        {
            decimal GiaGoc;
            decimal PhanTram;
            decimal.TryParse(txttien.Text, out GiaGoc);
            decimal.TryParse(phamtramgiam.Text, out PhanTram);
            TxtVoucher.Text = (GiaGoc * (PhanTram / 100)).ToString();
            decimal GiaCGiam;
            decimal.TryParse(TxtVoucher.Text, out GiaCGiam);
            TxtGiaCuoiCung.Text = (GiaGoc-GiaCGiam).ToString();

        }

        public void AddbillAndVoucher()
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm hóa đơn này không", " Xác nhận ", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    MessageBox.Show("Khách hàng mới");
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
                    MessageBox.Show("Khách hàng đã từng mua sản phẩm ở đây");
                    ThemHoaDon();
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
               IdVoucher = idvouchers > 0 ? idvouchers : null,
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
            
            if (Idddd == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần loại bỏ ra khỏi hóa đơn");
            }
            else
            {
                var delete = _context.Detailedbills.FirstOrDefault(x => x.DetailedbillsId == Idddd);
                var hoadonanhhuong = _context.Bills.FirstOrDefault(x => x.BillId == delete.BillId);
                if (delete != null)
                {
                    if (MessageBox.Show("Ban chac chan muon xoa san phan nay ra khoi hoa don khong", "Xac Nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var SanPhamHoiQuantity = _context.Products.FirstOrDefault(x => x.ProductId == delete.ProductId);
                        int  Quantity = SanPhamHoiQuantity.ProductQuantity + delete.Quantity;
                        SanPhamHoiQuantity.ProductQuantity = Quantity;
                        hoadonanhhuong.PriceBill = hoadonanhhuong.PriceBill - delete.Price;
                        decimal canxoa = delete.Price;
                        decimal tien;
                        decimal.TryParse(txttien.Text, out tien);
                        txttien.Text = (tien - canxoa).ToString();
                        _context.Detailedbills.Remove(delete);
                        _context.SaveChanges();
                        listView1.Clear();
                        TinhToan();
                        ListProduct();
                        hienthiview();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn cần chọn sản phẩm cần xóa ");
                }
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
            if (tiendua < tienHd)
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
                MessageBox.Show("Vui lòng kiểm tra lại tiền ");
            }
            else
            {
                decimal TienCuoi;
                decimal.TryParse(TxtGiaCuoiCung.Text, out TienCuoi);
                if (iddd != 0)
                {
                    var capnhat = _context.Bills.FirstOrDefault(x => x.BillId == iddd);
                    var Kiemtragiohang = _context.Detailedbills.FirstOrDefault(x => x.BillId == capnhat.BillId);
                    if (Kiemtragiohang == null)
                    {
                        MessageBox.Show("Chưa có sản phâmr nào trong giỏ hàng");
                    }
                    else
                    {
                        capnhat.StatusId = 1;
                        capnhat.PriceBill = TienCuoi;
                        _context.SaveChanges();
                        MessageBox.Show("Đã thanh toán thành công ");
                    }
                }
                else
                {
                    var a = _context.Bills.FirstOrDefault(x => x.BillId == idhdss);
                    if (a == null)
                    {
                        MessageBox.Show("Hóa đơn chưa được tạo");
                    }
                    else
                    {
                        var b = _context.Detailedbills.FirstOrDefault(x => x.BillId == a.BillId);
                        if (b == null)
                        {
                            MessageBox.Show("Chưa có sản phẩm trong giỏ hàng");
                        }
                        else
                        {
                            a.StatusId = 1;
                            a.PriceBill = TienCuoi;
                            MessageBox.Show("Đã thanh toán thành công ");
                            _context.SaveChanges();

                        }
                    }
                }

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void combovoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vou = _context.Vouchers.FirstOrDefault(x => x.IdVoucher == idvouchers);
            phamtramgiam.Text= vou.VoucherValue.ToString();
            
        }
        int? idvouchers;
        private void datavoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             var a = datavoucher.Rows[e.RowIndex];
              idvouchers = Convert.ToInt32(a.Cells["ID"].Value.ToString());
            if (idvouchers != null)
            {
                var valuevocher = _context.Vouchers.FirstOrDefault(x=>x.IdVoucher == idvouchers);
                phamtramgiam.Text = valuevocher.VoucherValue.ToString();
            }
        }
    }
}

