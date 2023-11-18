﻿using EmployeeManagement.Models;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee> GetEmployee(int id);
    Task<Employee> UpdateEmployee(Employee updatedEmployee);
    Task<Employee> CreateEmployee(Employee newEmployee);
}