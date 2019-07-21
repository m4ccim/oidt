using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using OidtWpf.DataModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OidtWpf.ViewModels
{
    public class Window2Model : INotifyPropertyChanged
    {
        DataContext context = new DataContext();
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }


        }

        public Window2Model(int parameter)
        {
            PlotModel = new PlotModel();
            SetUpModel();
            LoadData(parameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public void LoadData(int parameter)
        {

            
            switch (parameter)
            {
                case 1:

                    var itemsDistribution = new BarSeries
                    {
                        StrokeThickness = 2,
                        Title = string.Format("item distribution"),
                        
                    };
                    var items = context.ItemStatDays.Where(x => x.Date == new DateTime(2018, 01, 30)).OrderBy(x => x.CurrencySpent).Take(30).ToList();
                    if (PlotModel.Axes.Count > 0)
                    {
                        PlotModel.Axes.RemoveAt(0);
                        PlotModel.Axes.RemoveAt(1);
                    }
                    PlotModel.Axes.Add(new CategoryAxis { Key = "keys", Position = AxisPosition.Left, ItemsSource = items.Select(x => x.Name).ToList() });

                    var valueAxis = new LinearAxis { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
                    PlotModel.Axes.Add(valueAxis);
                    foreach (var data in items)
                    {
                        itemsDistribution.Items.Add(new BarItem(data.CurrencySpent));
                        
                    }
                    PlotModel.Series.Add(itemsDistribution);
       
                    break;

            }


        }

        public void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Inside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var startdate = new DateTime(2018, 01, 01);
            var enddate = new DateTime(2018, 01, 31);

            var minvalue = DateTimeAxis.ToDouble(startdate);
            var maxvalue = DateTimeAxis.ToDouble(enddate);






        }


    }
}
