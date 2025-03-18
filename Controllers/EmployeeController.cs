using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[Route("Employee/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployee() =>
        Ok(await _repository.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _repository.GetById(id);
        return employee == null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        var result = await _repository.Add(employee);

        if (result == null)
        {
            return BadRequest(new { message = "Employee already exists" });
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, Employee employee)
    {
        if (id != employee.Id) return BadRequest();
        return Ok(await _repository.Update(employee));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id) =>
        await _repository.Delete(id) ? NoContent() : NotFound();
}