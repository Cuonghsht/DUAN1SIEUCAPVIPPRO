using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1.Chủ
{
    public partial class Frofile : Form
    {
        public readonly QuanLyHieuThuocEntities5 _context;
        public int id;
        public Frofile(QuanLyHieuThuocEntities5 context, int _idd)
        {
            InitializeComponent();
            _context = context;
            this.id = _idd;
        }

        public void DayData()
        {

        }
        private void Frofile_Load(object sender, EventArgs e)
        {
            var hienthi = (from a in _context.Roles
                           join b in _context.TaiKhoans on a.IdRoles equals b.IdRoles
                           join c in _context.Users on b.IdTaiKhoan equals c.IdTaiKhoan
                           where(c.IdTaiKhoan==id)
                           select new
                           {
                               users = c,
                               nameRole = a.NameRoles
                           }).FirstOrDefault();

            txtdiachi.Text = hienthi.users.UserAddress.ToString();
            txtMail.Text = hienthi.users.Email.ToString();
            txtName.Text = hienthi.users.UserName.ToString();
            txtphone.Text = hienthi.users.SDT.ToString();
            DateTime dateee;
            DateTime.TryParse(hienthi.users.UserAge.ToString(), out dateee);
            Date.Value = dateee;
           

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
