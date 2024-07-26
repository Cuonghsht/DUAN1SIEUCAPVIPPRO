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
    public partial class SellMedicine : Form
    {
        private readonly QuanLyHieuThuocEntities1 _context;
        private int _id;
        public SellMedicine(QuanLyHieuThuocEntities1 context, int id)
        {
            InitializeComponent();
            _context = context;
            this._id = id;
            var a = _id;
            ListProduct();
        }
         public void ListProduct()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("ID", 30);
            listView1.Columns.Add("Name", 170);
            listView1.Columns.Add("Price", 130);

            var list = (from a in _context.Products select a).ToList();
            foreach (var a in list)
            {
                ListViewItem item = new ListViewItem(a.ProductId.ToString());

                item.SubItems.Add(a.ProductName);

                item.SubItems.Add(a.ProductPrice.ToString());

                listView1.Items.Add(item);
            }
        }
        private void SellMedicine_Load(object sender, EventArgs e)
        {

        }
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem listViewItem = listView1.SelectedItems[0];
                IdPro.Text = listViewItem.Text;
                int id;
                int.TryParse(listViewItem.Text, out id);
                var ThongTinSp = _context.Products.FirstOrDefault(x => x.ProductId == id);
                NamePro.Text = ThongTinSp.ProductName;
                PricePro.Text = ThongTinSp.ProductPrice.ToString();
                DateTime HSDpro;
                DateTime.TryParse((ThongTinSp.ProductExpiry.ToString()), out HSDpro);
                HSD.Value = HSDpro;
                int sl;
                int.TryParse(txtSl.Text, out sl);
                if (ThongTinSp.ProductQuantity < sl)
                {
                    txtSl.BackColor = Color.Red;
                    danger.Visible = true;
                }
                else
                {
                    danger.Visible = false;
                }
                decimal gia = ThongTinSp.ProductPrice;
                decimal tong = sl * gia;
                txtGia.Text = tong.ToString();


            }
        }
        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtGia_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
