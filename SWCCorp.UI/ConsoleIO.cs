using SWCCorpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
	public static class ConsoleIO
	{
		public static void DisplayOrders(Order order)
		{
			Console.WriteLine("------------------------------");
			Console.WriteLine("Order");
			Console.WriteLine($"Order #{order.OrderNumber} | {order.OrderDate}");
			Console.WriteLine($"{order.CustomerName}");
			Console.WriteLine($"{order.State}");
			Console.WriteLine($"Product: {order.ProductType}");
			Console.WriteLine($"Materials: {order.MaterialCost:c}");
			Console.WriteLine($"Labor: {order.LaborCost:c}");
			Console.WriteLine($"Tax: {order.Tax:c}");
			Console.WriteLine($"Total: {order.Total:c}");
			Console.WriteLine("------------------------------");
		}
	}
}
