﻿using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext appDbContext;

    public DepartmentRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Department> GetDepartment(int departmentId)
    {
        return await appDbContext.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
    }

    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await appDbContext.Departments.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByEmail(string email)
    {
        return await appDbContext.Employees
            .FirstOrDefaultAsync(e => e.Email == email);
    }
}