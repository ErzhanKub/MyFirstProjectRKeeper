using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace РесторанUpdraid
{
    internal class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public int Count { get; set; }

        public Dish(int id, string name, int price, int count)
        {
            Id = id;
            Name = name;
            Price = price;
            Count = count;
        }
    }
}
