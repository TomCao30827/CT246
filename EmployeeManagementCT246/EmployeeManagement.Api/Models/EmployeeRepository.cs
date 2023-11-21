using EmployeeManagement.Models;
using EmployeeManagement.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //public async Task<Employee?> GetEmployee(int employeeId)
        //{
        //    return await appDbContext.Employees.FindAsync(employeeId);
        //}

        public async Task<Employee?> GetEmployee(int employeeId)
        {
            return await appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> AddEmployee(AddEmployeeDto employee)
        {
            var result = await appDbContext.Employees.AddAsync(new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId,
                Gender = employee.Gender,
                PhotoPath = employee.PhotoPath,
            });
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        //public async Task DeleteEmployee(int employeeId)
        //{
        //    var result = await appDbContext.Employees
        //    .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        //    if (result != null)
        //    {
        //        appDbContext.Employees.Remove(result);
        //        await appDbContext.SaveChangesAsync();
        //    }
        //}

        public async Task<Employee?> DeleteEmployee(int employeeId)
        {
            var empToDel = await appDbContext.Employees.FindAsync(employeeId);
            if (empToDel is null)
                return null;

            appDbContext.Employees.Remove(empToDel);
            await appDbContext.SaveChangesAsync();
            return empToDel;
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender gender)
        {
            return await appDbContext.Employees
                .Where(empl =>
                    empl.Gender == gender &&
                    empl.FirstName.ToLower().Equals(name.ToLower()))
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
