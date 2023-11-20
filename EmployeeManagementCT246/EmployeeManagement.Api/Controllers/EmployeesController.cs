using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var result = await employeeRepository.GetEmployees();
            return result.Any()
                ? Ok(result)
                : NotFound("No record found!");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var result = await employeeRepository.GetEmployee(id);

            if (result == null)
                return NotFound($"Employee at id {id} not found!");

            return result;
        }

        //[HttpPost]
        //public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        //{
        //    try
        //    {
        //        if (employee == null)
        //        {
        //            return BadRequest();
        //        }

        //        var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

        //        if (emp != null)
        //        {
        //            ModelState.AddModelError("email", "Employee email already in use");
        //            return BadRequest(ModelState);
        //        }

        //        var createdEmployee = await employeeRepository.AddEmployee(employee);

        //        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId },
        //            createdEmployee);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(AddEmployeeDto employee)
        {
            var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

            if (emp != null)
            {
                return BadRequest("Email already been taken");
            }

            var createdEmployee = await employeeRepository.AddEmployee(employee);

            return Ok(createdEmployee);
        }


        [HttpPut()]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {employee.EmployeeId} not found");
                }

                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error update data");
            }
        }

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        //{

        //        var employeeToDelete = await employeeRepository.GetEmployee(id);
        //        if (employeeToDelete == null)
        //        {
        //            return NotFound($"Employee with Id = {id} not found");
        //        }
        //        return await employeeRepository.DeleteEmployee(id);


        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //        "Error deleting data");

        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {

            var employeeToDelete = await employeeRepository.GetEmployee(id);
            if (employeeToDelete == null)
            {
                return NotFound($"Employee with Id = {id} not found");
            }
            await employeeRepository.DeleteEmployee(id);
            return Ok(employeeToDelete);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender gender)
        {
            var result = await employeeRepository.Search(name, gender);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}