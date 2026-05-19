using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Payment
{
	// Task:
	// Cash payment should:
	// - store received amount
	// - calculate change
	// - validate that cash is enough

	class CashPayment : IPaymentMethod
	{
		public double CashReceived { get; set; }
		public double Change { get; set; }

		public CashPayment(double cashReceived)
		{
			// TODO
			if (cashReceived <= 0)
				throw new ArgumentOutOfRangeException("Cash received must be greater than zero.");

			this.CashReceived = cashReceived;
		}

		public bool Pay(double amount)
		{
            // TODO:
			if(amount <= 0)
			{
				throw new ArgumentOutOfRangeException("Payment amount must be greater than zero.");
			}
            
			if (CashReceived < amount)
            {
				throw new InvalidOperationException($"Insufficient cash. You need {amount - CashReceived:F3} KD more.");
            }

            Change = CashReceived - amount;
            Console.WriteLine($"Processing cash payment of {amount:F3} KD...");
            return true;
        }

		public string GetPaymentDetails()
		{
            // TODO
            return $"Payment Method: Cash (Received: {CashReceived:F3} KD, Change: {Change:F3} KD)";
        }

		public void PrintReceipt()
		{
            // TODO
            Console.WriteLine("---------- CASH RECEIPT ----------");
            Console.WriteLine($"Cash Received: {CashReceived:F3} KD");
            Console.WriteLine($"Change:        {Change:F3} KD");
            Console.WriteLine("Status: PAID");
            Console.WriteLine("----------------------------------");
        }
	}
}
