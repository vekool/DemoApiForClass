using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /// <summary>
   /// Employee Controller for CRUD operations on Employee table in DemoContext
   /// </summary>
    public class EmployeeController : ControllerBase
    {
        readonly DemoContext dc;
        /// <summary>
        /// Constructor for injecting Democontext by the framework
        /// </summary>
        /// <param name="dc"></param>
        public EmployeeController(DemoContext dc)
        {
            this.dc = dc;
        }
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of Employees</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDeptDTO>>> GetAllEmployees()
        {
            EmployeeDeptDTO[] edt = await (from e in dc.Employees
                                           join d in dc.Departments
                                           on e.DepartmentDID equals d.DID into t
                                           from d in t.DefaultIfEmpty()
                                           select new EmployeeDeptDTO
                                           {
                                               DepartmentName = d == null?"NA":d.DName,
                                              DeptId = d == null?0:d.DID,
                                               EName = e.EName,
                                               ESalary = e.ESalary,
                                               EID = e.EID
                                           }).ToArrayAsync();
            return Ok(edt);
            //Employee[] e = await dc.Employees.ToArrayAsync();
            //return Ok(e);
                //EmployeeDeptDTO[] edt = await (from e in dc.Employees
                //                               join d in dc.Departments
                //                               on e.DeptId equals d.DID
                //                               select new EmployeeDeptDTO
                //                               {
                //                                   DepartmentName = d.DName,
                //                                   DeptId = d.DID,
                //                                   EName = e.EName,
                //                                   ESalary = e.ESalary,
                //                                   EID = e.EID
                //                               }).ToArrayAsync();
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

        /// <summary>
        ///  Delete an employee
        /// </summary>
        /// <param name="id"> The id of the employee </param>
        /// <returns> The deleted employee</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> DeleteEmployee(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("No Employee Id Specified");
            }
            Employee e = await dc.Employees.FindAsync(id.Value);
            if (e == null)
            {
                return NotFound("No Employee Found");
            }
            dc.Employees.Remove(e);
            await dc.SaveChangesAsync();
            return Ok(e);
        }

        /// <summary>
        /// Update an employee
        /// </summary>
        /// <param name="id">The id to be edited</param>
        /// <param name="e"> The employee data</param>
        /// <returns> No Content</returns>

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> UpdateEmployee(int? id, Employee e)
        {
            if (!id.HasValue)
            {
                return NotFound("No Employee Id Specified");
            }
            if(id.Value != e.EID)
            {
                return BadRequest("ID mismatch");
            }
            dc.Entry(e).State = EntityState.Modified;
            //EF will automatcially generate the required update command
            try
            {
                await dc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dc.Employees.Any(emp => e.EID == emp.EID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            //200 range -- everything is ok
            //400 - error
            //500 - Server error
        }
    }
}
