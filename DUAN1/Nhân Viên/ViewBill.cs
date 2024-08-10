using DUAN1.Chủ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DUAN1.Nhân_Viên
{
    public partial class ViewBill : Form
    {
        private readonly QuanLyHieuThuocEntities6 _context;
        public int ids;
        public int idddd;
        public int Id;
        public ViewBill(QuanLyHieuThuocEntities6 context, int idd, int iddd)
        {
            _context = context;
            ids = idd;
            idddd = iddd;
            InitializeComponent();
           
            var billsWithoutDetails = _context.Bills.Where(b => !_context.Detailedbills.Any(d => d.BillId == b.BillId)).ToList();
            foreach (var bill in billsWithoutDetails)
            {
                _context.Bills.Remove(bill);
            }
            Hienthi();
            var combo = _context.Statusses.ToList();
            foreach (var status in combo)
            {
                combosheacher.Items.Add(status.StatusName);
            }
        }

        public void Hienthi()
        {
            var viewwwwww = (from a in _context.Users
                             join b in _context.Detailedbills on a.IdUser equals b.IdUser
                             join c in _context.Bills on b.BillId equals c.BillId
                             join d in _context.Statusses on c.StatusId equals d.StatusId
                             join v in _context.Vouchers on c.IdVoucher equals v.IdVoucher into vouchers
                             from v in vouchers 
                             join e in _context.Customers on c.IdCustomer equals e.IdCustomer
                             select new
                             {
                                 IdBills = c.BillId,
                                 NameCustomer = e.CustomerName,
                                 Price = c.PriceBill,
                                 Date = c.DateBill,
                                 StaTus = d.StatusName,
                                 VoucherName = v != null ? v.NameVoucher : null,
                             }).Distinct().ToList();
            dataGridView1.DataSource = viewwwwww;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void ChiTiet_Click(object sender, EventArgs e)
        {

            SellMedicine f = new SellMedicine(_context, ids, Id);
            f.FormClosed += (a, b) => this.Show();
            f.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var a = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(a.Cells["IdBills"].Value.ToString());

            }
            else
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Hienthi();
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int? trangthai = combosheacher.SelectedIndex == -1 ? (int?)null : combosheacher.SelectedIndex;
            DateTime? searchDate = ddatesh.Checked ? (DateTime?)ddatesh.Value.Date : null;
            string searchText = txtsearche.Text.Trim();

            var viewwwwww = (from a in _context.Users
                             join b in _context.Detailedbills on a.IdUser equals b.IdUser
                             join c in _context.Bills on b.BillId equals c.BillId
                             join d in _context.Statusses on c.StatusId equals d.StatusId
                             join v in _context.Vouchers on c.IdVoucher equals v.IdVoucher into vouchers
                             from v in vouchers.DefaultIfEmpty()
                             join r in _context.Customers on c.IdCustomer equals r.IdCustomer
                             where (trangthai == null || c.StatusId == trangthai +1)
                             && (searchDate == null || c.DateBill >= searchDate.Value)
                             && (string.IsNullOrEmpty(searchText) || r.CustomerName.Contains(searchText))
                             select new
                             {
                                 IdBills = c.BillId,
                                 NameCustomer = r.CustomerName,
                                 Price = c.PriceBill,
                                 Date = c.DateBill,
                                 StaTus = d.StatusName,
                                 VoucherName = v != null ? v.NameVoucher : null,
                             }).Distinct().ToList();

            dataGridView1.DataSource = viewwwwww;

        }

        private void ViewBill_Load(object sender, EventArgs e)
        {

        }
    }
}
