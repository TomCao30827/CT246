using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<Employee[]>("api/employees");
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var response = await httpClient.PutAsJsonAsync("api/employees", updatedEmployee);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            var response = await httpClient.PostAsJsonAsync("api/employees", newEmployee);

            // Check if the request was successful (status code 200 OK)
            response.EnsureSuccessStatusCode();

            // Deserialize the response content into an Employee object
            return await response.Content.ReadFromJsonAsync<Employee>();
        }


    }
}