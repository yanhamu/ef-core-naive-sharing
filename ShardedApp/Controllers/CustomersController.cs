using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShardedApp.DataAccess;
using System;
using System.Threading.Tasks;

namespace ShardedApp.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ShardingContext context;

        public CustomersController(ShardingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await this.context.Set<Customer>().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var customer = new Customer() { Id = Guid.NewGuid() };
            var result = await this.context.Set<Customer>().AddAsync(customer);
            await context.SaveChangesAsync();
            return Ok(result.Entity);
        }
    }
}
