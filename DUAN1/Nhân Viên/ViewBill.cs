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

namespace DUAN1.Nhân_Viên
{
    public partial class ViewBill : Form
    {
        private readonly QuanLyHieuThuocEntities5 _context;
        public int ids;
        public int idddd;
        public int Id;
        public ViewBill(QuanLyHieuThuocEntities5 context, int idd, int iddd)
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
        }

        public void Hienthi()
        {
            var viewwwwww = (from a in _context.Users
                             join b in _context.Detailedbills on a.IdUser equals b.IdUser
                             join c in _context.Bills on b.BillId equals c.BillId
                             join d in _context.Statusses on c.StatusId equals d.StatusId
                             join v in _context.Vouchers on c.IdVoucher equals v.IdVoucher into vouchers
                             from v in vouchers.DefaultIfEmpty()
                             select new
                             {
                                 IdBills = c.BillId,
                                 NameUser = a.UserName,
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

        private void groupBox1_Enter(object sender, EventArgs e)
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
    }
}
