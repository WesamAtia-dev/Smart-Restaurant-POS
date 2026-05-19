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
		private int sugarGrams;
        public int SugarGrams 
		{ 
			get {  return sugarGrams; }
			set
			{
				if(value < 0)
                    throw new ArgumentOutOfRangeException("Sugar grams cannot be negative.");
                sugarGrams = value;
			}
		}

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
