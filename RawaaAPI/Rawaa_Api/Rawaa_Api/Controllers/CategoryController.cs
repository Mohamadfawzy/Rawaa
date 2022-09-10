using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Rawaa_Api.Controllers
{
    [Route("{lang}/api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProvider<Category> data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        // Constructor ================================================================
        public CategoryController(IProvider<Category> _data, IWebHostEnvironment webHost)
        {
            data = _data;
            this.webHost = webHost;
            this.fileProcessor = new FileProcessor(this.webHost);
        }

        [HttpPost("create")]
        public IActionResult PostSingle([FromForm] ImageUplod Files, [FromForm] Category model, string lang)
        {
            if (String.IsNullOrEmpty(model.Title))
                return BadRequest(new { StatusCode = 400, Message = $"The {nameof(model.Title)} field is required." });

            if (Files.Images == null)
                return BadRequest(new ErrorClass("400", $"The {nameof(Files.Images)} field is required"));

            var entity = data.Add(model);
            var imageName = fileProcessor.SaveImage(Files, entity.Image);

            //CreatedAtAction(nameof(GetId), new { id = result.Id }, result); // id == param id in func GetIt
            return Created("", new { Id = entity.Id, Image = entity.Image, Title = entity.Title, TitleEn = entity.TitleEn });
        }

        // PUT api/<AdController>/5
        [HttpPut("Update/{id}")]
        public IActionResult Put(int id, [FromForm] ImageUplod? image, [FromForm] Category model, string lang)
        {
            var thereImage = image.Images != null ? true : false;
            var entity = data.Update(id, model, lang, thereImage);


            if (!string.IsNullOrEmpty(entity.Image) && thereImage)
            {
                // add new image
                var imageName = fileProcessor.UpdateImage(image, entity.Image);
            }
            return Ok(entity);
        }

        [HttpGet("show_all")]
        public IActionResult GetAll(string lang)
        {
            return Ok(data.List(lang));
        }

        // DELETE api/<AdController>/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id,string lang)
        {
            var result = data.Delete(id);
            _= fileProcessor.RemoveImage(result.Image);
            return Ok(result);
        }

    }
}
