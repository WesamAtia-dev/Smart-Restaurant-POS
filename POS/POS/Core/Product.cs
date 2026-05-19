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
		private string name;
        private double price;

        public string Name 
		{ 
			get { return name; }
			set
			{
				if(string.IsNullOrWhiteSpace(value))
                    // throw Exception: Custom Exception
                    throw new ArgumentException("Product name cannot be empty.");
				name = value;
            }
		}
		public double Price 
		{
			get { return price; }
			set
			{
				if(value < 0)
                    // throw Exception: Custom Exception
                    throw new ArgumentOutOfRangeException("Price cannot be negative.");
                price = value;
			}
        }
		public Category Category { get; set; }

		public Product(string name, double price, Category category)
		{
			// TODO:
			this.Name = name;
			
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
