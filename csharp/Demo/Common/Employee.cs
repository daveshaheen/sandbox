using System.ComponentModel.DataAnnotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     <para>
    ///         Employee model
    ///     </para>
    ///     <para>
    ///         NOTE: Not implementing these items for this demo, but additional related tables could include address and tax identification information.
    ///         Dependents can also be broken out into it's own table or instead maybe have a person table and types to avoid duplicating fields.
    ///         Tax information such as SSN must be encrypted and might also be better kept in another table for partitioning and security reasons or
    ///         maybe even another database entirely.
    ///     </para>
    ///     <para>
    ///         See https://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields for email length.
    ///     </para>
    ///     <para>
    ///         See https://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields and also
    ///         https://en.wikipedia.org/wiki/Hubert_Blaine_Wolfeschlegelsteinhausenbergerdorff,_Sr. regarding first and last name lengths.
    ///     </para>
    /// </summary>
    public class Employee : Base
    {
        public int Dependents { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        public Employer Employer { get; set; }

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
