using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <inheritdoc />
    /// <summary>
    ///     Employee audit table
    /// </summary>
    internal class EmployeeAudit : DatabaseTable
    {
        public int Dependents { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        public int EmployeeId { get; set; }

        public int EmployerId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(100)]
        public string MiddleName { get; set; }
    }
}
