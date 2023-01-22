using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Grand_Converter.Models
{
    public class ValueConverter
    {
        public ValueConverter() { }
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public string Result { get; set; } = "";
        public double Amount { get; set; } = 0;
        public DateTime ConvertTime { get; set; } = DateTime.Now;
        public string ForRate { get; set; } = "";
        public string ToRate { get; set; } = "";
        public static readonly List<string> Values = File.ReadAllText(@"D:\Mein progectos\Grand Converter\new.txt").Split('\n').ToList();
        public double GetRate()
        {
            WebClient Client = new();
            string url = $"https://fxds-public-exchange-rates-api.oanda.com/cc-api/currencies?base={From}&quote={To}&data_type=general_currency_pair&start_date={ConvertTime - new TimeSpan(1, 0, 0, 0):yyyy-MM-dd}&end_date={ConvertTime:yyyy-MM-dd}";
            return Convert.ToDouble(Regex.Match(Client.DownloadString(url), @"average_bid.:.(\d+.\d+)").Groups[1].Value, CultureInfo.InvariantCulture);
        }

    }
}
