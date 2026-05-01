using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core
{
	// Task:
	// An add-on has a name and a price.
	// Override ToString() so it displays nicely.

	class AddOn
	{
		public string Name { get; set; }
		public double Price { get; set; }

		public AddOn(string name, double price)
		{
			// TODO
			this.Name = name;
			this.Price = price;
        }

        // ToString() is a method that every object has, and it is used to convert the object to a string representation.
        public override string ToString()
		{
			// Expected format:
			// Extra Cheese (0.300 KD)

			return $"{Name} ({Price:F3} KD)";
		}
	}
}
