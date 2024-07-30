using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace DUAN1.Chủ
{
    public partial class ViewUser : Form
    {
        private readonly QuanLyHieuThuocEntities4 _context;
        public ViewUser(QuanLyHieuThuocEntities4 context)
        {
            InitializeComponent();
            _context = context;
            var vaitro = (from b in _context.Roles select b).ToList();
            foreach (var i in vaitro)
            {
                ComBoVaiTro.Items.Add(i.NameRoles);
            }
            HienThiView();

        }
        public void HienThiView()
        {
            var HienThi = (from a in _context.Users
                           join b in _context.TaiKhoans on a.IdTaiKhoan equals b.IdTaiKhoan
                           join c in _context.Roles on b.IdRoles equals c.IdRoles
                           select new
                           {
                               IdUser = a.IdUser,
                               UserName = a.UserName,
                               UserEmail = a.Email,
                               UserPhone = a.SDT,
                               UserBirthday = a.UserAge,
                               UserGend = (bool)a.UsesGend ? "Nam" : "Nữ",
                               UserAddress = a.UserAddress,
                               UserRole = c.NameRoles,

                           }).ToList();
            View.DataSource = HienThi;
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (TxtTimKiem.Text == "")
            {
                HienThiView();
            }
            else
            {
                var Tim = (from a in _context.Users
                           join b in _context.TaiKhoans on a.IdTaiKhoan equals b.IdTaiKhoan
                           join c in _context.Roles on b.IdRoles equals c.IdRoles
                           where (a.UserName.Contains(TxtTimKiem.Text) || a.SDT.Contains(TxtTimKiem.Text))
                           select new
                           {
                               IdUser = a.IdUser,
                               UserName = a.UserName,
                               UserEmail = a.Email,
                               UserPhone = a.SDT,
                               UserBirthday = a.UserAge,
                               UserGend = (bool)a.UsesGend ? "Nam" : "Nữ",
                               UserAddress = a.UserAddress,
                               UserRole = c.NameRoles,

                           }).ToList();
                View.DataSource = Tim;
            }
        }

        private void View_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int a = 0;
        private void View_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("Ban can chon nguoi dung cu the");
            }
            else
            {
                var dong = View.Rows[e.RowIndex];
                a = Convert.ToInt32(dong.Cells["IdUser"].Value.ToString());
                txtNameUser.Text = dong.Cells["UserName"].Value.ToString();
                txtEmail.Text = dong.Cells["UserEmail"].Value.ToString();
                txtPhone.Text = dong.Cells["UserPhone"].Value.ToString();
                txtAddress.Text = dong.Cells["UserAddress"].Value.ToString();
                DateTime ngayh;
                DateTime.TryParse(dong.Cells["UserBirthday"].Value.ToString(), out ngayh);
                DateUser.Value = ngayh;

                var laygiatri = _context.Users.FirstOrDefault(x => x.IdUser == a);
                var role = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == laygiatri.IdTaiKhoan);
                if (laygiatri.UsesGend == true)
                {
                    RadioNam.Checked = true;
                    RadioNu.Checked = false;
                }
                else
                {
                    RadioNam.Checked = false;
                    RadioNu.Checked = true;
                }
                var c = role.IdRoles + 1;
                ComBoVaiTro.SelectedIndex = role.IdRoles - 1;

            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (a == 0)
            {
                MessageBox.Show(" Ban can chon nguoi can xoa");
            }
            else
            {
                var delete = _context.Users.FirstOrDefault(x => x.IdUser == a);
                var DeleteAc = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == delete.IdTaiKhoan);
                if (delete == null)
                {
                    MessageBox.Show(" Nguoi nay khong ton tai");
                }
                else
                {
                    if (MessageBox.Show("Ban co chac chan muon tai khoan nay dung hoat dong khong", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DeleteAc.idtrangthai = 2;
                        _context.SaveChanges();
                        HienThiView();
                        a = 0;
                    }

                }
            }
        }

        private void ViewUser_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void RadioNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void update_Click(object sender, EventArgs e)
        {
            var Tuoi18 = DateTime.Now.Year - 18;
            if (txtAddress.Text == "" || txtEmail.Text == "" || txtPhone.Text == "" || txtNameUser.Text == "")
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
                                                

                                                if (MessageBox.Show("Ban co chac chan muon cap nhat khong", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {

                                                    bool gioitinnh;
                                                    if (RadioNam.Checked)
                                                    {
                                                        gioitinnh = true;
                                                    }
                                                    else
                                                    {
                                                        gioitinnh = false;
                                                    }
                                                    var caiCapNhat = _context.Users.FirstOrDefault(x => x.IdUser == a);
                                                    caiCapNhat.UserName = txtNameUser.Text;
                                                    caiCapNhat.UserAddress = txtAddress.Text;
                                                    caiCapNhat.SDT = txtPhone.Text;
                                                    caiCapNhat.UsesGend = gioitinnh;
                                                    caiCapNhat.UserAge = DateUser.Value.Date;
                                                    var DeleteAc = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == caiCapNhat.IdTaiKhoan);
                                                    DeleteAc.IdRoles = ComBoVaiTro.SelectedIndex + 1;
                                                    _context.SaveChanges();
                                                    HienThiView();
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
    

