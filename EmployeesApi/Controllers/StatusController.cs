using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class StatusController : Controller
    {
        // GET /status
        [HttpGet("/status")]
        public ActionResult GetTheServerStatus()
        {
            var response = new StatusResponse
            {
                Status = "Everything is running smoothly",
                CreatedAt = DateTime.Now
            };
            return Ok(response);
        }

        // GET /blogs/2020/3/12  (Route Parameters)
        [HttpGet("/blogs/{year:int}/{month:int:range(1,12)}/{day:int:range(1,31)}")]
        public ActionResult GetBlogPosts(int year, int month, int day)
        {
            return Ok($"Getting you the blog posts for {month}/{day}/{year}");
        }

        // GET /tickets?status=closed
        [HttpGet("/tickets")]
        public ActionResult GetTickets([FromQuery] string status = "all")
        {
            return Ok($"Getting you the {status} tickets...");
        }

        [HttpPost("/status")]
        public ActionResult SetStatus([FromBody] StatusRequest request)
        {
            // add it the database...
            return Ok(request);
        }
    }


    public class StatusRequest
    {
        public string Status { get; set; }
        public string SetBy { get; set; }
    }



    public class StatusResponse
    {
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
