using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Web.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        public ContactController()
        {

        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok();
        }

        [HttpGet("{page}?{size}")]
        public IActionResult Get(int page, int size)
        {
            return Ok();
        }
    }
}
