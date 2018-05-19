using System.Threading.Tasks;
using Demo.Common.Models;
using Demo.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Domain
{
    /// <inheritdoc />
    public class EmployerService : IEmployerService
    {
        /// <inheritdoc />
        public async Task<int> SaveEmployer(Employer employer)
        {
            using(var demoDbContext = new DatabaseContext())
            {
                var entity = await demoDbContext.Employer.FirstOrDefaultAsync(e => e.Name == employer.Name);
                if (entity == null)
                {
                    await demoDbContext.Employer.AddAsync(employer);
                }

                return await demoDbContext.SaveChangesAsync();
            }
        }
    }
}
