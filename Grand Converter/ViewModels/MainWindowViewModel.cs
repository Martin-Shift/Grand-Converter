using Grand_Converter.Models;
using My.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Grand_Converter.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private ValueConverter _converter;
        public MainWindowViewModel()
        { 
            _converter = new();
        }
        public ObservableCollection<string> FromFinds { get; set; } = new ObservableCollection<string>(ValueConverter.Values);
        public ObservableCollection<string> ToFinds { get; set; } = new ObservableCollection<string>(ValueConverter.Values);
        private string _fromFind = "";
        public string FromFind
        {
            get => _fromFind; set
            {
                _fromFind = value;
                FromFinds = new ObservableCollection<string>(ValueConverter.Values.Where(x => x.Contains(FromFind.ToUpper())));
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
                ToFinds = new ObservableCollection<string>(ValueConverter.Values.Where(x => x.Contains(ToFind.ToUpper())));
                OnPropertyChanged(nameof(ToFind));
                OnPropertyChanged(nameof(ToFinds));
            }
        }
        public DateTime ConvertTime
        {
            get => _converter.ConvertTime;
            set { _converter.ConvertTime = value; OnPropertyChanged(nameof(ConvertTime)); }
        }
        public double Amount
        {
            get => _converter.Amount;
            set { _converter.Amount = value; OnPropertyChanged(nameof(Amount)); }
        }
        public string From
        {
            get => _converter.From;
            set { _converter.From = value; OnPropertyChanged(nameof(From)); }
        }
        public string To
        {
            get => _converter.To;
            set { _converter.To = value; OnPropertyChanged(nameof(To)); }
        }
        public string Result
        {
            get => _converter.Result;
            set { _converter.Result = value; OnPropertyChanged(nameof(Result)); }
        }
        public string For1
        {
            get => _converter.ForRate; set
            {
                _converter.ForRate = value;
                OnPropertyChanged(nameof(For1));
            }
        }
        public string To1 { get => _converter.ToRate; set { _converter.ToRate = value; OnPropertyChanged(nameof(To1)); } }
        public ICommand Calculate => new RelayCommand(
            x =>
           {
               var rate = _converter.GetRate();
               For1 = $"1 {From} = {Math.Round(rate, 2)} {To}";
               To1 = $"1 {To} = {Math.Round(1 / rate, 2)} {From}";
               Result = $"{Amount} {From} = {Math.Round(rate * Amount, 2)} {To}";
           }, x => From != "" && To != "" && Amount != 0);
        public ICommand Swap => new RelayCommand(
            x =>
        {
            string fromtemp = To;
            string totemp = From;
            FromFind = "";
            ToFind = "";
            From = ValueConverter.Values.Where(x => x.Contains(fromtemp.ToUpper())).First();
            To = ValueConverter.Values.Where(x => x.Contains(totemp.ToUpper())).First();
        }, x => From != "" && To != ""
        );
    }
}
