using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models;

public class Department
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(255)]
    public string Description { get; set; }
}