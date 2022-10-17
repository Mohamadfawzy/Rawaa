using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Services.ControlPanel;

namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Services.ControlPanel.ProductData data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;
        private readonly IMapper mapper;
        private readonly IProductData data;

        public ProductController(IWebHostEnvironment webHost, IMapper mapper, IProductData _data)
        {
            data = _data;
            //data = new Services.ControlPanel.ProductData(mapper);
            this.webHost = webHost;
            this.mapper = mapper;
            fileProcessor = new FileProcessor(this.webHost);
        }

        [HttpPost]
        public IActionResult PostSingle([FromForm] IFormFile? image, [FromForm] ProductRequest model, string lang)
        {
            if (string.IsNullOrEmpty(model.TitleAr))
            {
                ModelState.AddModelError(nameof(model.TitleAr), "models title ar is requird");
                return ValidationProblem();
            }

            //if (!ModelState.IsValid)
            //{
            //    //return BadRequest();
            //    return ValidationProblem();
            //}

            if (string.IsNullOrEmpty(model.TitleAr) || string.IsNullOrEmpty(model.TitleEn))
                return BadRequest(new { StatusCode = 400, Message = $"The Title field is required." });

            if (image is null)
                return BadRequest(new ErrorClass("400", $"The {nameof(image)} field is required"));

            var imageExtension = fileProcessor.ImageExtension(image.FileName);
            model.Image = imageExtension;
            var entity = data.Add(model, imageExtension);

            if (entity.Image is not null)
                _ = fileProcessor.SaveImage(image, entity.Image);

            return CreatedAtAction(nameof(GetSingle), new { id = 1, lang = lang }, entity);


            //return CreatedAtRoute("SingleProductRoute", new { id = 1, lang = "eeeeeeee" }, new { myObject = "object" });
            //return Created("asdfa/asdasdjsdkj/haweihGetSingle", new { post = "psot" });
        }

        [HttpGet("{id}", Name = "SingleProductRoute")]
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


        // put image in product
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
                return NotFound(new ErrorClass("404", "the Product your want Update not found"));

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
                return NotFound(new ErrorClass("404", "the product your want delate not found"));
            _ = fileProcessor.RemoveImage(result.Image);
            return Ok(result);
        }
        
    }
}
