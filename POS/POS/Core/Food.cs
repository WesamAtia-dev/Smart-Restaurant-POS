using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core
{
	// Task:
	// Food inherits from Product.
	// Add a calories property.
	// Food price should include a small calories-based increase.

	class Food : Product
	{
		public int Calories { get; set; }

		public Food(string name, double price, int calories)
			: base(name, price, Category.MainCourse)
		{
			// TODO
			this.Calories = calories;
		}

		public override void DisplayInfo()
		{
            // TODO:
            // string interpolation: {variable:format}
            Console.WriteLine($"Food: {Name}, Category: {Category}, Calories: {Calories}, Price: {CalculatePrice():F2}");
        }

		public override double CalculatePrice()
		{
            // Code Reusability: Call the base method to get the basic price, then add the cold drink surcharge if applicable.
            return base.CalculatePrice() + (Calories * 0.001);
		}
	}
}
