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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(
                x=>x.Id==id);
            if(employee==null)
                return NotFound(employee);
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        {

            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(employee);

            //set details
            employee.Name=updateEmployeeRequest.Name;
            employee.Email=updateEmployeeRequest.Email;
            employee.Phone=updateEmployeeRequest.Phone;
            employee.Salary=updateEmployeeRequest.Salary;
            employee.Department=updateEmployeeRequest.Department;
            //

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {

            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(employee);

            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }

}
