using EmployeesApi.Domain;
using EmployeesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    [Route("employees")]
    public class EmployeesController : Controller
    {
        EmployeesDataContext Context;

        public EmployeesController(EmployeesDataContext context)
        {
            Context = context;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> FireAnEmployee(int id)
        {
            // DEMO
            Thread.Sleep(3000);

            if(id == 1)
            {
                return StatusCode(403, new { message = "You cannot fire the CEO!" });
            }
            var employee = await Context.Employees
                .Where(e => e.Id == id && e.IsActive)
                .SingleOrDefaultAsync();

            if(employee != null)
            {
                employee.IsActive = false;
                await Context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPost("")]
        public async Task<ActionResult> HireAnEmployee([FromBody] EmployeePostRequest employeeToHire)
        {
            // 1 Make sure they posted a good thing (validation) -> 400 Bad Request
            // For demo purpose only!!!
            Thread.Sleep(3000); // pause here for three seconds to simulate a slow(er) API
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // 2 Add it to the database.
            //   a. Map our EmployeePostRequest -> Employee
            var employee = new Employee
            {
                FirstName = employeeToHire.FirstName,
                LastName = employeeToHire.LastName,
                Department = employeeToHire.Department,
                IsActive = true
            };
            //   b. Add the employee to the data context.
            Context.Employees.Add(employee);
            //    c. Save it to the datbase
            await Context.SaveChangesAsync();
            // 3. Create the response.
            //    a. 201 Created Status Code
            //    b. Add Location = url of the new resource (like Http://localhost:1337/employees/12)
            //    c. Just attach a copy of whatever they would get if they called the Location Url
            var response = new EmployeeDetailsResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department

            };

            return CreatedAtRoute("employees#getanemployee", new { id = response.Id }, response);
        }

        // GET /employees/1
        [HttpGet("{id:int}", Name ="employees#getanemployee")]
        public async Task<ActionResult> GetAnEmployee(int id)
        {
            var employee =  await Context.Employees
                .Where(e => e.IsActive && e.Id == id)
                .Select(e=> new EmployeeDetailsResponse
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Department = e.Department
                    
                })
                .SingleOrDefaultAsync();

            if(employee == null)
            {
                return NotFound();
            } else
            {
                return Ok(employee);
            }
        }


        // GET /employees
        [HttpGet("")]
        public ActionResult GetEmployees()
        {
            var model = new EmployeesGetResponse();

            var response = Context.Employees
                .Where(e => e.IsActive)
                .Select(e => new EmployeesGetResponseItem
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Department = e.Department
                })
                .ToList();

            model.Data = response;
            return Ok(model);
        }
    }
}
