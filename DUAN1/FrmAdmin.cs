using DUAN1.Chủ;
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
    public partial class FrmAdmin : Form
    {
        private readonly QuanLyHieuThuocEntities1 _dbcontext;
        public int _idd;
        public FrmAdmin(  QuanLyHieuThuocEntities1 dbcontext,int idd)
        {
            _idd = idd;
            _dbcontext= dbcontext;
            InitializeComponent();

            var a = _idd;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUser f = new AddUser(_dbcontext);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();
        }

        private void btnViewUser_Click(object sender, EventArgs e)
        {
            ViewUser f = new ViewUser(_dbcontext);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
            Form1 f = new Form1();
            f.FormClosed += (a, b) => this.Close();
            f.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Frofile f = new Frofile(_dbcontext,_idd);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();
        }
    }
}
