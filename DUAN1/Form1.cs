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

namespace DUAN1
{
    public partial class Form1 : Form
    {
         private QuanLyHieuThuocEntities1 dbContext;

        public Form1()
        {
            InitializeComponent();
            dbContext = new QuanLyHieuThuocEntities1();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //sadlkfhjksldasadlkfjskadlfjisladfhjlksdahfjkosdahf
        private void btnSigin_Click(object sender, EventArgs e)
        {
            var DangNhap = dbContext.TaiKhoans.FirstOrDefault(x => x.NameTaiKhoan == txtTaiKhoan.Text && x.PassWordd == txtMatKhau.Text);
            if (txtMatKhau.Text==""||txtTaiKhoan.Text=="")
            {
                MessageBox.Show("Ban can nhap day du thong tin");
            }
            else {
                if (DangNhap == null)
                {
                    MessageBox.Show("Dang Nhap that bai do tai khoan khong chinh xac");
                }
                else
                {
                    var idd = DangNhap.IdTaiKhoan;
                    if (DangNhap.IdRoles == 2)
                    {
                        MessageBox.Show("Day la chu");
                        FrmAdmin admin = new FrmAdmin(dbContext, idd);
                        admin.FormClosed += (a, b) => this.Close();
                        admin.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("day la nhan vien");
                        Users us = new Users(dbContext,idd);
                        us.FormClosed += (a, b) => this.Close();
                        us.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtMatKhau.Text = "";
            txtTaiKhoan.Text = "";
        }
    }
}
