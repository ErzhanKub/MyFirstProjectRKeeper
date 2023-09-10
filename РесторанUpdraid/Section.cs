using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace РесторанUpdraid
{
    internal class Section
    {
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
        public Section(string name, List<Dish> dishes)
        {
            Name = name;
            Dishes = dishes;
        }
    }
}
