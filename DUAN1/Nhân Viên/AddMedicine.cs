﻿using Guna.UI2.WinForms;
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
	public partial class AddMedicine : Form
	{
		private readonly QuanLyHieuThuocEntities6 _context;
		public AddMedicine(QuanLyHieuThuocEntities6 context)
		{
			_context = context;
			InitializeComponent();
			addCateUnit();
		}
		public void Reload()
		{
			TxtName.Text = "";
			Date.Value = DateTime.Now;
			TxtPrice.Text = "";
			TxtQuantity.Text = "";
			ComboUnit.SelectedIndex = 0;
			ComboDanhMuc.SelectedIndex = 0;
		}
		public void addCateUnit()
		{
			var AddUnit = (from i in _context.Units select i).ToList();
			var AddCate = (from a in _context.Categories select a).ToList();
			ComboDanhMuc.Items.Clear();
			ComboUnit.Items.Clear();
			foreach (var i in AddUnit)
			{
				ComboUnit.Items.Add(i.UnitName);
			}
			foreach (var r in AddCate)
			{
				ComboDanhMuc.Items.Add(r.CategoryName);
			}
		}
		private void btnSigin_Click(object sender, EventArgs e)
		{
			decimal price;
			decimal.TryParse(TxtPrice.Text, out price);
			int Quantity;
			int.TryParse(TxtQuantity.Text, out Quantity);
			if (TxtName.Text == "" || TxtPrice.Text == "" || TxtQuantity.Text == "")
			{
				MessageBox.Show("Bạn cần nhập đầy đủ thông tin ");
				return;
			}
			else if (Date.Value < DateTime.Now)
			{
				MessageBox.Show("Bạn không thể thêm sản phẩm đã hết hạn sử dụng");
			}
			else if ((TxtPrice.Text.All(char.IsDigit) == false || TxtQuantity.Text.All(char.IsDigit) == false))
			{
				MessageBox.Show("Giá tiền  và số lượng không được tồn tại kí tự ");
			}
			else if (price<0||Quantity<0)
			{
				MessageBox.Show("Giá và số lượng phải lớn hơn 0");
			}
			else if (!int.TryParse(TxtQuantity.Text,out Quantity))
			{
				MessageBox.Show("Số lượng bắt buộc phải là 1 số nguyên");
			}
			else
			{
				var add = new Product
				{
					ProductName = TxtName.Text,
					ProductPrice = Convert.ToInt32(TxtPrice.Text),
					ProductQuantity = Convert.ToInt32(TxtQuantity.Text),
					IdUnit = ComboUnit.SelectedIndex + 1,
					ProductExpiry = Date.Value.Date,
					IdCategory = ComboDanhMuc.SelectedIndex + 1,
					StatusPr = 1
				};
				MessageBox.Show("Thêm thành công sản phẩm");
				_context.Products.Add(add);
				_context.SaveChanges();
				Reload();
			}
		}
		private void AddMedicine_Load(object sender, EventArgs e)
		{
		}
		private void guna2Button1_Click(object sender, EventArgs e)
		{
			Reload();
		}
		private void guna2Button2_Click(object sender, EventArgs e)
		{

			if (ComboDanhMuc.Text == "")
			{
				MessageBox.Show("Nhập đầy đủ thông tin");
			}
			else
			{
				var checkcate = _context.Categories.FirstOrDefault(x => x.CategoryName == ComboDanhMuc.Text);
				if (checkcate != null)
				{
					MessageBox.Show("Danh mục này đã tồn tại ");
				}
				else
				{
					Category category = new Category
					{
						CategoryName = ComboDanhMuc.Text,
					};
					_context.Categories.Add(category);
					_context.SaveChanges();

					addCateUnit();
					MessageBox.Show("thêm thành công");
				}
			}
		}
		private void guna2Button3_Click(object sender, EventArgs e)
		{
			if (ComboUnit.Text == "")
			{
				MessageBox.Show("nhập đầy đủ thông tin");
			}
			else
			{
				var checkunit = _context.Units.FirstOrDefault(x => x.UnitName == ComboUnit.Text);
				if (checkunit != null)
				{
					MessageBox.Show("Đơn vị này đã tồn tại");
				}
				else {
					Unit unit = new Unit
					{
						UnitName = ComboUnit.Text,
					};
					_context.Units.Add(unit);
					_context.SaveChanges();
					MessageBox.Show("thêm thành thành công");
					addCateUnit();
				}
			}
		}
		private void ComboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
		private void ComboDanhMuc_TextChanged(object sender, EventArgs e)
		{
		}
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
	}
}
