using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OidtWpf.DataModel;
using System.Transactions;
using Z.BulkOperations;
using System.Data.Common;
using System.Data;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OidtWpf.ViewModels;

namespace OidtWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private MainWindowModel viewModel;
        private int current = 1;
        private int selectedMenu;
        private int[] ages;
        private string[] countries;
        private DataContext context = new DataContext();

        public MainWindow()
        {
            //viewModel = new MainWindowModel(1);
            DataContext = viewModel;

            InitializeComponent();
            //Window1 win1 = new Window1();
            //win1.Show();
            //Window2 win2 = new Window2();
            //win2.Show();
            //Window3 win3 = new Window3();
            //win3.Show();

            //MyLoadJson();
            //UpdateDayStat();
            //UpdateItemStatDay();
            //UpdateItemStat();
            //UpdateStageStatDay();
            //UpdateStageStat();
            //Predict();


        }

        public void MyLoadJson()
        {
            List<Event> items = new List<Event>();
            string[] filePaths = Directory.GetFiles(@"C:\Users\maxim\Desktop\OIDt\AGDRGenerator\", "*.json");
            foreach (string path in filePaths)
            {
                context.Dispose();
                context = new DataContext();
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                Console.WriteLine(path);
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    List<EventNested> events = JsonConvert.DeserializeObject<List<EventNested>>(json);
                    //events = events.Take((int)(events.Count * 0.1)).ToList();
                    items = events.Select(x => new Event
                    {
                        date = x.date,
                        udid = x.udid,
                        event_id = x.event_id,
                        parametersAge = x.Parameters.age,
                        parametersCountry = x.Parameters.country,
                        parametersGender = x.Parameters.gender,
                        parametersIncome = x.Parameters.income,
                        parametersItem = x.Parameters.item,
                        parametersName = x.Parameters.name,
                        parametersPrice = x.Parameters.price,
                        parametersStage = x.Parameters.stage,
                        parametersTime = x.Parameters.time,
                        parametersWin = x.Parameters.win
                    }).ToList();
                }

                context.BulkInsert(items, options => options.AutoMapOutputDirection = false);
            }
        }

        public class DeyStat
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int DAU { get; set; }
            public int NewUsers { get; set; }
            public double Revenue { get; set; }
            public double ExchangeRate { get; set; }
        }
        public void UpdateDayStat()
        {
            var dates = context.Events.GroupBy(x => x.date).Select(x => x.Key).OrderBy(x => x.Day).ToList();
            dates = dates.ToList();

            List<DeyStat> dayStats = new List<DeyStat>();

            dayStats = context.Events.GroupBy(x => x.date).Select(g => new DeyStat
            {

                Date = g.Select(u => u.date).FirstOrDefault(),
                DAU = g.Where(u => u.event_id == 1).GroupBy(x => x.udid).Count(),
                NewUsers = g.Where(u => u.event_id == 2).GroupBy(x => x.udid).Count(),
                Revenue = g.Where(u => u.event_id == 6).Select(x => x.parametersPrice).Sum(),
                ExchangeRate = g.Where(u => u.event_id == 6).Select(x => x.parametersPrice).Sum()
                / g.Where(u => u.event_id == 6).Select(x => x.parametersIncome).Sum(),
        }).ToList();


            //dayStat.Date = date;
            //dayStat.DAU = context.Events.Where(x => x.date == date && x.event_id== 1).GroupBy(x => x.udid).Count();
            //dayStat.NewUsers = context.Events.Where(x => x.date == date && x.event_id == 2).GroupBy(x => x.udid).Count();
            //dayStat.Revenue = context.Events.Where(x => x.date == date && x.event_id == 6).Select(x => x.parametersPrice).Sum();
            //dayStat.ExchangeRate = dayStat.Revenue / context.Events.Where(x => x.date == date && x.event_id == 6).Select(x => x.parametersIncome).Sum();

            //dayStats.Add(dayStat);

            var result  = dayStats.Select(x => new DayStat
            { 
                Date = x.Date,
                DAU = x.DAU,
                NewUsers = x.NewUsers,
                Revenue = x.Revenue,
                ExchangeRate = x.ExchangeRate,
            }).ToList();

            Console.WriteLine("Uploading to DB");
            context.DayStats.AddRange(result);
            context.SaveChanges();
        }

        public void UpdateItemStatDay()
        {
            var dates = context.Events.GroupBy(x => x.date).Select(gr => gr.Key).OrderBy(x=>x.Day).Skip(16).ToList();

            foreach (DateTime date in dates)
            {
                Console.WriteLine("Updating items for " + date);
                String[] itemNames = context.Events.Where(x => x.date == date && x.event_id == 5)
                    .GroupBy(x => x.parametersItem).OrderBy(x => x.Key).Select(gr => gr.Key).ToArray();
                double exchangeRate = context.DayStats.Where(x => x.Date == date).FirstOrDefault().ExchangeRate;
                ItemStatDay[] items = new ItemStatDay[itemNames.Length];
                Event[] events = context.Events.Where(x => x.event_id == 5 && x.date == date).ToArray();
                for (int i = 0; i < itemNames.Length; i++)
                {
                    string itemName = itemNames[i];
                    Console.WriteLine(itemName);
                    Event[] eventsItem =  events.Where(x => x.parametersItem == itemName).ToArray();
                    ItemStatDay item = new ItemStatDay();
                    item.Date = date;
                    item.Amount = eventsItem.Count();
                    item.CurrencySpent = eventsItem.Select(x => x.parametersPrice).Sum();
                    item.USD = item.CurrencySpent * exchangeRate;
                    item.Name = itemName;
                    items[i] = item;
                }
                context.ItemStatDays.AddRange(items);
                context.SaveChanges();
            }
        }

        private void UpdateItemStat()
        {
            var items = context.ItemStatDays.GroupBy(t => t.Name).AsEnumerable()
                .Select(t => new ItemStat {
                    Name = t.Key, Amount = t.Sum(u => u.Amount),
                    CurrencySpent = t.Sum(u => u.CurrencySpent),
                    USD = t.Sum(u => u.USD) }).ToList();
            context.ItemStats.AddRange(items);
            context.SaveChanges();
        }
        private void UpdateStageStatDay()
        {
            var dates = context.Events.GroupBy(x => x.date).OrderBy(x=>x.Key).Select(gr => gr.Key).ToList();

            foreach (DateTime date in dates)
            {
                Console.WriteLine("Updating stages for " + date);
                int[] stageNums = context.Events.Where(x => x.date == date && x.event_id == 3)
                    .GroupBy(x => x.parametersStage).OrderBy(x => x.Key).Select(gr => gr.Key).ToArray();
                double exchangeRate = context.DayStats.Where(x => x.Date == date).FirstOrDefault().ExchangeRate;
                StageStatDay[] stageStatDays = new StageStatDay[stageNums.Length];
                var events = context.Events.Where(x => x.date == date);
                for (int i = 0; i < stageNums.Length; i++)
                {
                    int stageNum = stageNums[i];
                    Console.WriteLine(stageNum);
                    Event[] stageEvent = events.Where(x => x.parametersStage == stageNum).ToArray();
                    StageStatDay stage = new StageStatDay();
                    stage.DateTime = date;
                    stage.StageNum = stageNum;
                    stage.Started = stageEvent.Where(x => x.event_id == 3).Count();
                    stage.Finished = stageEvent.Where(x => x.event_id == 4).Count();
                    stage.Wins = stageEvent.Where(x => x.parametersWin == true).Count();
                    stage.Income = stageEvent.Select(x => x.parametersIncome).Sum();
                    stage.Revenue = stage.Income * exchangeRate;
                    stageStatDays[i] = stage;
                }
                context.StagesStatDays.AddRange(stageStatDays);
                context.SaveChanges();
            }
        }
        private void UpdateStageStat()
        {
            var stages = context.StagesStatDays.GroupBy(t => t.StageNum).AsEnumerable()
              .Select(t => new StageStat
              {
                  StageNum = t.Key,
                  Finished = t.Sum(u => u.Finished),
                  Started = t.Sum(u => u.Started),
                  Income = t.Sum(u => u.Income),
                  Revenue = t.Sum(u => u.Revenue),
                  Wins = t.Sum(u => u.Wins)
              }).ToList();

            context.StagesStats.AddRange(stages);
            context.SaveChanges();
        }

        struct LinReg
        {
            public double Slope;
            public double yIntercept;
            public double rSquared;
        }
        private void Predict()
        {

            double firstDay = context.DayStats.OrderByDescending(x => x.Date).Select(x => (double)x.Date.Day + 1).First();
            int amountOfDays = 182;

            var result = new LinReg[5];
            var days = context.DayStats.Select(x => (double)x.Date.Day).ToArray();
            Maths.LinearRegression(days,
                context.DayStats.Select(x => (double)x.DAU).ToArray(),
                out result[0].rSquared,
                out result[0].yIntercept,
                out result[0].Slope);
            Maths.LinearRegression(days,
                context.DayStats.Select(x => (double)x.NewUsers).ToArray(),
                out result[1].rSquared,
                out result[1].yIntercept,
                out result[1].Slope);
            Maths.LinearRegression(days,
                context.DayStats.Select(x => (double)x.Revenue).ToArray(),
                out result[2].rSquared,
                out result[2].yIntercept,
                out result[2].Slope);
            Maths.LinearRegression(days,
                context.ItemStatDays.GroupBy(x => x.Date).OrderBy(x=>x.Key).Select(g => (double)g.Sum(p => p.Amount)).ToArray(),
                out result[3].rSquared,
                out result[3].yIntercept,
                out result[3].Slope);
            Maths.LinearRegression(days,
                context.ItemStatDays.GroupBy(x => x.Date).OrderBy(x => x.Key).Select(g => (double)g.Sum(p => p.USD)).ToArray(),
                out result[4].rSquared,
                out result[4].yIntercept,
                out result[4].Slope);

            Prediction[] predictions = new Prediction[amountOfDays];
               
            for (int i = 0; i < amountOfDays; i++)
            {
                predictions[i] = new Prediction
                {
                    Date = new DateTime(2018, 1, 1).AddDays(firstDay + i - 1),
                    DAU = Convert.ToInt32((result[0].Slope * (firstDay + i)) + result[0].yIntercept),
                    NewUsers = Convert.ToInt32((result[1].Slope * (firstDay + i)) + result[1].yIntercept),
                    Revenue = Math.Round((result[2].Slope * (firstDay + i)) + result[2].yIntercept, 2),
                    SoldAmount = Convert.ToInt32((result[3].Slope * (firstDay + i)) + result[3].yIntercept),
                    SoldUSD = (result[4].Slope * (firstDay + i)) + result[4].yIntercept
                };

                Console.WriteLine("Day: {0}   DAU: {1}   NewUsers: {2}   Revenue:{3}   SoldAmount: {4}   SoldUSD: {5}",
                    predictions[i].Date.ToShortDateString(), predictions[i].DAU,predictions[i].NewUsers,predictions[i].Revenue, predictions[i].SoldAmount,
                    predictions[i].SoldUSD);
            }
            context.Prediction.AddRange(predictions);
            context.SaveChanges();
        }

        public void OverallCount()
        {
            Console.WriteLine("male - {0}", context.Events.Where(x => x.parametersGender == "male").Count());
            Console.WriteLine("female - {0}", context.Events.Where(x => x.parametersGender == "female").Count());

            ages = context.Events.Where(x => x.parametersAge != 0).GroupBy(x => x.parametersAge).Select(x => x.Key).ToArray();
            foreach (int age in ages)
            {
                Console.WriteLine("{0} age - {1}", age, context.Events.Where(x => x.parametersAge == age).Count());
            }

            countries = context.Events.GroupBy(x => x.parametersCountry).Select(x => x.Key).ToArray();
            foreach (string c in countries)
            {
                Console.WriteLine("{0} - {1}", c, context.Events.Where(x => x.parametersCountry == c).Count());
            }
        }

        public void GroupByMale(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            dataGrid.ItemsSource = context.Events.Where(x => x.parametersGender == "male").ToList();
            Console.WriteLine("male - {0}", context.Events.Where(x => x.parametersGender == "male").Count());
        }

        public void GroupByFemale(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            dataGrid.ItemsSource = context.Events.Where(x => x.parametersGender == "female").ToList();
            Console.WriteLine("female - {0}", context.Events.Where(x => x.parametersGender == "female").Count());
        }

        public void AgeMenu(object sender, RoutedEventArgs e)
        {
            current = 1;
            selectedMenu = 1;
            previous.Visibility = Visibility.Visible;
            previous.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            ages = context.Events.GroupBy(x => x.parametersAge).Select(x => x.Key).ToArray();
            GroupByAge();
        }

        public void CountryMenu(object sender, RoutedEventArgs e)
        {
            current = 0;
            selectedMenu = 2;
            previous.Visibility = Visibility.Visible;
            previous.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            countries = context.Events.GroupBy(x => x.parametersCountry).Select(x => x.Key).ToArray();
            GroupByCountry();
        }

        public void GroupByAge()
        {
            int age = ages[current];
            dataGrid.ItemsSource = context.Events.Where(x => x.parametersAge == age).ToList();
        }

        public void GroupByCountry()
        {
            string country = countries[current];
            dataGrid.ItemsSource = context.Events.Where(x => x.parametersCountry == country).ToList();
        }

        public void NextBtnClick(object sender, RoutedEventArgs e)
        {
            previous.IsEnabled = true;
            current++;
            switch (selectedMenu)
            {
                case 1:
                    GroupByAge();
                    if (current == ages.Length)
                        next.IsEnabled = false;
                    break;
                case 2:
                    GroupByCountry();
                    if (current == countries.Length)
                        next.IsEnabled = false;
                    break;
            }
        }

        public void PreviousBtnClick(object sender, RoutedEventArgs e)
        {
            next.IsEnabled = true;
            current--;
            switch (selectedMenu)
            {
                case 1:
                    GroupByAge();
                    if (current == 1)
                        previous.IsEnabled = false;
                    break;
                case 2:
                    GroupByCountry();
                    if (current == 0)
                        previous.IsEnabled = false;
                    break;
            }
        }
        class UserPrice
        {
            public string udid;
             public double price;
        }


        public void Tier0(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            string[] users = context.Events.GroupBy(x => x.udid).Select(x => x.Key).ToArray();
            List<UserPrice> events = (from string u in users
          select context.Events.Where(x => x.event_id == 6 && x.udid == u).GroupBy(x => x.udid).Select(
                                    g => new UserPrice
                                    {
                                        udid = g.Key,
                                        price = g.Sum(s => s.parametersPrice),
                                    }).FirstOrDefault()).ToList();
            dataGrid.ItemsSource = events;
        }

        public void Tier1(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            string[] users = context.Events.GroupBy(x => x.udid).Select(x => x.Key).ToArray();
            int count = 0;
            foreach (string u in users)
            {
                float t = context.Events.Where(x => x.udid == u && x.event_id == 6).Sum(x => (int?)x.parametersPrice) ?? 0;
                if (t > 0 && t <= 30)
                    Console.WriteLine(u);
                count++;
            }
            Console.WriteLine("\n" + count);
        }

        public void Tier2(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            string[] users = context.Events.GroupBy(x => x.udid).Select(x => x.Key).ToArray();
            int count = 0;
            foreach (string u in users)
            {
                float t = context.Events.Where(x => x.udid == u && x.event_id == 6).Sum(x => (int?)x.parametersPrice) ?? 0;
                if (t > 30 && t <= 60)
                    Console.WriteLine(u);
                count++;
            }
            Console.WriteLine("\n" + count);
        }

        public void Tier3(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            previous.Visibility = Visibility.Collapsed;
            string[] users = context.Events.GroupBy(x => x.udid).Select(x => x.Key).ToArray();
            int count = 0;
            foreach (string u in users)
            {
                float t = context.Events.Where(x => x.udid == u && x.event_id == 6).Sum(x => (int?)x.parametersPrice) ?? 0;
                if (t > 60)
                    Console.WriteLine(u);
                count++;
            }
            Console.WriteLine("\n" + count);
            //var temp = context.Events.SqlQuery("select udid, sum(parametersPrice) from Events where (event_id <> 5) group by udid");
        }

        public void Cheaters(object sender, RoutedEventArgs e)
        {
            List<string> result = new List<string>();
            string[] users = context.Events.GroupBy(x => x.udid).Select(x => x.Key).ToArray();
            foreach (string user in users)
            {
                float gainded = context.Events.Where(x => x.event_id == 4 && x.udid == user).Sum(x => x.parametersIncome);
                float spent = context.Events.Where(x => x.event_id == 5 && x.udid == user).Sum(x => (int?)x.parametersPrice) ?? 0;
                float bought = context.Events.Where(x => x.event_id == 6 && x.udid == user).Sum(x => (int?)x.parametersIncome) ?? 0;
                if (spent > (gainded + bought))
                {
                    string str = String.Format("{0}: gained {1} > {2} spent", user, gainded, spent);
                    result.Add(str);
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("\n Cheaters - {0}", result.Count);
        }
        class userIncome
        {
            public string udid;
            public double? earnedGameMoney;
            public double? spentGameMoney;
        }

        public void Cheaters2(object sender, RoutedEventArgs e)
        {

            List<userIncome> result = new List<userIncome>();
            result = context.Events.Where(x => x.event_id == 4 || x.event_id==5 || x.event_id == 6).GroupBy(x => x.udid).Select(
                                    g => new userIncome
                                    {
                                        udid = g.Key,
                                        earnedGameMoney = g.Sum(s => s.parametersIncome),
                                        spentGameMoney = g.Where(s=>s.event_id==5).Sum(s=>s.parametersPrice)
                                    }).ToList();

            List<userIncome> result2 = new List<userIncome>();
            //result2 = context.Events.Where(x => x.event_id == 5).GroupBy(x => x.udid).Select(
            //                        g => new userIncome
            //                        {
            //                            udid = g.Key,
            //                            spentGameMoney = g.Sum(s => s.parametersPrice),
            //                        }).ToList();


            List<Users> users = result.Select(x => new Users
            {
                udid = Guid.Parse(x.udid),
                earnedGameMoney = x.earnedGameMoney ?? 0,
                spentGameMoney = x.spentGameMoney ?? 0,
            }).ToList();


            //result3 = result3.Where(x => x.income < x.price);
            context.BulkUpdate(users);

            //context.SaveChanges();

            dataGrid.ItemsSource = result;
        }

    }
}

