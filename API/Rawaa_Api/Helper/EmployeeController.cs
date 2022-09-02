
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rawaa_Api.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public EmployeeController(ApplicationDbContext _db)
        {
            db = _db;
        }
        [HttpGet("emp")]
        public IActionResult GetEmployeeName()
        {

            var all = db.Authors.ToList();
            return Ok(all);
        }

        [HttpGet("image")]
        public IActionResult Get()
        {
            return PhysicalFile(@"E:\\Test.jpg", "image/jpeg");
        }
    }
}
