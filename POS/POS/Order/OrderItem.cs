using POS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Order
{
	// Task:
	// One order item contains:
	// - one product
	// - quantity
	// - optional add-ons

	class OrderItem
	{
		private Product product;
		private int quantity;
		public Product Product 
		{ 
			get { return product; }
			set
			{
				if(value == null)
					throw new ArgumentNullException("Product cannot be null.");
				product = value;
			}
        }
		public int Quantity
		{
			get { return quantity; }
			set
			{

				if(value <= 0)
					throw new ArgumentOutOfRangeException("Quantity must be greater than zero.");
				quantity = value;
			}
		}
		public List<AddOn> AddOns { get; set; }

		public OrderItem(Product product, int quantity)
		{
			// TODO:
			this.Product = product;
			this.Quantity = quantity;
			this.AddOns = new List<AddOn>();
		}

		public void AddAddOn(AddOn addOn)
		{
			// TODO						
			if(addOn == null)
                throw new ArgumentNullException("Add-on cannot be null.");
            AddOns.Add(addOn);
        }

		public double GetAddOnsTotalPerProduct()
		{
			double total = 0;

			// TODO:
			foreach (AddOn addOn in AddOns)
			{
				total += addOn.Price;
			}

			return total;
		}

		public double GetProductPriceWithAddOns()
		{
			// TODO:			
			double finalPrice = Product.CalculatePrice() + GetAddOnsTotalPerProduct();

			return finalPrice;
		}

		public double GetLineTotal()
		{
			// TODO:
			double lineTotal = GetProductPriceWithAddOns() * Quantity;

			return lineTotal;
		}

		public string GetAddOnsText()
		{
			// TODO:			
			if (AddOns.Count == 0)
			{
				return "None";
			}
			else
			{
                // Use string.Join to concatenate the add-ons with a comma and space as a separator.
                //	will call the ToString() method of each AddOn object to get its string representation.
                return string.Join(", ", AddOns); 
			}
		}
	}
}
