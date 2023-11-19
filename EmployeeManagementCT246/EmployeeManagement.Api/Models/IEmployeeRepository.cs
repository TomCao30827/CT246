using EmployeeManagement.Models;
using EmployeeManagement.Models.Dtos;

namespace EmployeeManagement.Api.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(AddEmployeeDto employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> DeleteEmployee(int employeeId);
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);



    }

}
