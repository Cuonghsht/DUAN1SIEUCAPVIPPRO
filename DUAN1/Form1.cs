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
         private QuanLyHieuThuocEntities4 dbContext;

        public Form1()
        {
            InitializeComponent();
            dbContext = new QuanLyHieuThuocEntities4();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //sadlkfhjksldasadlkfjskadlfjisladfhjlksdahfjkosdahf
        // Comment 2, etst
        private void btnSigin_Click(object sender, EventArgs e)
        {
            var DangNhap = dbContext.TaiKhoans.FirstOrDefault(x => x.NameTaiKhoan == txtTaiKhoan.Text && x.PassWordd == txtMatKhau.Text);
            if (txtMatKhau.Text==""||txtTaiKhoan.Text=="")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                if (DangNhap == null || DangNhap.idtrangthai==2)
                {
                    MessageBox.Show("Đăng nhập thất bại do tài khoản không chính xác hoặc tài khoản của bạn đã dừng hoạt động", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    var idd = DangNhap.IdTaiKhoan;
                    if (DangNhap.IdRoles == 2)
                    {
                        MessageBox.Show("Đây là chủ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmAdmin admin = new FrmAdmin(dbContext, idd);
                        admin.FormClosed += (a, b) => this.Close();
                        admin.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đây là nhân viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
