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
    public partial class change_Password : Form
    {
        public readonly QuanLyHieuThuocEntities5 _context;
        public int idtk;

        public change_Password(QuanLyHieuThuocEntities5 context, int iddtk)
        {
            _context = context;
            idtk = iddtk;
            InitializeComponent();
            var taikhoan = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == idtk);
            txtAccountName.Text = taikhoan.NameTaiKhoan.ToString();
            txtAccountName.Enabled = false;
        }

        private void change_Password_Load(object sender, EventArgs e)
        {

        }

        private void btnDoi_Click(object sender, EventArgs e)
        {
            var taikhoan = _context.TaiKhoans.FirstOrDefault(x => x.IdTaiKhoan == idtk);

            if (txtAccountName.Text == "" || txtPassword.Text == "" || txtPasswordNew.Text == "" || txtPasswordNewPlus.Text == "")
            {
                MessageBox.Show("Ban can nhap thong tin day du len cac o ");
            }
            else
            {
                if (taikhoan.PassWordd != txtPassword.Text.ToString())
                {
                    MessageBox.Show("Mat khau khong chinh xac");
                }
                else
                {
                    if (txtPasswordNew.TextLength < 10)
                    {
                        MessageBox.Show("Mat khau phai lon hon 10 ki tu");
                    }
                    else
                    {
                        if (txtPasswordNew.Text.Any(char.IsDigit) == false || txtPasswordNew.Text.Any(char.IsLetter) == false)
                        {
                            MessageBox.Show("mat khau moi phai bao gon ca so va ki tu");
                        }
                        else
                        {
                            if (txtPasswordNew.Text != txtPasswordNewPlus.Text)
                            {
                                MessageBox.Show("Mat khau nhap lai chua chinh xac");
                            }
                            else
                            {
                                if (MessageBox.Show("ban co chac chan muon doi mat khau khong", "xac Nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    taikhoan.PassWordd = txtPasswordNew.Text.ToString();
                                    _context.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPasswordNew.UseSystemPasswordChar = true;
                txtPasswordNewPlus.UseSystemPasswordChar = true;
            }
            else
            {
                txtPasswordNew.UseSystemPasswordChar = false;
                txtPasswordNewPlus.UseSystemPasswordChar = false;
            }
        }
    }
}
