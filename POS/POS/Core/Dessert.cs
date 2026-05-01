using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core
{
	// Dessert class inherits from Product
	// Desserts have sugar grams
	// const Dessert(...)
	// DisplayInfo()
	// Add a sugar-based price increase Price + (SugarGrams * 0.002)
	class Dessert : Product
	{
		public int SugarGrams { get; set; }

		public Dessert(string name, double price, int sugarGrams): base(name, price, Category.Dessert)
		{
			this.SugarGrams = sugarGrams;				
		}

        public override void DisplayInfo()
        {
            Console.WriteLine($"Dessert: {Name}, Category: {Category}, Sugar: {SugarGrams}g, Price: {CalculatePrice():F2}");
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + (SugarGrams * 0.002);
        }
    }

}
