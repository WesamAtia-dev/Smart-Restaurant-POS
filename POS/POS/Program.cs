using POS.Core;
using POS.Order;
using POS.Payment;
using POS.Settings;

class Program
{
	static void Main(string[] args)
	{
		List<Product> menu = new List<Product>()
			{
			// TODO:
			new Food("Classic Burger", 2.500, 650),      // Name, Price, Calories
			new Food("Crispy Chicken", 2.250, 700),
            new Drink("Iced Latte", 1.500, true),        // Name, Price, IsCold
			new Drink("Hot Tea", 0.500, false),
            new Dessert("Chocolate Cake", 1.200, 45),    // Name, Price, SugarGrams
			new Dessert("Fruit Salad", 0.950, 15)
        };

		List<AddOn> availableAddOns = new List<AddOn>()
			{
			// TODO:
			new AddOn("Extra Cheese", 0.250),
            new AddOn("Jalapenos", 0.150),
            new AddOn("Ice Cubes", 0.000),
            new AddOn("Whipped Cream", 0.200)
        };

		Order currentOrder = new Order();
		bool running = true;

		while (running)
		{
			Console.WriteLine();
			Console.WriteLine($"============== {RestaurantSettings.RestaurantName} ==============");
			Console.WriteLine("1. Show Menu");
			Console.WriteLine("2. Add Item");
			Console.WriteLine("3. View Current Order");
			Console.WriteLine("4. Update Quantity");
			Console.WriteLine("5. Remove Item");
			Console.WriteLine("6. Apply Discount");
			Console.WriteLine("7. Checkout");
			Console.Write("Choose an option: ");
			string option = Console.ReadLine();

			switch (option)
			{
				case "1":
                    // TODO
                    ShowMenu(menu);
                    break;

				case "2":
                    // TODO
                    AddItemToOrder(menu, availableAddOns, currentOrder);
                    break;

				case "3":
                    // TODO
                    currentOrder.ViewCurrentOrder();
                    break;

				case "4":
                    // TODO
                    UpdateItemQuantity(currentOrder);
                    break;

				case "5":
                    // TODO
                    RemoveItemFromOrder(currentOrder);
                    break;

				case "6":
                    // TODO
                    ApplyDiscount(currentOrder);
                    break;

				case "7":
                    // TODO
                    Checkout(currentOrder);
                    // After successfully completing the payment, terminate the program
                    running = false;
					break;

				default:
					Console.WriteLine("Invalid option. Please try again.");
					break;
			}
		}

		Console.WriteLine();
        Console.WriteLine("Thank you for using Smart POS System. Visit us again!");
        Console.WriteLine("Press any key to exit...");
		Console.ReadKey();
	}

	static void ShowMenu(List<Product> menu)
	{
		Console.WriteLine();
		Console.WriteLine("================== MENU ==================");

		for (int i = 0; i < menu.Count; i++)
		{
			Console.Write($"{i + 1}. ");
			menu[i].DisplayInfo();
		}

		Console.WriteLine("==========================================");
	}

	static void ShowAddOns(List<AddOn> addOns)
	{
		Console.WriteLine();
		Console.WriteLine("Available Add-ons:");

		for (int i = 0; i < addOns.Count; i++)
		{
			Console.WriteLine($"{i + 1}. {addOns[i].Name} - {addOns[i].Price:F3} KD");
		}
	}

	static void AddItemToOrder(List<Product> menu, List<AddOn> availableAddOns, Order order)
	{
		ShowMenu(menu);

		Console.Write("Choose product number: ");
		int productChoice;
		if (!int.TryParse(Console.ReadLine(), out productChoice) ||
			productChoice < 1 || productChoice > menu.Count)
		{
			Console.WriteLine("Invalid product choice.");
			return;
		}

		Console.Write("Enter quantity: ");
		int quantity;
		if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
		{
			Console.WriteLine("Invalid quantity.");
			return;
		}

		Product selectedProduct = menu[productChoice - 1];
		OrderItem orderItem = new OrderItem(selectedProduct, quantity);

		Console.Write("Do you want to add add-ons? (y/n): ");
		string addOnChoice = Console.ReadLine();

		if (addOnChoice.ToLower() == "y")
		{
			bool addingAddOns = true;

			while (addingAddOns)
			{
				ShowAddOns(availableAddOns);

				Console.Write("Choose add-on number: ");
				int addOnNumber;

				if (int.TryParse(Console.ReadLine(), out addOnNumber) &&
					addOnNumber >= 1 && addOnNumber <= availableAddOns.Count)
				{
					orderItem.AddAddOn(availableAddOns[addOnNumber - 1]);
					Console.WriteLine("Add-on added successfully.");
				}
				else
				{
					Console.WriteLine("Invalid add-on number.");
				}

				Console.Write("Do you want to add another add-on? (y/n): ");
				string anotherAddOn = Console.ReadLine();

				if (anotherAddOn.ToLower() != "y")
				{
					addingAddOns = false;
				}
			}
		}

		order.AddItem(orderItem);
		Console.WriteLine($"{quantity} x {selectedProduct.Name} added successfully.");
	}

	static void UpdateItemQuantity(Order order)
	{
		if (order.Items.Count == 0)
		{
			Console.WriteLine("The order is empty.");
			return;
		}

		order.ViewCurrentOrder();

		Console.Write("Enter item number to update: ");
		int itemNumber;
		if (!int.TryParse(Console.ReadLine(), out itemNumber))
		{
			Console.WriteLine("Invalid item number.");
			return;
		}

		Console.Write("Enter new quantity: ");
		int newQuantity;
		if (!int.TryParse(Console.ReadLine(), out newQuantity))
		{
			Console.WriteLine("Invalid quantity.");
			return;
		}

		order.UpdateQuantity(itemNumber, newQuantity);
	}

	static void RemoveItemFromOrder(Order order)
	{
		if (order.Items.Count == 0)
		{
			Console.WriteLine("The order is empty.");
			return;
		}

		order.ViewCurrentOrder();

		Console.Write("Enter item number to remove: ");
		int itemNumber;
		if (!int.TryParse(Console.ReadLine(), out itemNumber))
		{
			Console.WriteLine("Invalid item number.");
			return;
		}

		order.RemoveItem(itemNumber);
	}

	static void ApplyDiscount(Order order)
	{
		Console.Write("Enter discount percent (0-100): ");
		double discountPercent;

		if (double.TryParse(Console.ReadLine(), out discountPercent) && discountPercent >= 0 && discountPercent <= 100)
		{
			order.DiscountPercent = discountPercent;
			Console.WriteLine($"Discount of {discountPercent:F2}% applied.");
		}
		else
		{
			Console.WriteLine("Invalid discount! Please enter a value between 0 and 100.");
		}
	}

	static void Checkout(Order order)
	{
		if (order.Items.Count == 0)
		{
			Console.WriteLine("Cannot checkout. The order is empty.");
			return;
		}

		bool paymentSuccess = false;
		double finalTotal = order.CalculateFinalTotal();

		while (!paymentSuccess)
		{
			Console.WriteLine();
			Console.WriteLine("Choose payment method:");
			Console.WriteLine("1. Cash");
			Console.WriteLine("2. Card");
			Console.Write("Enter your choice: ");
			string paymentChoice = Console.ReadLine();

			if (paymentChoice == "1")
			{
				Console.Write("Enter cash received: ");
				double cashReceived;

				if (double.TryParse(Console.ReadLine(), out cashReceived))
				{
					CashPayment cashPayment = new CashPayment(cashReceived);

					if (cashPayment.Pay(finalTotal))
					{
						order.PaymentMethod = cashPayment;
						paymentSuccess = true;
					}
					else
					{
						Console.WriteLine("Cash amount is not enough.");
					}
				}
				else
				{
					Console.WriteLine("Invalid cash amount.");
				}
			}
			else if (paymentChoice == "2")
			{
				Console.Write("Enter card number: ");
				string cardNumber = Console.ReadLine();

				CardPayment cardPayment = new CardPayment(cardNumber);

				if (cardPayment.Pay(finalTotal))
				{
					order.PaymentMethod = cardPayment;
					paymentSuccess = true;
				}
				else
				{
					Console.WriteLine("Invalid card number.");
				}
			}
			else
			{
				Console.WriteLine("Invalid payment option.");
			}
		}

		order.PrintInvoice();

		Console.Write("Do you want to save the invoice to a text file? (y/n): ");
		string saveChoice = Console.ReadLine();

		if (saveChoice.ToLower() == "y")
		{
			Console.Write("Enter file name (example: invoice.txt): ");
			string fileName = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(fileName))
			{
				fileName = "invoice.txt";
			}

			order.SaveInvoiceToFile(fileName);
			Console.WriteLine($"Invoice saved successfully to: {fileName}");
		}
	}
}