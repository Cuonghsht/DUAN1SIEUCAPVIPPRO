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
        private readonly QuanLyHieuThuocEntities4 _dbcontext;
        public AddUser(QuanLyHieuThuocEntities4 dbcontext)
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
            if (txtAddress.Text == "" || txtEmail.Text == "" || txtPassword.Text == "" || txtPhone.Text == "" || TxtNameAccount.Text == "" || txtNameUser.Text == "")
            {
                MessageBox.Show("Ban can nhap day du thong tin vao cac o");
            }
            else
            {
                if (RadioNam.Checked == false && RadioNu.Checked == false)
                {
                    MessageBox.Show("Vui long chon gioi tinh");
                }
                else
                {
                    if (DateUser.Value.Year > Tuoi18)
                    {
                        MessageBox.Show("Nguoi dung phai lon hon 18 tuoi");
                    }
                    else
                    {
                        if (ComBoVaiTro.SelectedIndex < 0)
                        {
                            MessageBox.Show("Vui long chon vai tro");
                        }
                        else
                        {
                            if (txtPhone.TextLength != 10)
                            {
                                MessageBox.Show("So dien thoai bao gom 10 ki tu va bat dau bang so 0");
                            }
                            else
                            {
                                if (txtPhone.Text.Substring(0, 1) != "0")
                                {
                                    MessageBox.Show("So dien thoai bat dau bang So 0");

                                }
                                else
                                {

                                    if (txtPhone.Text.Any(char.IsDigit) == false)
                                    {
                                        MessageBox.Show("So dien thoai la day so tu 0-9");
                                    }
                                    else
                                    {
                                        if (txtEmail.TextLength < 11)
                                        {
                                            MessageBox.Show("Email chua dung dinh  dang");

                                        }
                                        else
                                        {
                                            var dodai = txtEmail.TextLength;

                                            var loai = dodai - 10;
                                            var sosanh = txtEmail.Text.Substring(loai, 10);
                                            if (sosanh != "@gmail.com")
                                            {
                                                MessageBox.Show("Duoi phai la @gmail.com");
                                            }

                                            else
                                            {
                                                var a = _dbcontext.TaiKhoans.FirstOrDefault(x => x.NameTaiKhoan == TxtNameAccount.Text);
                                                if (a != null)
                                                {
                                                    MessageBox.Show("ten tai khoan nay da ton tai");
                                                }
                                                else
                                                {
                                                    if (txtPassword.Text.Any(char.IsDigit) ==false || txtPassword.Text.Any(char.IsLetter)==false)
                                                    {
                                                        MessageBox.Show("Mat khau bao gom ca so va ki tu");
                                                    }
                                                    else
                                                    {
                                                        if (MessageBox.Show("Xac Nhan", "Ban co chac chan muon them khong", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                        {
                                                            var Ac = new TaiKhoan
                                                            {
                                                                NameTaiKhoan = TxtNameAccount.Text,
                                                                PassWordd = txtPassword.Text,
                                                                IdTaiKhoan = 1,
                                                                IdRoles = ComBoVaiTro.SelectedIndex + 1
                                                            };
                                                            _dbcontext.TaiKhoans.Add(Ac);
                                                            bool gioitinnh;
                                                            if (RadioNam.Checked)
                                                            {
                                                                gioitinnh = true;
                                                            }
                                                            else
                                                            {
                                                                gioitinnh = false;
                                                            }
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
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
