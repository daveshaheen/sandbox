using System.Threading.Tasks;
using Common;

namespace Domain
{
    /// <summary>
    ///     Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        ///     Save an employee's information.
        /// </summary>
        /// <param name="employee">The Employee model.</param>
        Task<int> SaveEmployee(Employee employee);
    }
}
