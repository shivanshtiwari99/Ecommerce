using ecomm.Application.Interfaces;
using ecomm.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IUserServices _user;
        private readonly ICategoryServies _categ;
        private readonly IProductServices _prod;

        public AdminApiController(IUserServices user,ICategoryServies categ, IProductServices prod)
        {
            _user = user;
            _categ = categ;
            _prod = prod;
        }
        [HttpGet("categories")]
        public IActionResult AllCategory()
        {
            var all_categ = _categ.AllCategory();
            return Ok(all_categ);
        }
        [HttpGet("products")]
        public IActionResult AllProduct()
        {
            var all_prod = _prod.AllProduct();
            return Ok(all_prod);
        }
        [HttpGet("users")]
        public IActionResult AllUser()
        {
            var all_user = _user.GetUser();
            return Ok(all_user);
        }
        [HttpGet("dashcount")]
        public IActionResult DashboardCount()
        {
            var data = new
            {
                totalUsers = _user.GetUser().Count,
                totalCategories = _categ.AllCategory().Count,
                totalProducts = _prod.AllProduct().Count
            };

            return Ok(data);
        }
        [HttpPost("addcategory")]
        public IActionResult AddCategory([FromForm] Category categ, [FromForm] IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // 1️⃣ Folder path
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos/Category");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // 2️⃣ Original file name lo
                string fileName = Path.GetFileName(imageFile.FileName);

                // 3️⃣ Full path banao
                string fullPath = Path.Combine(folderPath, fileName);

                // 4️⃣ Image save karo
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // 5️⃣ DB me path set karo
                categ.c_picture = "/Photos/Category/" + fileName;
            }
            _categ.AddCategory(categ);
            return Ok("Category Added Successfully");
        }

        [HttpPost("addproduct")]
        public IActionResult AddProduct([FromForm] Product prod, [FromForm] IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // 1️⃣ Folder path
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos/Product");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // 2️⃣ Original file name lo
                string fileName = Path.GetFileName(imageFile.FileName);

                // 3️⃣ Full path banao
                string fullPath = Path.Combine(folderPath, fileName);

                // 4️⃣ Image save karo
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // 5️⃣ DB me path set karo
                prod.p_picture = "/Photos/Product/" + fileName;
            }
            _prod.AddProduct(prod);
            return Ok("Product Added Successfully");
        }
        [HttpDelete("deleteCateg")]
        public IActionResult DeleteCategory([FromQuery]int? cid)
        {
            return Ok("Category Deleted Successfully");
        }
    }
}
