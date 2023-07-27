using Intern.API.Models;
using Intern.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Intern.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> ListEmployees();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeModel>> ListEmployees()
        {
            var list = await _context.Employees.ToListAsync();

            return list.ConvertAll(e => new EmployeeModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName
            });
        }
    }
}
