using System;
using App.Repositories;
using App.Services;
using Xunit;

namespace Tests.Services
{
    public class ParkingRateServiceTests
    {
        private readonly IParkingRateService _parkingRateService;

        public ParkingRateServiceTests()
        {
            var json = @"{
    ""rates"": [
        {
            ""days"": ""mon,tues,thurs"",
            ""times"": ""0900-2100"",
            ""price"": 1500
        },
        {
            ""days"": ""fri,sat,sun"",
            ""times"": ""0900-2100"",
            ""price"": 2000
        },
        {
            ""days"": ""wed"",
            ""times"": ""0600-1800"",
            ""price"": 1750
        },
        {
            ""days"": ""mon,wed,sat"",
            ""times"": ""0100-0500"",
            ""price"": 1000
        },
        {
            ""days"": ""sun,tues"",
            ""times"": ""0100-0700"",
            ""price"": 925
        }
    ]
}";
            var parkingRateRepository = new ParkingRateRepository(json);

            _parkingRateService = new ParkingRateService(parkingRateRepository);
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, 1, 00, 7, 00, 925)]
        [InlineData(DayOfWeek.Sunday, 1, 00, 8, 00, null)]
        [InlineData(DayOfWeek.Sunday, 0, 00, 4, 00, null)]
        [InlineData(DayOfWeek.Sunday, 2, 00, 4, 00, 925)]
        [InlineData(DayOfWeek.Monday, 9, 00, 21, 00, 1500)]
        [InlineData(DayOfWeek.Monday, 17, 00, 20, 00, 1500)]
        [InlineData(DayOfWeek.Monday, 2, 00, 4, 00, 1000)]
        [InlineData(DayOfWeek.Tuesday, 9, 00, 21, 00, 1500)]
        [InlineData(DayOfWeek.Tuesday, 9, 00, 22, 00, null)]
        [InlineData(DayOfWeek.Tuesday, 1, 00, 7, 00, 925)]
        [InlineData(DayOfWeek.Tuesday, 0, 00, 7, 00, null)]
        [InlineData(DayOfWeek.Wednesday, 5, 00, 6, 00, null)]
        [InlineData(DayOfWeek.Wednesday, 6, 00, 18, 00, 1750)]
        [InlineData(DayOfWeek.Wednesday, 6, 00, 19, 00, null)]
        [InlineData(DayOfWeek.Thursday, 9, 00, 19, 00, 1500)]
        [InlineData(DayOfWeek.Thursday, 8, 00, 22, 00, null)]
        [InlineData(DayOfWeek.Friday, 22, 00, 10, 00, null)]
        [InlineData(DayOfWeek.Friday, 10, 00, 21, 00, 2000)]
        [InlineData(DayOfWeek.Saturday, 1, 00, 1, 00, 1000)]
        [InlineData(DayOfWeek.Saturday, 1, 00, 9, 00, null)]
        [InlineData(DayOfWeek.Saturday, 12, 00, 12, 00, 2000)]
        public void GetPriceTest(DayOfWeek dayOfWeek, int startHour, int startMin, int endHour, int endMin, int? expected)
        {
            // arrange
            var start = new TimeSpan(startHour, startMin, 0);
            var end = new TimeSpan(endHour, endMin, 0);

            // act
            var result = _parkingRateService.GetPrice(dayOfWeek, start, end);

            // assert
            Assert.Equal(expected, result);
        }

        private static TimeSpan GetTimeSpan(int hour, int min) {
            return new TimeSpan(hour, min, 00);
        }
    }
}
