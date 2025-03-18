using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employee>> GetAll() =>
        await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    
    public async Task<Employee> GetById(int id) =>
        await _context.Employees.FindAsync(id);

    public async Task<Employee> Add(Employee employee)
    {

        bool exists = await _context.Employees
            .AnyAsync(e => e.Name == employee.Name && e.DepartmentId == employee.DepartmentId);
        if (exists)
        {
            return null;
        }
        
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        
        var savedEmployee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == employee.Id);
        return savedEmployee;
    }

    public async Task<Employee> Update(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}