using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core
{
	// Task:
	// 1) Complete the constructor.
	// 2) Make this class a base class for all products.
	// 3) Each product type must display its own info.

	abstract class Product
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public Category Category { get; set; }

		public Product(string name, double price, Category category)
		{
			// TODO:
			this.Name = name;

			if(price<0)
				throw new ArgumentOutOfRangeException("Price cannot be negative.");
			this.Price = price;

			this.Category = category;						
		}

		public abstract void DisplayInfo();

		public virtual double CalculatePrice()
		{
			// TODO:
			return Price;
		}		
	}


}
