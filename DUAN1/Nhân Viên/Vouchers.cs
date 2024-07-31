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
        public void HienThi()
        {
            var a = (from b in _context.Vouchers join c in _context.TrangThaiVouchers on b.idtt equals c.idtt select new
            {
                Name = b.NameVoucher,

                tt = c
            }).ToList();
            dataGridView1.DataSource = a;
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
    }
}
