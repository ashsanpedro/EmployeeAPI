using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models;

    public class Employee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        public decimal Salary { get; set; }
        
        [Required]
        public Guid DepartmentId { get; set; }
        
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }

