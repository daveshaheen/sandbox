using System;
using System.Collections.Generic;
using System.Globalization;
using Demo.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Applications.Api.Controllers
{
    [FormatFilter]
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    public sealed class EmployeeController : Controller
    {
        // GET api/values
        [HttpGet]
        [HttpGet(".{format?}")]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}.{format?}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        // rough mock up
        public decimal GetPayInfo(Employee employee, int dependents)
        {
            if (employee == null) {
                throw new ArgumentNullException(nameof(employee));
            }

            if (dependents < 0) {
                throw new ArgumentOutOfRangeException(nameof(dependents));
            }

            var pay = 2000.00m;
            var payPeriods = 26m;
            var employeeBenefitsCost = 1000.00m;
            var dependentBenfitsCost = 500.00m;
            var discount = 1.0m;
            if (
                employee.FirstName.StartsWith("a", ignoreCase: true, culture: CultureInfo.CurrentCulture) ||
                employee.LastName.StartsWith("a", ignoreCase: true, culture: CultureInfo.CurrentCulture))
            {
                discount = 0.9m;
            }

            return (pay * payPeriods) - ((employeeBenefitsCost + (dependentBenfitsCost * dependents)) * discount);
        }
    }
}
