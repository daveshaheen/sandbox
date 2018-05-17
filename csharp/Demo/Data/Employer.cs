using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <inheritdoc />
    /// <summary>
    ///     <para>
    ///         Employer table
    ///     </para>
    ///     <para>
    ///         Not implementing these items for this demo, but...
    ///         We could also store address and tax identification information here or in related tables.
    ///         Tax information must be encrypted and might also be better kept in another table for partitioning and security reasons or another database/system entirely.
    ///         There could be many addresses so a related table might be better than storing here with headquarter information and other branch address information.
    ///     </para>
    /// </summary>
    public class Employer : DatabaseTable
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
