using POS.Core;
using POS.Payment;
using POS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace POS.Order
{
	// Order class
	class Order
	{
		public List<OrderItem> Items { get; set; }
		public IPaymentMethod PaymentMethod { get; set; }
		public double DiscountPercent { get; set; }
		public Order()
		{
			// TODO
			Items = new List<OrderItem>();
			DiscountPercent = 0;
		}

		public void AddItem(OrderItem item)
		{
			// TODO
			Items.Add(item);
		}

		public void ViewCurrentOrder()
		{
			Console.WriteLine();
			Console.WriteLine("=============== CURRENT ORDER ===============");

			if (Items.Count == 0)
			{
				Console.WriteLine("The order is empty.");
				Console.WriteLine("=============================================");
				return;
			}

            // String Alignment: -5 means left align in a 5 character width, 10 means right align in a 10 character width
            Console.WriteLine($"{"No.",-5} {"Item",-20} {"Qty",5} {"Unit",10} {"Total",10}");
			Console.WriteLine("---------------------------------------------");

			for (int i = 0; i < Items.Count; i++)
			{
				OrderItem item = Items[i];
				Console.WriteLine($"{i + 1,-5} {item.Product.Name,-20} {item.Quantity,5} {item.GetProductPriceWithAddOns(),10:F3} {item.GetLineTotal(),10:F3}");

				if (item.AddOns.Count > 0)
				{
					Console.WriteLine($"      Add-ons: {item.GetAddOnsText()}");
				}
			}

			Console.WriteLine("---------------------------------------------");
			Console.WriteLine($"Current Subtotal: {CalculateSubtotal():F3} KD");
			Console.WriteLine("=============================================");
		}

		public void RemoveItem(int itemNumber)
		{
            // TODO
            
            if (itemNumber >= 1 && itemNumber <= Items.Count)
            {
                // itemNumber starts from 1 for the user, but list index starts from 0, so we need to subtract 1
                Items.RemoveAt(itemNumber - 1);
                Console.WriteLine("Item removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid item number.");
            }
        }

		public void UpdateQuantity(int itemNumber, int newQuantity)
		{
            // TODO
            if (itemNumber >= 1 && itemNumber <= Items.Count)
            {
                if (newQuantity > 0)
                {
                    Items[itemNumber - 1].Quantity = newQuantity;
                    Console.WriteLine("Quantity updated.");
                }
                else
                {
                    // If new quantity is 0 or less, we can consider it as removing the item from the order
                    RemoveItem(itemNumber);
                }
            }
        }

		public double CalculateSubtotal()
		{
			// TODO
			double subtotal = 0;
			foreach (OrderItem item in Items)
			{
				subtotal += item.GetLineTotal();
            }
			return subtotal;
		}

		public double CalculateDiscountAmount()
		{
			// TODO
			return CalculateSubtotal() * (DiscountPercent / 100);
		}

		public double CalculateSubtotalAfterDiscount()
		{
			// TODO
			return CalculateSubtotal() - CalculateDiscountAmount();
		}

		public double CalculateServiceCharge()
		{
            // TODO
            // depends on restaurant settings, but for now we can return a fixed value
			double subtotalAfterDiscount = CalculateSubtotalAfterDiscount();
            return RestaurantSettings.CalculateServiceCharge(subtotalAfterDiscount);
		}

		public double CalculateTax()
		{
			double taxableAmount = CalculateSubtotalAfterDiscount() + CalculateServiceCharge();
			return RestaurantSettings.CalculateTax(taxableAmount);
		}

		public double CalculateFinalTotal()
		{
			return CalculateSubtotalAfterDiscount() + CalculateServiceCharge() + CalculateTax();
		}

		public string GenerateInvoiceText()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("==============================================================");
			sb.AppendLine($"                  {RestaurantSettings.RestaurantName}");
			sb.AppendLine("==============================================================");
			sb.AppendLine(string.Format("{0,-5} {1,-20} {2,5} {3,10} {4,12}", "No.", "Item", "Qty", "Unit", "Total"));
			sb.AppendLine("--------------------------------------------------------------");

			for (int i = 0; i < Items.Count; i++)
			{
				OrderItem item = Items[i];

				sb.AppendLine(string.Format("{0,-5} {1,-20} {2,5} {3,10:F3} {4,12:F3}",
					i + 1,
					item.Product.Name,
					item.Quantity,
					item.GetProductPriceWithAddOns(),
					item.GetLineTotal()));

				if (item.AddOns.Count > 0)
				{
					sb.AppendLine($"      Add-ons: {item.GetAddOnsText()}");
				}
			}

			sb.AppendLine("--------------------------------------------------------------");
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "Subtotal:", CalculateSubtotal()));
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "Discount:", CalculateDiscountAmount()));
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "After Discount:", CalculateSubtotalAfterDiscount()));
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "Service Charge:", CalculateServiceCharge()));
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "Tax:", CalculateTax()));
			sb.AppendLine(string.Format("{0,-35} {1,15:F3} KD", "Final Total:", CalculateFinalTotal()));
			sb.AppendLine("--------------------------------------------------------------");

			if (PaymentMethod != null)
			{
				sb.AppendLine(PaymentMethod.GetPaymentDetails());
			}
			else
			{
				sb.AppendLine("Payment Method: Not Selected");
			}

			sb.AppendLine("==============================================================");

			return sb.ToString();
		}

		public void PrintInvoice()
		{
			Console.WriteLine();
			Console.WriteLine(GenerateInvoiceText());
			PaymentMethod.PrintReceipt();
		}

		public void SaveInvoiceToFile(string filePath)
		{
            // TODO
            // Error Handling: If the file cannot be written, catch the exception and display an error message.
            try
            {				 				
                File.WriteAllText(filePath, GenerateInvoiceText());
                Console.WriteLine("Invoice saved to file.");
            }
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving file: {ex.Message}");
			}
		}
	}
}
