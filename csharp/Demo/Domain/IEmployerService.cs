using System.Threading.Tasks;
using Common;

namespace Domain
{
    /// <summary>
    ///     Employer service.
    /// </summary>
    public interface IEmployerService
    {
        /// <summary>
        ///     Save an employer.
        /// </summary>
        /// <param name="employer">The employer model.</param>
        Task<int> SaveEmployer(Employer employer);
    }
}
