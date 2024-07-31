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
		public readonly QuanLyHieuThuocEntities4 _context;
		public Vouchers(QuanLyHieuThuocEntities4 contetxt)
		{
			_context = contetxt;
			InitializeComponent();
			HienThi();
		}
		public void HienThi()
		{
			var a = (from b in _context.Vouchers select b).ToList();
			dataGridView1.DataSource = a;
		}
        private void Vouchers_Load(object sender, EventArgs e)
        {

        }
		public bool Validate()
		{
            int phamtrm;
            int soluong;
           bool ss= int.TryParse(txtPhanTram.Text, out phamtrm);
            bool aa= int.TryParse(txtSoLuong.Text, out soluong);
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
            else if(dateSX.Value>dateHD.Value)
            {
                MessageBox.Show("ngày bắt đầu không thể lơns hơn ngay hết thúc được");
            }
            else if(dateHD.Value<DateTime.Now)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày hiẹn tại");
            }
            else
            {
                return true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                MessageBox.Show("thanh cong");
            }
        }
    }
}
