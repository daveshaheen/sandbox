using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using App.Models;
using Newtonsoft.Json;

namespace App.Repositories
{
    /// <summary>
    ///     The ParkingRateRepository class.
    ///     <para>Contains implementations for the methods and properties needed to retrieve the data from the parking rate repository.</para>
    /// </summary>
    /// <remarks>Implements <see cref="IParkingRateRepository"/></remarks>
    public class ParkingRateRepository : IParkingRateRepository
    {
        private readonly string data = @"{
    ""rates"": [
        {
            ""days"": ""mon,tues,wed,thurs,fri"",
            ""times"": ""0600-1800"",
            ""price"": 1500
        },
        {
            ""days"": ""sat,sun"",
            ""times"": ""0600-2000"",
            ""price"": 2000
        }
    ]
}";

        /// <summary>
        ///     Constructor for the parking rate repository.
        /// </summary>
        /// <param name="json">The data as a json string.</param>
        public ParkingRateRepository(string json = null)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                data = json;
            }
        }

        /// <summary>
        ///     Fills the ParkingRatesDataModel from a json string.
        /// </summary>
        /// <returns>Returns a ParkingRatesDataModel</returns>
        public IEnumerable<ParkingRateDataModel> GetDataFromString(string data)
        {
            var json = JsonConvert.DeserializeObject<Sample>(data);

            var parkingRates = new List<ParkingRateDataModel>();
            foreach (var rate in json.Rates)
            {
                var parkingRateData = new ParkingRateDataModel();
                var parkingRateDays = new List<DayOfWeek>();

                var days = rate.Days.Split(',');
                foreach (var day in days)
                {
                    parkingRateDays.Add(GetDayOfWeek(day.ToLowerInvariant()));
                }
                parkingRateData.Days = parkingRateDays;

                var times = rate.Times.Split('-');
                if (times.Length != 2)
                {
                    throw new Exception($"Invalid times: {times}.");
                }
                parkingRateData.StartTime = GetTimeSpan(times[0]);
                parkingRateData.EndTime = GetTimeSpan(times[1]);

                parkingRateData.Price = rate.Price;

                parkingRates.Add(parkingRateData);
            }

            return parkingRates;
        }

        /// <summary>
        ///     Gets the parking rates from the data repository.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>Returns the ParkingRatesDataModel</returns>
        public ParkingRatesDataModel GetParkingRatesData(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime) =>
            new ParkingRatesDataModel
            {
                Rates = GetDataFromString(data)
                        .Where(r => r.Days.Any(d => d == dayOfWeek))
                        .Where(r => r.StartTime <= startTime && r.EndTime > endTime)
                        .ToList()
            };

        private class Sample
        {
            public SampleData[] Rates { get; set; }
        }

        private class SampleData
        {
            public string Days { get; set; }
            public string Times { get; set; }
            public int Price { get; set; }
        }

        private TimeSpan GetTimeSpan(string time)
        {
            if (time.ToCharArray().Length != 4)
            {
                throw new ArgumentException($"Invalid time: {time}");
            }

            var hours = time.Substring(0, 2);
            var days = time.Substring(2, 2);

            hours = hours.StartsWith('0') ? hours.Substring(1, 1) : hours;
            days = days.StartsWith('0') ? days.Substring(1, 1) : days;

            return new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(days), 0);
        }

        private DayOfWeek GetDayOfWeek(string day)
        {
            switch (day.ToLowerInvariant())
            {
                case "mon":
                    return DayOfWeek.Monday;
                case "tues":
                    return DayOfWeek.Tuesday;
                case "wed":
                    return DayOfWeek.Wednesday;
                case "thurs":
                    return DayOfWeek.Thursday;
                case "fri":
                    return DayOfWeek.Friday;
                case "sat":
                    return DayOfWeek.Saturday;
                case "sun":
                    return DayOfWeek.Sunday;
            }

            throw new ArgumentException($"Invalid day: {day}");
        }
    }
}
