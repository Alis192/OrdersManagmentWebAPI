using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Core.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "Product name can't be blank")]
        public string ProductName { get; set; }

        
        [Required(ErrorMessage = "Quantity can't be blank")]
        [Range(1, uint.MaxValue, ErrorMessage = "Only positive numbers are allowed")]
        public uint Quantity { get; set; }


        [Required(ErrorMessage = "Unit price can't be blank")]
        [Range(1, uint.MaxValue, ErrorMessage = "Only positive numbers are allowed")]
        public uint UnitPrice { get; set; } 

        public double TotalAmount
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }

        public Order? Order { get; set; }  //Navigation property

    }
}
