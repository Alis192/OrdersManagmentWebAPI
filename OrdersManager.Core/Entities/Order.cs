using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Core.Entities
{
    public class Order
    {
        private static int orderNumber = 0;
        public Order()
        {
            var dateYear = DateTime.Now.Year;
            OrderNumber = $"Order_{dateYear}_{orderNumber}";
            orderNumber++;
        }

        [Key]
        public Guid OrderID { get; set; }

        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Customer name can't be blank")]
        public string CustomerName { get; set; }
            
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public double TotalAmount
        {
            get
            {
                var sum = OrderItems?.Sum(orders => orders.TotalAmount) ?? 0;
                return sum;
            }
        }   

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

    }
}
