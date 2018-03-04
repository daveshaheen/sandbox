using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using App.Models;

namespace App.Repositories
{
    /// <summary>
    ///     The ParkingRateRepository class.
    ///     <para>Contains implementations for the methods and properties needed to retrieve the data from the parking rate repository.</para>
    /// </summary>
    /// <remarks>Implements <see cref="IParkingRateRepository"/></remarks>
    public class ParkingRateRepository : IParkingRateRepository
    {
        /// <summary>
        ///     Gets the parking rates from the data repository.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>Returns the ParkingRatesDataModel</returns>
        public ParkingRatesDataModel GetParkingRatesData(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            /*
                Sample file:
                {
                    "rates": [
                        {
                            "days": "mon,tues,wed,thurs,fri",
                            "times": "0600-1800",
                            "price": 1500
                        },
                        {
                            "days": "sat,sun",
                            "times": "0600-2000",
                            "price": 2000
                        }
                    ]
                }
            */
            // Let's pretend this model is filled by some query against IDbConnection and that IDbConnection is
            // passed in through the constructor and assigned to a private variable.
            return new ParkingRatesDataModel
            {
                Rates = new[] {
                    new ParkingRateDataModel
                    {
                        Days = new[] {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        },
                        Price = 1500,
                        StartTime = new TimeSpan(6, 0, 0),
                        EndTime = new TimeSpan(18, 0, 0)
                    },
                    new ParkingRateDataModel
                    {
                        Days = new[] {
                            DayOfWeek.Saturday,
                            DayOfWeek.Sunday
                        },
                        Price = 2000,
                        StartTime = new TimeSpan(6, 0, 0),
                        EndTime = new TimeSpan(20, 0, 0)
                    }
                }
                .Where(r => r.Days.Any(d => d == dayOfWeek))
                .Where(r => r.StartTime <= startTime && r.EndTime > endTime)
                .ToList()
            };
        }
    }
}
