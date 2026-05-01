using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Payment
{
	interface IPaymentMethod
	{
		bool Pay(double amount);
		string GetPaymentDetails();

		void PrintReceipt();
	}
}
