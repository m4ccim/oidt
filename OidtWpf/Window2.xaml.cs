using OidtWpf.DataModel;
using OidtWpf.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OidtWpf
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2Model viewModel;
        public DataContext context = new DataContext();
        public Window2()
        {
            viewModel = new Window2Model(1);
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)(sender as Slider).Value;


            var itemsDistribution = new BarSeries
            {
                StrokeThickness = 2,
                Title = string.Format("item distribution"),

            };
            var date = new DateTime(2018, 01, value + 1);
            var items = context.ItemStatDays.Where(x => x.Date == date).OrderBy(x => x.CurrencySpent).Take(30).ToList();
            if (viewModel.PlotModel.Axes.Count > 0)
            {
                viewModel.PlotModel.Axes.Clear();
                viewModel.PlotModel.Series.RemoveAt(0);
            }
            viewModel.PlotModel.Axes.Add(new CategoryAxis { Key = "keys", Position = AxisPosition.Left, ItemsSource = items.Select(x => x.Name).ToList() });

            var valueAxis = new LinearAxis { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            viewModel.PlotModel.Axes.Add(valueAxis);
            foreach (var data in items)
            {
                itemsDistribution.Items.Add(new BarItem(data.CurrencySpent));

            }
            viewModel.PlotModel.Series.Add(itemsDistribution);
            viewModel.PlotModel.InvalidatePlot(true);
            
        }
    }
}
