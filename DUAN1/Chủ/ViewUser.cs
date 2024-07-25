using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1.Chủ
{
    public partial class ViewUser : Form
    {
        private readonly QuanLyHieuThuocEntities1 _context;
        public ViewUser(QuanLyHieuThuocEntities1 context)
        {
            InitializeComponent();
            _context = context;
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
                    if (MessageBox.Show("Ban co chac chan muon xoa nguoi nay ra khoi ung dung khong", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _context.Users.Remove(delete);
                        _context.TaiKhoans.Remove(DeleteAc);
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
    }
}
