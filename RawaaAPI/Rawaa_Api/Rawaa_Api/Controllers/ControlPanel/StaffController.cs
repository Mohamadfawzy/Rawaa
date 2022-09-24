using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.ControlPanel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffData data;

        // ctor
        public StaffController()
        {
            data = new StaffData();
        }

        [HttpPost]
        public IActionResult PostSingle([FromBody] Staff staff)
        {
            if(string.IsNullOrEmpty(staff.FullName) || string.IsNullOrEmpty(staff.Password))
            {
                return BadRequest(new ErrorClass("400", "fullName,Password are required"));
            }
            if (string.IsNullOrEmpty(staff.UserName) || staff.UserName.Contains(" "))
            {
                return BadRequest(new ErrorClass("400", "user Name is invalid"));
            }
            else
            {
                var isValed = data.checkUserName(staff.UserName);
                if(isValed)
                    return BadRequest(new ErrorClass("400", "user Name is exist please choose another userName"));
            }
            if(staff.Jop== "admin")
            {
                return BadRequest(new ErrorClass("400", "can not add admin jop from web. we add admin from just database"));
            }
            // add
            var result = data.Add(staff);
            return Created("", result);
        }

        // login
        [HttpPost("login")]
        public IActionResult Login([FromBody] StaffRequest model)
        {
            if (string.IsNullOrEmpty(model.UserName) || model.UserName.Contains(" "))
            {
                return BadRequest(new ErrorClass("400", "user Name is invalid"));
            }
            
            var result = data.Login(model);
            if (result == null)
            {
                return BadRequest(new ErrorClass("400", "Password or email is invalid"));
            }
            return Created("", result);
        }
        // id
        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            if (id < 1)
                return BadRequest(new ErrorClass("400", "id is invalid"));
            var res = data.Find(id);
            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }

        [HttpGet("all")]
        public IActionResult GetAll(string lang)
        {
            var result = data.List(lang);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        [HttpGet("Search/{text}")]
        public IActionResult GetAllSearch(string text)
        {
            var result = data.Search(text);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // update 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Staff model)
        {
            var result = data.Update(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string lang)
        {
            var result = data.Delete(id);
            if(result!=null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "can not delete admin or id not found"));
        }
    }
}
