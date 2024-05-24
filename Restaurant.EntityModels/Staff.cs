using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.EntityModels;

public class Staff
{
    public int Id { get; set; }

    [Required]
    [StringLength(60, ErrorMessage = "Staff[FirstName] must be less the 100 char.")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(60, ErrorMessage = "Staff[LastName] must be less the 100 char.")]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(60, ErrorMessage = "Staff[Position] must be less the 100 char.")]
    public string Position { get; set; } = null!;

    [Required]
    [StringLength(60, ErrorMessage = "Staff[Department] must be less the 100 char.")]
    public string Department { get; set; } = null!;

    [Required]
    public long DateOfBirth { get; set; } = 0;

    [Required]
    [Range(1, 999999999, ErrorMessage = "Staff[Salary] must be between 1 and 999999999")]
    public double Salary { get; set; } = 0;
}
