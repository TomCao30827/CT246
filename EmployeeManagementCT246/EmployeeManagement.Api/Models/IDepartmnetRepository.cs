﻿using EmployeeManagement.Models;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department> GetDepartment(int departmentId);
}