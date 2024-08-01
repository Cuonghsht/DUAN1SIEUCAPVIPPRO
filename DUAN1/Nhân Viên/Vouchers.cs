using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DUAN1.Nhân_Viên
{
    public partial class Vouchers : Form
    {
        public readonly QuanLyHieuThuocEntities5 _context;
        public Vouchers(QuanLyHieuThuocEntities5 contetxt)
        {
            _context = contetxt;
            InitializeComponent();
            HienThi();
            var tt = (from b in _context.TrangThaiVouchers select b).ToList();
            foreach (var i in tt)
            {
                trangthai.Items.Add(i.Namett);
            }
        }

        private void Vouchers_Load(object sender, EventArgs e)
        {

        }
        public bool Validate()
        {
            int phamtrm;
            int soluong;
            bool ss = int.TryParse(txtPhanTram.Text, out phamtrm);
            bool aa = int.TryParse(txtSoLuong.Text, out soluong);
            if (txtPhanTram.Text == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Không được để trống các ô");
                return false;
            }
            else if (phamtrm < 0 || phamtrm > 100 || soluong <= 0)
            {
                MessageBox.Show("số lượng hoặc giá trị voucer không phù hợp ");
                return false;

            }
            else if (!aa || !ss)
            {
                MessageBox.Show("bạn cần kiểm tra lại giá trị và số lượng của voucher ");
                return false;
            }
            else if (dateSX.Value > dateHD.Value)
            {
                MessageBox.Show("ngày bắt đầu không thể lơns hơn ngay hết thúc được");
                return false;
            }
            else if (dateHD.Value < DateTime.Now)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày hiẹn tại");
                return false;
            }
            else if (trangthai.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn cần chọn trạng thái cho voucher ạ ");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                int soluong;
                int value;
                int.TryParse(txtSoLuong.Text, out soluong);
                int.TryParse(txtPhanTram.Text, out value);
                Voucher vouchers = new Voucher
                {
                    NSX = dateSX.Value.Date,
                    HSD = dateHD.Value.Date,
                    NameVoucher = VoucherName.Text.ToString(),
                    Quantity = soluong,
                    VoucherValue = value,
                    idtt = trangthai.SelectedIndex + 1,
                };
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm voucher không", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _context.Vouchers.Add(vouchers);
                    _context.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    HienThi();
                }
            }
        }
        public void HienThi()
        {
            var a = (from b in _context.Vouchers
                     join c in _context.TrangThaiVouchers on b.idtt equals c.idtt
                     select new
                     {
                         IdVoucher = b.IdVoucher,
                         Name = b.NameVoucher,
                         DateSX = b.NSX,
                         DateSD = b.HSD,
                         Quantity = b.Quantity,
                         Value = b.VoucherValue,
                         Status = c.Namett,
                        
                     }).ToList();
            dataGridView1.DataSource = a;
            
          
        }
        int idCapNhat;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn dòng hợp lệ ạ ");
            }
            else
            {
                DateTime nsx;
                DateTime hsd;
                var a = dataGridView1.Rows[e.RowIndex];
                idCapNhat = Convert.ToInt32(a.Cells["IdVoucher"].Value.ToString());
                txtPhanTram.Text = (a.Cells["Value"].Value.ToString());
                txtSoLuong.Text = a.Cells["Quantity"].Value.ToString();
                VoucherName.Text = a.Cells["Name"].Value.ToString();
                DateTime.TryParse(a.Cells["DateSX"].Value.ToString(), out nsx);
                DateTime.TryParse(a.Cells["DateSD"].Value.ToString(), out hsd);
                var tragthai = _context.Vouchers.FirstOrDefault(x => x.IdVoucher == idCapNhat);
                trangthai.SelectedIndex = tragthai.idtt-1;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn câp nhật thông tin của voucher không ạ ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int soluong;
                    int value;
                    int.TryParse(txtSoLuong.Text, out soluong);
                    int.TryParse(txtPhanTram.Text, out value);
                    var ThongTinVoucher = _context.Vouchers.FirstOrDefault(x => x.IdVoucher == idCapNhat);
                    ThongTinVoucher.NSX = dateSX.Value.Date;
                    ThongTinVoucher.HSD = dateHD.Value.Date;
                    ThongTinVoucher.NameVoucher = VoucherName.Text.ToString();
                    ThongTinVoucher.Quantity = soluong;
                    ThongTinVoucher.VoucherValue = value;
                    ThongTinVoucher.idtt = trangthai.SelectedIndex + 1;
                    _context.SaveChanges();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var VoucherDung = _context.Vouchers.FirstOrDefault(x => x.IdVoucher == idCapNhat);
            if (MessageBox.Show("Bạn có chắc chắn muốn Voucher này ngưng hoạt động không ạ","Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                VoucherDung.idtt = 2;
                _context.SaveChanges();
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            var tragthai = (from r in _context.Vouchers where(r.idtt==2) select r).ToList();
            List<int> mausac = new List<int>();
            foreach(var i in tragthai)
            {
                if (i.idtt == 2)
                {
                    mausac.Add(i.IdVoucher);
                }
            }
            var Ss = dataGridView1.Rows[e.RowIndex];
            var a = Convert.ToInt32(Ss.Cells["IdVoucher"].Value.ToString());
            foreach (var i in mausac)
            {
                if (a == i)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }


        }
    }
}
