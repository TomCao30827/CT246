using EmployeeManagement.Models;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department> GetDepartment(int id);
}