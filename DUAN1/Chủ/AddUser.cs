using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1.Chủ
{
    public partial class AddUser : Form
    {
        private readonly QuanLyHieuThuocEntities6 _dbcontext;
        public AddUser(QuanLyHieuThuocEntities6 dbcontext)
        {
            _dbcontext = dbcontext;
            InitializeComponent();
            var VaiTro = (from a in _dbcontext.Roles select a).ToList();
            foreach (var i in VaiTro)
            {
                ComBoVaiTro.Items.Add(i.NameRoles);
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }

        private void btnSigin_Click(object sender, EventArgs e)
        {
            var Tuoi18 = DateTime.Now.Year - 18;

            if (string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(TxtNameAccount.Text) || string.IsNullOrEmpty(txtNameUser.Text))
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin vào các ô");
                return;
            }

            if (!RadioNam.Checked && !RadioNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                return;
            }

            if (DateUser.Value.Year > Tuoi18)
            {
                MessageBox.Show("Người dùng phải lớn hơn 18 tuổi");
                return;
            }

            if (ComBoVaiTro.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò");
                return;
            }

            if (txtPhone.TextLength != 10)
            {
                MessageBox.Show("Số điện thoại bao gồm 10 kí tự và bắt đầu bằng số 0");
                return;
            }

            if (txtPhone.Text.Substring(0, 1) != "0")
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng số 0");
                return;
            }

            if (!txtPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại gồm các số tự nhiên 0-9");
                return;
            }

            if (txtEmail.TextLength < 11)
            {
                MessageBox.Show("Email chưa đúng định dạng");
                return;
            }

            var dodai = txtEmail.TextLength;
            var loai = dodai - 10;
            var sosanh = txtEmail.Text.Substring(loai, 10);
            if (sosanh != "@gmail.com")
            {
                MessageBox.Show("Đuôi của email phải là @gmail.com");
                return;
            }

            var a = _dbcontext.TaiKhoans.FirstOrDefault(x => x.NameTaiKhoan == TxtNameAccount.Text);
            if (a != null)
            {
                MessageBox.Show("Tên tài khoản này đã tồn tại");
                return;
            }

            if (!txtPassword.Text.Any(char.IsDigit) || !txtPassword.Text.Any(char.IsLetter) || txtPassword.TextLength < 10)
            {
                MessageBox.Show("Mật khẩu phải bao gồm số, ký tự và phải dài hơn 10 ký tự");
                return;
            }

            var checksdt = _dbcontext.Users.FirstOrDefault(x => x.SDT == txtPhone.Text);
            if (checksdt != null)
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thêm người này vào không", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var Ac = new TaiKhoan
                {
                    NameTaiKhoan = TxtNameAccount.Text,
                    PassWordd = txtPassword.Text,
                    IdTaiKhoan = 1,
                    IdRoles = ComBoVaiTro.SelectedIndex + 1,
                    idtrangthai = 1,
                };

                _dbcontext.TaiKhoans.Add(Ac);

                var gioitinnh = RadioNam.Checked;

                var user = new User
                {
                    UserName = txtNameUser.Text,
                    UserAddress = txtAddress.Text,
                    UserAge = DateUser.Value.Date,
                    SDT = txtPhone.Text,
                    Email = txtEmail.Text,
                    UsesGend = gioitinnh,
                    IdTaiKhoan = Ac.IdTaiKhoan,
                };

                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();
                clear();
            }

        }

        private void label7_Click(object sender, EventArgs e)
            {

            }

            private void label8_Click(object sender, EventArgs e)
            {

            }

            private void label3_Click(object sender, EventArgs e)
            {

            }

            private void label10_Click(object sender, EventArgs e)
            {

            }

            private void label6_Click(object sender, EventArgs e)
            {

            }

            private void txtPassword_TextChanged(object sender, EventArgs e)
            {

            }

            private void label5_Click(object sender, EventArgs e)
            {

            }

            private void radioButton1_CheckedChanged(object sender, EventArgs e)
            {

            }

            public void clear()
            {
                txtAddress.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtPhone.Text = "";
                TxtNameAccount.Text = "";
                RadioNam.Checked = false;
                RadioNu.Checked = false;
                ComBoVaiTro.SelectedIndex = -1;
                txtNameUser.Text = "";

            }
            private void guna2Button1_Click(object sender, EventArgs e)
            {
                clear();
            }
        }
    }


