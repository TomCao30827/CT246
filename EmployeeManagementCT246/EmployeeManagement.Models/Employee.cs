using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int EmployeeId { get; set; }
    [Required(ErrorMessage = "FirstName is mandatory")]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Lastname is mandatory")]
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public string PhotoPath { get; set; }
    public Department Department { get; set; }
}
