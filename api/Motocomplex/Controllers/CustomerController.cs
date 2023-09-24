using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {

        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer()
        {

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer()
        {

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer()
        {

        }
    }
}
