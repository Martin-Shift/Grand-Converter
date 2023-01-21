using My.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Grand_Converter.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public MainWindowViewModel()
        {
            Strings = new ObservableCollection<string>(File.ReadAllText(@"D:\Mein progectos\Grand Converter\new.txt").Split('\n'));
        }
        private DateTime _convertTime = DateTime.Now;
        public DateTime ConvertTime { get => _convertTime; set { _convertTime = value; OnPropertyChanged(nameof(ConvertTime)); } }
        public WebClient Client = new WebClient();
        public ObservableCollection<string> FromFinds { get; set; } = new ObservableCollection<string>(File.ReadAllText(@"D:\Mein progectos\Grand Converter\new.txt").Split('\n'));
        private string _fromFind = "";
        public string FromFind
        {
            get => _fromFind; set
            {
                _fromFind = value;
                FromFinds = new ObservableCollection<string>(Strings.Where(x => x.Contains(FromFind.ToUpper())));
                OnPropertyChanged(nameof(FromFind));
                OnPropertyChanged(nameof(FromFinds));
            }
        }
        private string _toFind = "";
        public string ToFind
        {
            get => _toFind; set
            {
                _toFind = value;
                ToFinds = new ObservableCollection<string>(Strings.Where(x => x.Contains(ToFind.ToUpper())));
                OnPropertyChanged(nameof(ToFind));
                OnPropertyChanged(nameof(ToFinds));
            }
        }

        public ObservableCollection<string> ToFinds { get; set; } = new ObservableCollection<string>(File.ReadAllText(@"D:\Mein progectos\Grand Converter\new.txt").Split('\n'));
        public ObservableCollection<string> Strings { get; set; } = new ObservableCollection<string>(File.ReadAllText(@"D:\Mein progectos\Grand Converter\new.txt").Split('\n'));
        private double _amount = 0;
        public double Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(nameof(Amount)); }
        }
        private string _from = "";
        public string From { get => _from; set { _from = value; OnPropertyChanged(nameof(From)); } }
        private string _to = "";
        public string To { get => _to; set { _to = value; OnPropertyChanged(nameof(To)); } }
        private string _result = "";
        public string Result { get => _result; set { _result = value; OnPropertyChanged(nameof(Result)); } }
        private string _for1 = "";
        public string For1 { get => _for1; set { _for1 = value; OnPropertyChanged(nameof(For1)); } }
        private string _to1 = "";
        public string To1 { get => _to1; set { _to1 = value; OnPropertyChanged(nameof(To1)); } }
        public ICommand Calculate => new RelayCommand(
            x =>
           { 
               string url = $"https://fxds-public-exchange-rates-api.oanda.com/cc-api/currencies?base={From}&quote={To}&data_type=general_currency_pair&start_date={ConvertTime - new TimeSpan(1, 0, 0, 0):yyyy-MM-dd}&end_date={ConvertTime:yyyy-MM-dd}";
               double res = Convert.ToDouble(Regex.Match(Client.DownloadString(url).Replace('.', ','), @"average_bid.:.(\d+.\d+)").Groups[1].Value);
               For1 = $"1 {From} = {Math.Round(res,2)} {To}";
               To1 = $"1 {To} = {Math.Round(1 / res, 2)} {From}";
               Result = $"{Amount} {From} = {Math.Round(res * Amount,2)} {To}";
           }, x => From != "" && To != "" && Amount != 0);
        public ICommand Swap => new RelayCommand(
            x =>
        {
            string fromtemp = To;
            string totemp = From;
            FromFind = "";
            ToFind = "";
            From = Strings.Where(x => x.Contains(fromtemp.ToUpper())).First();
            To = Strings.Where(x => x.Contains(totemp.ToUpper())).First();
        }, x => From != "" && To != ""
        );
    }
}
