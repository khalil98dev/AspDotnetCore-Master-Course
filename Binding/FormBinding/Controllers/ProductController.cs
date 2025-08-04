
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Controller-Invoice")]
public class ProductController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Get(IFormFile file)
    {
        if (file == null || file.Length == 0)
          return BadRequest("Uploaded Faild!");

       var Dir = Path.Combine(Directory.GetCurrentDirectory(),"Uploaded");

    if (!Directory.Exists(Dir))
    {
        Directory.CreateDirectory(Dir); 
    }

    var path = Path.Combine(Dir,"invoice",Path.GetExtension(file.FileName));

    using var stream = new FileStream(path, FileMode.Create);

         await file.CopyToAsync(stream);
        return Ok("Uploaded");
    }
}