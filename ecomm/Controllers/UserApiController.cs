using ecomm.Application.Interfaces;
using ecomm.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserServices _user;
        private readonly ICategoryServies _categ;
        private readonly IProductServices _prod;

        public UserApiController(ICategoryServies categ,IProductServices prod)
        {
            
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

        [HttpGet("categproducts")]
        public IActionResult CategProduct([FromQuery]int? cid)
        {
            var categ_prod = _prod.CategProduct(cid);
            return Ok(categ_prod);
        }

        
        
        

    }
}
