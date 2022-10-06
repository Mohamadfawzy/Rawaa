using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rawaa_Api.Controllers.Client
{
    [Route("api/client/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly UserData data;
        public UserController()
        {
            data = new UserData();
        }
        [HttpPost]
        public IActionResult PostSingle([FromBody] Customer user)
        {
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new ErrorClass("400", "fullName,Password are required"));
            }
            if (string.IsNullOrEmpty(user.Email) || user.Email.Contains(" "))
            {
                return BadRequest(new ErrorClass("400", "Email is invalid"));
            }
            else
            {
                var isValed = data.checkEmail(user.Email);
                if (isValed)
                    return BadRequest(new ErrorClass("400", "email is exist"));
            }
            // add
            var result = data.Add(user);
            return Created("", result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequest user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new ErrorClass("400", "Password are required"));
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                if (user.Email.Contains(" "))
                {
                    return BadRequest(new ErrorClass("400", "Email is invalid"));
                }
                else
                {
                    var isValed = data.checkEmail(user.Email);
                    if (!isValed)
                        return BadRequest(new ErrorClass("400", "email is not exist"));
                }
            }
           
            // add
            var result = data.Login(user);
            if(result == null)
            {
                return BadRequest(new ErrorClass("400", "Password or email is invalid"));
            }
            return Created("", result);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new ErrorClass("400", "Password are required"));
            }
            if (string.IsNullOrEmpty(user.Email) || user.Email.Contains(" "))
            {
                return BadRequest(new ErrorClass("400", "Email is invalid"));
            }
            else
            {
                var isValed = data.EmailValidity(user.Email,id);
                if (isValed)
                    return BadRequest(new ErrorClass("400", "can not use this email"));
            }
            var result = data.Update(id, user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] UserRequest user, int id )
        {
            var result = data.Delete(id, user.Password);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "check your password or user not found"));
        }


        // end clint

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
        public IActionResult GetAll()
        {
            var result = data.List();
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

       
    }
}
