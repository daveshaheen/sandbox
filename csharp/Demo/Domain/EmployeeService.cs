using System.Threading.Tasks;
using Common;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    /// <inheritdoc />
    public class EmployeeService : IEmployeeService
    {
        /// <inheritdoc />
        public async Task<int> SaveEmployee(Employee employee)
        {
            using(var demoDbContext = new DemoDbContext())
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
