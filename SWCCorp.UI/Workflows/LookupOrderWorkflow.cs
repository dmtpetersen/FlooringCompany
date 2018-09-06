using SWCCorp.BLL;
using SWCCorpModels.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
	public class LookupOrderWorkflow
	{
		public void Execute()
		{
			bool checkDate = false;
			while (!checkDate)
			{
				OrderManager manager = OrderManagerFactory.Create();
				Console.Clear();
				Console.Write("Enter an order date (yyyy/mm/dd): ");
				DateTime orderDate;
				string strOrderDate = Console.ReadLine();

				if (DateTime.TryParse(strOrderDate, out orderDate))
				{
					bool checkNum = false;
					while (!checkNum)
					{
						Console.Write("Enter an order number: ");
						int orderNum;
						string strOrderNum = Console.ReadLine();
						if (int.TryParse(strOrderNum, out orderNum))
						{
							LookupOrderResponse response = manager.LookupOrder(orderNum, orderDate);
							if (response.Success)
							{
								checkDate = true;
								checkNum = true;
								Console.WriteLine("Order located:\n");
								ConsoleIO.DisplayOrders(response.Order);
							}
							else
							{
								Console.WriteLine("That order doesn't appear to exist in our records.");
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							checkDate = true;
							checkNum = true;
						}
						else
						{
							Console.WriteLine("Try that again.");
							Console.ReadLine();
						}
					}
				}
				else
				{
					Console.WriteLine("Try that again.");
					Console.ReadLine();
				}
			}
		}
	}
}