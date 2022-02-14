using System;

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        private const string dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/who_covid_19_situation_reports/who_covid_19_sit_rep_time_series/who_covid_19_sit_rep_time_series.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);
            while(!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (String.IsNullOrWhiteSpace(line)) continue;

                if (line.Contains("Bonaire,")) 
                    yield return line.Replace("Bonaire,", "Bonaire -");
                if (line.Contains("Korea,"))
                    yield return line.Replace("Korea,", "Korea -");
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0];
                var countryName = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(s => (String.IsNullOrEmpty(s))? 0:int.Parse(s)).ToArray();
                yield return (countryName, province, counts);
            }

        }

        static void Main(string[] args)
        {
            //var date = GetDates();
            //Console.WriteLine(String.Join("\r\n",date));
            var russaiData = GetData()
                .First(v => v.Country.Equals("Russian Federation", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(String.Join("\r\n", GetDates().Zip(russaiData.Counts, (date, count) => $"{date:dd:MM} - {count}")));
            Console.ReadLine();

        }
    }
}
