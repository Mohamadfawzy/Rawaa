using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;


namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly IProvider<ProductRequest> data;
        Services.ControlPanel.ProductData data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        public ProductController(IWebHostEnvironment webHost)
        {
            //data = _data;
            data = new Services.ControlPanel.ProductData(webHost);
            this.webHost = webHost;
            fileProcessor = new FileProcessor(this.webHost);
        }

        [HttpPost]
        public IActionResult PostSingle([FromForm] IFormFile? image, [FromForm] ProductRequest model, string lang)
        {
            if (string.IsNullOrEmpty(model.TitleAr) || string.IsNullOrEmpty(model.TitleEn))
                return BadRequest(new { StatusCode = 400, Message = $"The Title field is required." });

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
                return NotFound(new ErrorClass("404", $"the Product id: {id} not found"));
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAll(string lang)
        {
            var result = data.List(lang);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no Products to display"));

            return Ok(data.List(lang));
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
        public IActionResult Put(int id, [FromForm] IFormFile? image, [FromForm] ProductRequest model, string lang)
        {
            var thereImage = image != null ? true : false;
            if (image != null)
                model.Image = fileProcessor.ImageExtension(image.FileName);
            var entity = data.Update(id, model, lang, thereImage);
            if (entity == null)
                return NotFound(new ErrorClass("404", "the Product your want Update not found"));

            if (!string.IsNullOrEmpty(entity.Image) && thereImage)
            {
                // add new image
                var imageName = fileProcessor.UpdateImage(image, entity.Image);
            }
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string lang)
        {
            var result = data.Delete(id);
            if (result == null)
                return NotFound(new ErrorClass("404", "the product your want delate not found"));
            _ = fileProcessor.RemoveImage(result.Image);
            return Ok(result);
        }
    }
}
