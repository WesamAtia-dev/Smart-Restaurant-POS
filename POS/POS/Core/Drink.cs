using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core
{
	// Task:
	// Drinks can be cold or hot.
	// Cold drinks have an extra charge.

	class Drink : Product
	{
		public bool IsCold { get; set; }

		public Drink(string name, double price, bool isCold)
			: base(name, price, Category.Drink)
		{
			// TODO
			this.IsCold = isCold;
		}

		public override void DisplayInfo()
		{
            // TODO:            

            // another way to do it:
            //	string type = IsCold ? "Cold" : "Hot";
            string type;
			if (IsCold)
			{
				type = "Cold";

			}
			else
			{
				type = "Hot";
			}
            

            Console.WriteLine($"Drink: {Name}, Category: {Category}, Type: {type}, Price: {CalculatePrice():F2}");
        }

		public override double CalculatePrice()
		{            
            return IsCold ? base.CalculatePrice() + 0.050 : base.CalculatePrice();
		}
	}
}
