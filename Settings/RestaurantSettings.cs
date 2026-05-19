using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Settings
{
    // Task:
    // This class stores shared restaurant settings.
    /* 
	* WHY STATIC?
	* 1. Global Access: Provides a single, centralized place for shared data (e.g., Tax Rate, Restaurant Name).
	* 2. Memory Efficiency: Loaded once when the program starts and stays in memory; no need to create multiple instances (new objects).
	* 3. Utility Driven: Ideal for constant settings and calculation methods that don't change per order. (e.g., CalculateTax())
	* 
	* WHEN TO USE? 
	* Use 'static' when the data or method belongs to the class itself, rather than a specific object.
	*/

    static class RestaurantSettings
	{
		public static string RestaurantName = "Smart Restaurant POS";
		public static double TaxRate = 0.10;
		public static double ServiceChargeRate = 0.05;

		public static double CalculateTax(double amount)
		{
			// TODO:		
			return amount * TaxRate;
		}

		public static double CalculateServiceCharge(double amount)
		{
			// TODO:
			return amount * ServiceChargeRate;
		}
	}
}
