using System.ComponentModel.DataAnnotations;

namespace Data
{
    /// <inheritdoc />
    /// <summary>
    ///     <para>
    ///         Employee table
    ///     </para>
    ///     <para>
    ///         Not implementing these items for this demo, but...
    ///         We could store address and tax identification information here or in related tables. Dependents can be broken out into it's own table or maybe make a person table and types.
    ///         Tax information such as SSN must be encrypted and might also be better kept in another table for partitioning and security reasons or another database/system entirely.
    ///         An employee could have many addresses with one marked as primary so a related table would probably be better than storing here.
    ///     </para>
    ///     <para>
    ///         See https://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields for email length.
    ///     </para>
    ///     <para>
    ///         See https://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields and also
    ///         https://en.wikipedia.org/wiki/Hubert_Blaine_Wolfeschlegelsteinhausenbergerdorff,_Sr. regarding first and last name lengths.
    ///     </para>
    /// </summary>
    public class Employee : DatabaseTable
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
