using System;
using System.Net.Http;

namespace CV19Console
{
    internal class Program
    {
        private const string dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/who_covid_19_situation_reports/who_covid_19_sit_rep_time_series/who_covid_19_sit_rep_time_series.csv";

        static void Main(string[] args)
        {
            var client = new HttpClient();
            var csvStr = client.GetStringAsync(dataUrl).Result;
            Console.ReadLine();
        }
    }
}
