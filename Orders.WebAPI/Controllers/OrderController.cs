using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Core.Entities;
using OrdersManager.Infrastructure.DatabaseContext;

namespace Orders.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Retrieves all Orders from Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var order = await _dbContext.Orders.ToListAsync();
            
            return Ok(order);
        }

        /// <summary>
        /// Retrieves an Order by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        /// <summary>
        /// Updates an existing Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [Bind(nameof(Order.CustomerName))] Order order)
        {
            if (orderId != order.OrderID) //If route ID value and ID value from request body don't match
            {
                return BadRequest();
            }

            Order existingOrder = await _dbContext.Orders.FindAsync(orderId); //Getting specific order which exists in database with given ID 

            if (existingOrder == null) //If no order found with given ID
            {
                return NotFound();
            }

            existingOrder.CustomerName = order.CustomerName; //Modifying Property of existing order from DB
            
            try
            {
                await _dbContext.SaveChangesAsync(); //Updating database with new property value
            } 
            catch (DbUpdateConcurrencyException) //If multiple changes made to DB simultaneously, an exception will be generated
            {
                if (!OrderExists(orderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        /// <summary>
        /// Creates a new Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([Bind(nameof(Order.CustomerName))] Order order) 
        {
            //Internally this code is created by AspNetCore

            //if (ModelState.IsValid == false)
            //{
            //    return ValidationProblem(ModelState);
            //}

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetOrderById", new { id = order.OrderID}, order);
        }


        /// <summary>
        /// Deletes an Order with its ID value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }


            _dbContext.Orders.Remove(order);

            var orderItems = _dbContext.OrdersItem.Where(item => item.OrderId == id);
            _dbContext.OrdersItem.RemoveRange(orderItems);
            await _dbContext.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }


        /// <summary>
        /// Creates a new Order Item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>

        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItem>> PostOrderItem([Bind(nameof(OrderItem.OrderId), nameof(OrderItem.ProductName), nameof(OrderItem.Quantity), nameof(OrderItem.UnitPrice))] OrderItem orderItem, Guid orderId)
        {
            //Internally this code is created by AspNetCore

            //if (ModelState.IsValid == false)
            //{
            //    return ValidationProblem(ModelState);
            //}


            Order order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                return BadRequest();
            }

            orderItem.OrderId = orderId;
            _dbContext.OrdersItem.Add(orderItem);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetOrderById", new { id = orderItem.OrderId }, orderItem);
        }


        /// <summary>
        /// Updates a specific order item entity in Database
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <param name="orderId"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        [HttpPut("{orderId}/items/{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(Guid orderItemId, Guid orderId, [Bind(nameof(OrderItem.ProductName), nameof(OrderItem.Quantity), nameof(OrderItem.UnitPrice))] OrderItem orderItem)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId); //Checks if any order exists with given ID in DB

            if (order == null) //If no match found, returns NotFound
            {
                return NotFound();
            }

            //if (orderItemId != orderItem.OrderId) //If route ID value and ID value from request body don't match
            //{
            //    return BadRequest();
            //}

            OrderItem existingOrderItem = await _dbContext.OrdersItem.FindAsync(orderItemId); //Getting specific order which exists in database with given ID 

            if (existingOrderItem == null) //If no order found with given ID
            {
                return NotFound();
            }

            if (existingOrderItem.OrderId != orderId) 
            {
                return BadRequest();
            }


            //Modifying Property of existing order from DB
            existingOrderItem.ProductName = orderItem.ProductName;
            existingOrderItem.Quantity= orderItem.Quantity;
            existingOrderItem.UnitPrice = orderItem.UnitPrice;


            try
            {
                await _dbContext.SaveChangesAsync(); //Updating database with new property value
            }
            catch (DbUpdateConcurrencyException) //If multiple changes made to DB simultaneously, an exception will be generated
            {
                if (!OrderItemExists(orderItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Order item entity by its ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderItemsId"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}/items/{orderItemsId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid orderItemsId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            OrderItem orderItem = await _dbContext.OrdersItem.FindAsync(orderItemsId);


            if (orderItem == null)
            {
                return NotFound();
            }

            if (orderItem.OrderId != orderId)
            {
                return BadRequest();
            }


            _dbContext.OrdersItem.Remove(orderItem);
            await _dbContext.SaveChangesAsync();

            return NoContent(); //HTTP 200
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
