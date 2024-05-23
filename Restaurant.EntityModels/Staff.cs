using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.EntityModels;

public class Staff
{
    public int Id { get; set; } // The primary key.

    [Required]
    [StringLength(60)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(60)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(60)]
    public string Position { get; set; } = null!;

    [Required]
    [StringLength(60)]
    public string Department { get; set; } = null!;

    [Required]
    [StringLength(60)]
    public string DateOfBirth { get; set; } = null!;

    // public bool Fired { get; set; }
}
