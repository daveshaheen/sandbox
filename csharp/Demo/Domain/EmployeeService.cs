using System.Threading.Tasks;
using Demo.Common.Models;
using Demo.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Domain
{
    /// <inheritdoc />
    public class EmployeeService : IEmployeeService
    {
        /// <inheritdoc />
        public async Task<int> SaveEmployee(Employee employee)
        {
            using(var demoDbContext = new DatabaseContext())
            {
                var entity = await demoDbContext.Employee.FirstOrDefaultAsync(e => e.Email == employee.Email);
                if (entity == null)
                {
                    await demoDbContext.Employee.AddAsync(employee);
                }

                return await demoDbContext.SaveChangesAsync();
            }
        }
    }
}
