using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <inheritdoc />
    /// <summary>
    ///     Employer audit table
    /// </summary>
    internal class EmployerAudit : DatabaseTable
    {
        public int EmployerId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
