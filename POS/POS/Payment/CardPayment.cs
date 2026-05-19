using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Payment
{
	// Task:
	// Card payment should:
	// - store the card number
	// - mask the card number
	// - accept cards with at least 4 digits

	class CardPayment : IPaymentMethod
	{
		public string CardNumber { get; set; }
		public string MaskedCardNumber { get; set; }

		public CardPayment(string cardNumber)
		{
			// TODO
			if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 4)
				throw new ArgumentException("Invalid card number. It cannot be empty and must be at least 4 digits.");
			this.CardNumber = cardNumber;            
            this.MaskedCardNumber = MaskCardNumber(cardNumber);
		}

		public bool Pay(double amount)
		{
			// TODO:            			
			if (amount <= 0)				
				throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be greater than zero.");            

            Console.WriteLine($"Processing card payment of {amount:F3} KD...");
            return true;						
		}

		public string GetPaymentDetails()
		{
            // TODO
            return $"Payment Method: Card (No: {MaskedCardNumber})";
        }

        // MaskCardNumber is private because it's an internal helper method that should not be exposed outside of the CardPayment class. It shows only the last four digits for security reasons.
        private string MaskCardNumber(string cardNumber)
		{
            // TODO:                        
			// Convert to masked string directly (Constructor guarantees the validity of cardNumber)
            string lastFourDigits = cardNumber.Substring(cardNumber.Length - 4);

            return $"**** **** **** {lastFourDigits}";
		}

		public void PrintReceipt()
		{
            // TODO
            Console.WriteLine("---------- CARD RECEIPT ----------");
            Console.WriteLine($"Card Number: {MaskedCardNumber}");
            Console.WriteLine("Status: TRANSACTION SUCCESSFUL");
            Console.WriteLine("----------------------------------");
        }
	}
}
