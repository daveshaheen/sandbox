using System.ComponentModel.DataAnnotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     Employer audit model for change tracking.
    /// </summary>
    public class EmployerAudit : Base
    {
        public int EmployerId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
