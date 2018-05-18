using System.Threading.Tasks;
using Common;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    /// <inheritdoc />
    public class EmployerService : IEmployerService
    {
        /// <inheritdoc />
        public async Task<int> SaveEmployer(Employer employer)
        {
            using(var demoDbContext = new DemoDbContext())
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
