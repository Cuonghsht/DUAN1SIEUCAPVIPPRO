using DUAN1.Chủ;
using DUAN1.Nhân_Viên;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1
{
    public partial class Users : Form
    {
        private readonly QuanLyHieuThuocEntities1 aa;
        private int idd;
       
        public Users(QuanLyHieuThuocEntities1 dbContext,int id)
        {
            InitializeComponent();
            idd = id;
            aa = dbContext;
            
            
   
        }
        
        private void Users_Load(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddMedicine f = new AddMedicine(aa);
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

        private void btnViewUser_Click(object sender, EventArgs e)
        {

            ViewMedicine f = new ViewMedicine(aa);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();
        }
       
        private void btnDashbord_Click(object sender, EventArgs e)
        {
            
            SellMedicine f = new SellMedicine(aa, idd);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();

        }
    }
}
