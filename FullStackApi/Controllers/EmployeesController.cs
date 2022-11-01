using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees=await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee emplyeeRequest)
        {
            emplyeeRequest.Id=Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(emplyeeRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emplyeeRequest);
        }
    }

}
