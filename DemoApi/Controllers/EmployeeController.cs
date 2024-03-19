using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly DemoContext dc;
        public EmployeeController(DemoContext dc)
        {
            this.dc = dc;
        }
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of Employees</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            Employee[] e = await dc.Employees.ToArrayAsync();
            return Ok(e);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">The id of the employee</param>
        /// <returns>An employee object</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployee(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("No Employee Id Specified");
            }

            Employee e = await dc.Employees.FindAsync(id);

            if (e == null)
            {
                return NotFound("No Employee Found");
            }
            return Ok(e);
        }
        /// <summary>
        /// Add an employee
        /// </summary>
        /// <param name="e">The employee data without id</param>
        /// <returns>The created employee with ID</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Employee>> AddEmployee([Bind(include: "EName, ESalary")] Employee e)
        {
            dc.Employees.Add(e);
            await dc.SaveChangesAsync(); //201  Resource Created
            return CreatedAtAction(nameof(GetEmployee), new { id = e.EID }, e);
        }
    }
}
