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
        private readonly QuanLyHieuThuocEntities6 _context;
        public ViewUser(QuanLyHieuThuocEntities6 context)
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
                               idtk = b.IdTaiKhoan
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
                MessageBox.Show("Bạn cần chọn người cụ thể ");
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
                MessageBox.Show(" Bạn cần chọn đúng người để cấm ");
            }
            else
            {
                var delete = _context.Users.FirstOrDefault(x => x.IdUser == a);
                var DeleteAc = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == delete.IdTaiKhoan);
                if (delete == null)
                {
                    MessageBox.Show(" Người này không tồn tại");
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn tài khoản này dưng hoạt động không ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                MessageBox.Show("Bạn càn nhập đầy đủ thông tin vào các ô");
            }
            else
            {
                if (RadioNam.Checked == false && RadioNu.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn giới tính ");
                }
                else
                {
                    if (DateUser.Value.Year > Tuoi18)
                    {
                        MessageBox.Show("Người dùng phải lớn hơn 18 tuổi");
                    }
                    else
                    {
                        if (ComBoVaiTro.SelectedIndex < 0)
                        {
                            MessageBox.Show("Vui lòng chọn vai trò");
                        }
                        else
                        {
                            if (txtPhone.TextLength != 10)
                            {
                                MessageBox.Show("Số điện thoại bao gồm 10 kí tự và bắt đầu bằng số 0");
                            }
                            else
                            {
                                if (txtPhone.Text.Substring(0, 1) != "0")
                                {
                                    MessageBox.Show("Số điện thoại bắt đầu bằng số  0");

                                }
                                else
                                {

                                    if (txtPhone.Text.Any(char.IsDigit) == false)
                                    {
                                        MessageBox.Show("Số điện thoại phải là dãy số từ 0-9");
                                    }
                                    else
                                    {
                                        if (txtEmail.TextLength < 11)
                                        {
                                            MessageBox.Show("Email chưa đúng định dạng");

                                        }
                                        else
                                        {
                                            var dodai = txtEmail.TextLength;

                                            var loai = dodai - 10;
                                            var sosanh = txtEmail.Text.Substring(loai, 10);
                                            if (sosanh != "@gmail.com")
                                            {
                                                MessageBox.Show("Đuôi phải là  @gmail.com");
                                            }

                                            else
                                            {


                                                if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật không ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void View_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var trangthai = (from a in _context.TaiKhoans
                             join b in _context.Users on a.IdTaiKhoan equals b.IdTaiKhoan
                             where a.idtrangthai == 2
                             select new
                             {
                                 tk = a,
                                 us = b
                             }
                             ).ToList();
            List<int> bicam = new List<int>();
            foreach (var i in trangthai)
            {
                if (i.tk.idtrangthai == 2)
                {
                    bicam.Add(i.us.IdUser);
                }
            }
            var dong = View.Rows[e.RowIndex];
            var y = Convert.ToInt32(dong.Cells["IdUser"].Value.ToString());
            foreach (var i in bicam)
            {
                if (y == i)
                {
                    View.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                }
            }
        }
    }
}


