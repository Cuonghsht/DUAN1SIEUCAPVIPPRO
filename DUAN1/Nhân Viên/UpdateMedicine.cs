using LiveCharts.Wpf;
using LiveCharts;
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
    public partial class UpdateMedicine : Form
    {
        public readonly QuanLyHieuThuocEntities6 aa;
        public UpdateMedicine(QuanLyHieuThuocEntities6 context)
        {
            aa = context;
            InitializeComponent();
        }



        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var bills = aa.Bills
                .Where(x => x.StatusId == 1)
                .Select(x => new
                {
                    x.DateBill,
                    x.PriceBill
                })
                .ToList();

            var billPrices = bills
                .GroupBy(x => new { x.DateBill.Year, x.DateBill.Month })
                .Select(g => new
                {
                    YearMonth = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalPrice = g.Sum(x => x.PriceBill)
                })
                .OrderBy(x => x.YearMonth)
                .ToList();

            var series = new ColumnSeries
            {
                Title = "Tổng Giá Trị Hóa Đơn",
                Values = new ChartValues<decimal>(billPrices.Select(bp => bp.TotalPrice))
            };

            var labels = billPrices.Select(bp => bp.YearMonth.ToString("MMM yyyy")).ToArray();

            cartesianChart1.Series = new SeriesCollection { series };
            cartesianChart1.AxisX = new AxesCollection
{
                new Axis
                {
                    Title = "Tháng",
                    Labels = labels
                }
            };
            cartesianChart1.AxisY = new AxesCollection
            {
                new Axis
                {
                    Title = "Tổng Giá Trị (VND)",
                    LabelFormatter = value => value.ToString("C")
                }
            };

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            // Xác định khoảng thời gian cần tìm kiếm
            var startDate = new DateTime(batdau.Value.Year, batdau.Value.Month, 1);
            var endDate = new DateTime(ketthuc.Value.Year, ketthuc.Value.Month, DateTime.DaysInMonth(ketthuc.Value.Year, ketthuc.Value.Month));
            if (endDate < startDate)
            {
                MessageBox.Show("Ngày bắt đầu phải bé hơn ngày kết thúc");
                return;
            }
            else
            {

                var bills = aa.Bills
                    .Where(x => x.StatusId == 1 && x.DateBill >= startDate && x.DateBill <= endDate)
                    .Select(x => new
                    {
                        x.DateBill,
                        x.PriceBill
                    })
                    .ToList();

                var billPrices = bills
                    .GroupBy(x => new { x.DateBill.Year, x.DateBill.Month })
                    .Select(g => new
                    {
                        YearMonth = new DateTime(g.Key.Year, g.Key.Month, 1),
                        TotalPrice = g.Sum(x => x.PriceBill)
                    })
                    .OrderBy(x => x.YearMonth)
                    .ToList();

                var series = new ColumnSeries
                {
                    Title = "Tổng Giá Trị Hóa Đơn",
                    Values = new ChartValues<decimal>(billPrices.Select(bp => bp.TotalPrice))
                };

                var labels = billPrices.Select(bp => bp.YearMonth.ToString("MMM yyyy")).ToArray();

                    cartesianChart1.Series = new SeriesCollection { series };
                    cartesianChart1.AxisX = new AxesCollection
                {
                        new Axis
                        {
                            Title = "Tháng",
                            Labels = labels,
                            LabelFormatter = value => new DateTime((int)value, 1, 1).ToString("MMM yyyy") // Format ngày tháng
                        }
                };
                        cartesianChart1.AxisY = new AxesCollection
                        {
                            new Axis
                            {
                                Title = "Tổng Giá Trị (VND)",
                                LabelFormatter = value => value.ToString("C")
                            }
                        };
              }
                }

        private void reload_Click(object sender, EventArgs e)
        {
           // elementHost1_ChildChanged( sender);
        }
    }
}