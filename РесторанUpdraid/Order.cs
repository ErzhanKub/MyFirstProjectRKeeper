using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace РесторанUpdraid
{
    internal class Order
    {
        public int OrderNumber { get; set; }
        public List<Dish> Dishes { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }



        public Order(int ordernumber, decimal totalPrice, List<Dish>dishes, DateTime data)
        {
            OrderNumber = ordernumber;
            Dishes = dishes;
            TotalPrice = totalPrice;
            OrderTime= data;
        }
    }
}
