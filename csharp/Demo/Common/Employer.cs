using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     <para>
    ///         Employer model
    ///     </para>
    ///     <para>
    ///         NOTE: Not implementing these items for this demo, but it's another use case for address and tax identification information.
    ///         See the note in the <see cref="Employee" /> class.
    ///     </para>
    /// </summary>
    public class Employer : Base
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
