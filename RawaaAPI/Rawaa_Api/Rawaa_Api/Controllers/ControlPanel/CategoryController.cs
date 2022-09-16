using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.ControlPanel;
using Rawaa_Api.Services.Interfaces;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryData data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        public CategoryController(IProvider<CategoryRq> _data, IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            data = new CategoryData();
            fileProcessor = new FileProcessor(this.webHost);
        }


        [HttpPost]
        public IActionResult PostSingle([FromForm] IFormFile? image, [FromForm] CategoryRq model, string lang)
        {
            if (string.IsNullOrEmpty(model.TitleAr))
                return BadRequest(new { StatusCode = 400, Message = $"The {nameof(model.TitleAr)} field is required." });

            if (image == null)
                return BadRequest(new ErrorClass("400", $"The {nameof(image)} field is required"));

            var imageExtension = fileProcessor.ImageExtension(image.FileName);
            model.Image = imageExtension;
            var entity = data.Add(model);
            var imageName = fileProcessor.SaveImage(image, entity.Image);

            //CreatedAtAction(nameof(GetId), new { id = result.Id }, result); // id == param id in func GetIt
            return Created("", entity);// Created("", new { entity.Id, entity.Image, entity.TitleAr, entity.TitleEn });
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id, string lang)
        {
            var result = data.Find(id);
            if (result == null)
                return NotFound(new ErrorClass("404", $"the category id: {id} not found"));
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAll(string lang)
        {
            var result = data.List(lang);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no categories to display"));

            return Ok(result);
        }

        [HttpGet("Search/{text}")]
        public IActionResult GetAllSearch(string text)
        {
            var result = data.Search(text);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no categories to display"));
            return Ok(result);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] IFormFile? image, [FromForm] CategoryRq model, string lang)
        {
            if (string.IsNullOrEmpty(model.TitleAr) && string.IsNullOrEmpty(model.TitleEn))
                return BadRequest(new ErrorClass("404", "please check input"));

            var thereImage = image != null ? true : false;
            if (image != null)
                model.Image = fileProcessor.ImageExtension(image.FileName);
            var entity = data.Update(id, model, lang, thereImage);
            if (entity == null)
                return NotFound(new ErrorClass("404", "the category your wannt Update not found"));

            if (!string.IsNullOrEmpty(entity.Image) && thereImage)
            {
                // add new image
                var imageName = fileProcessor.UpdateImage(image, entity.Image);
            }
            return Ok(entity);
        }

        // put image
        [HttpPut("image/{id}")]
        public IActionResult PutImage(int id, [FromForm] IFormFile? image)
        {
            var thereImage = image != null ? true : false;
            var extension = "";
            if (image != null)
                extension = fileProcessor.ImageExtension(image.FileName);

            if (image == null)
                return NotFound(new ErrorClass("404", "Please insert image"));

            var entity = data.UpdateImage(id, extension);

            if (entity == null)
                return NotFound(new ErrorClass("404", "the Category your want Update not found"));

            if (!string.IsNullOrEmpty(entity.Image) && thereImage)
            {
                // add new image
                _ = fileProcessor.UpdateImage(image, entity.Image);
            }
            return Ok(entity);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string lang)
        {
            var result = data.Delete(id);
            if (result == null)
                return NotFound(new ErrorClass("404", "the category your wannt delate not found"));

            _ = fileProcessor.RemoveImage(result.Image);
            _ = fileProcessor.RemoveImages(result.ProductsImages);


            return Ok(result);
        }

    }
}
