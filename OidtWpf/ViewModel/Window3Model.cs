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
    public class Window3Model : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
            
            
        }

        public Window3Model(int parameter)
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
        private void LoadData(int parameter)
        {

            DataContext context = new DataContext();
            switch (parameter)
            {
                case 1:

                    var ExchangeRate = new LineSeries
                    {
                        StrokeThickness = 2,
                        MarkerSize = 3,
                        CanTrackerInterpolatePoints = false,
                        Title = string.Format("ExchangeRate"),
                        Smooth = false,
                    };
                    var NewUsersSeries = new LineSeries
                    {
                        StrokeThickness = 3,
                        MarkerSize = 4,
                        CanTrackerInterpolatePoints = false,
                        Title = string.Format("NewUserSeries"),
                        Smooth = false,
                    };

                    foreach (var data in context.DayStats.ToList())
                    {
                        ExchangeRate.Points.Add(new DataPoint(Axis.ToDouble(data.Date), data.ExchangeRate));
                        NewUsersSeries.Points.Add(new DataPoint(Axis.ToDouble(data.Date), data.NewUsers));
                    }
                    PlotModel.Series.Add(ExchangeRate);
                    //PlotModel.Series.Add(NewUsersSeries);
                    break;

                case 2:
                    var Revenue = new LineSeries
                    {
                        StrokeThickness = 2,
                        MarkerSize = 3,
                        CanTrackerInterpolatePoints = false,
                        Title = string.Format("Revenue"),
                        Smooth = false,
                    };
                    var SoldUsd = new LineSeries
                    {
                        StrokeThickness = 3,
                        MarkerSize = 4,
                        CanTrackerInterpolatePoints = false,
                        Title = string.Format("Sold Items Cost in USD"),
                        Smooth = false,
                    };

                    foreach (var data in context.Prediction.ToList())
                    {
                        Revenue.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.Revenue));
                        SoldUsd.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.SoldUSD));
                    }
                    PlotModel.Series.Add(Revenue);
                    PlotModel.Series.Add(SoldUsd);
                    break;
                case 3:
                    var soldAmount = new LineSeries
                    {
                        StrokeThickness = 2,
                        MarkerSize = 3,
                        CanTrackerInterpolatePoints = false,
                        Title = string.Format("Amount of sold Items"),
                        Smooth = false,
                    };
                    foreach (var data in context.Prediction.ToList())
                    {
                        soldAmount.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.SoldAmount));
                    }
                    PlotModel.Series.Add(soldAmount);
                    break;

            //    case 1:
                    
            //        var DauSeries = new LineSeries
            //        {
            //            StrokeThickness = 2,
            //            MarkerSize = 3,
            //            CanTrackerInterpolatePoints = false,
            //            Title = string.Format("DAU"),
            //            Smooth = false,
            //        };
            //        var NewUsersSeries = new LineSeries
            //        {
            //            StrokeThickness = 3,
            //            MarkerSize = 4,
            //            CanTrackerInterpolatePoints = false,
            //            Title = string.Format("NewUserSeries"),
            //            Smooth = false,
            //        };

            //        foreach (var data in context.Prediction.ToList())
            //        {
            //            DauSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.DAU));
            //            NewUsersSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.NewUsers));
            //        }
            //        PlotModel.Series.Add(DauSeries);
            //        PlotModel.Series.Add(NewUsersSeries);
            //        break;

            //case 2:
            //        var Revenue = new LineSeries
            //        {
            //            StrokeThickness = 2,
            //            MarkerSize = 3,
            //            CanTrackerInterpolatePoints = false,
            //            Title = string.Format("Revenue"),
            //            Smooth = false,
            //        };
            //        var SoldUsd = new LineSeries
            //        {
            //            StrokeThickness = 3,
            //            MarkerSize = 4,
            //            CanTrackerInterpolatePoints = false,
            //            Title = string.Format("Sold Items Cost in USD"),
            //            Smooth = false,
            //        };

            //        foreach (var data in context.Prediction.ToList())
            //        {
            //            Revenue.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.Revenue));
            //            SoldUsd.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.SoldUSD));
            //        }
            //        PlotModel.Series.Add(Revenue);
            //        PlotModel.Series.Add(SoldUsd);
            //        break;
            //case 3:
            //        var soldAmount = new LineSeries
            //        {
            //            StrokeThickness = 2,
            //            MarkerSize = 3,
            //            CanTrackerInterpolatePoints = false,
            //            Title = string.Format("Amount of sold Items"),
            //            Smooth = false,
            //        };
            //        foreach (var data in context.Prediction.ToList())
            //        {
            //            soldAmount.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.SoldAmount));
            //        }
            //        PlotModel.Series.Add(soldAmount);
            //        break;
            }


        }

        public void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var startdate = new DateTime(2018, 01, 01);
            var enddate = new DateTime(2018, 01, 31);

            var minValue = DateTimeAxis.ToDouble(startdate);
            var maxValue = DateTimeAxis.ToDouble(enddate);

            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "dd/MM/yy" });

            var valueAxis = new LinearAxis { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);
        }
    }
}
