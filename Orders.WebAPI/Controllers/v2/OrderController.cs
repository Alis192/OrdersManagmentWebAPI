using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Core.Entities;
using OrdersManager.Infrastructure.DatabaseContext;

namespace Orders.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class OrderController : CustomControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Retrieves only order names with customer name alphabetic order 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var order = await _dbContext.Orders.OrderBy(temp => temp.CustomerName).Select(temp => temp.CustomerName).ToListAsync();

            return Ok(order);
        }

        private bool OrderExists(Guid id)
        {
            return (_dbContext.Orders?.Any(o => o.OrderID == id)).GetValueOrDefault();
        }

        private bool OrderItemExists(Guid id)
        {
            return (_dbContext.OrdersItem?.Any(o => o.OrderItemId == id)).GetValueOrDefault();
        }
    }
}
