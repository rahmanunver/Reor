using Microsoft.WindowsAzure.MobileServices;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePage : ContentPage
    {
        IMobileServiceTable<Models.Item> ItemsTable = App.MobileService.GetTable<Models.Item>();

        public PlotModel ChartModel { get; set; }

        public TimePage()
        {
            InitializeComponent();
            SegControl.ValueChanged += this.SegControl_ValueChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.PrepareData();
        }

        async void PrepareData()
        {
            var items = await this.GetItems();
            this.ChartModel = this.CreatePieChart(items);
            this.myChart.Model = this.ChartModel;
            this.listItems.ItemsSource = items;
        }

        void SegControl_ValueChanged(object sender, EventArgs e)
        {
            this.PrepareData();
        }

        private async Task<List<Models.Item>> GetItems()
        {
            string userId = Settings.UserId;

            var itemQuery = ItemsTable.Where(x => x.UserId == userId & x.IsTime);
            switch (SegControl.SelectedSegment)
            {
                case 0:
                    itemQuery = itemQuery.Where(x => x.CreatedAt <= DateTime.Now & x.CreatedAt > DateTime.Now.AddDays(-1));
                    break;
                case 1:
                    itemQuery = itemQuery.Where(x => x.CreatedAt <= DateTime.Now & x.CreatedAt > DateTime.Now.AddDays(-7));
                    break;
                case 2:
                    itemQuery = itemQuery.Where(x => x.CreatedAt <= DateTime.Now & x.CreatedAt > DateTime.Now.AddMonths(-1));
                    break;
                default:
                    itemQuery = itemQuery.Where(x => x.CreatedAt <= DateTime.Now & x.CreatedAt > DateTime.Now.AddDays(-1));
                    break;
            }

            return await itemQuery.ToListAsync();

        }

        private PlotModel CreatePieChart(List<Models.Item> items)
        {
            
            var model = new PlotModel { };

            var ps = new PieSeries
            {
                TextColor = OxyColor.FromRgb(255, 255, 255)
            };

            foreach (var item in items)
            {
                ps.Slices.Add(new PieSlice(item.Name, item.Expense));
            }

            model.Series.Add(ps);
            return model;
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            txtTime.Text = "";
            txtItemName.Text = "";
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var item = new Models.Item()
            {
                Name = txtItemName.Text,
                Expense = int.Parse(txtTime.Text),
                IsTime = true,
                UserId = Settings.UserId
            };

            await ItemsTable.InsertAsync(item);

            if (!string.IsNullOrEmpty(item.Id))
            {
                this.PrepareData();
            }
            else
            {
                await DisplayAlert("Reor", "Entries are empty or incomplete", "Ok");
            }
        }

       

       
    }
}