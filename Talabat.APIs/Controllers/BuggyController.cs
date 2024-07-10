using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    
    public class BuggyController :BaseApiContoller
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notfound")] //api/Buggy/notfound
        public ActionResult GetNotFound()
        {
            var product = _storeContext.Product.Find(100);
            if(product == null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        [HttpGet("servererror")]  //api/Buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _storeContext.Product.Find(100);
            var productna=product.ToString();

            return Ok(productna);
        }

        [HttpGet("badrequest")]  //api/Buggy/badrequest
        public ActionResult Getbadrequest()
        {
           
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]   //api/Buggy/badrequest/five
        public ActionResult Getbadrequest(int id)
        {

            return Ok();
        }
        [HttpGet("unautherize")]  //api/Buggy/unautherize/100
        public ActionResult GetUnAutherize()
        {

            return Unauthorized(new ApiResponse(401));
        }

    }
}
