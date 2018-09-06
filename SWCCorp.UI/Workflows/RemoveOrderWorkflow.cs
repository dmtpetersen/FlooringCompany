using SWCCorp.BLL;
using SWCCorpModels.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
	public class RemoveOrderWorkflow
	{
		public void Execute()
		{
			bool checkDate = false;
			while (!checkDate)
			{
				OrderManager manager = OrderManagerFactory.Create();
				Console.Clear();
				Console.WriteLine("Look up an order");
				Console.WriteLine("---------------------");
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
								Console.WriteLine("Order located:\n");
								ConsoleIO.DisplayOrders(response.Order);
								Console.Write("Are you sure you want to remove this order? Press 'y' to delete it, or press any other key to return to the main menu.\n");
								string purge = Console.ReadLine();
								switch (purge)
								{
									case "y":
										RemoveOrderResponse remRespo = manager.RemoveOrder(response.Order);
										if (remRespo.Success)
										{
											checkDate = true;
											checkNum = true;
											Console.WriteLine("Order successfully removed.");
											Console.ReadLine();
										}
										else
										{
											Console.WriteLine("Removal failed.");
											Console.ReadLine();
										}
										break;
									default:
										checkDate = true;
										checkNum = true;
										break;
								}
							}
							else
							{
								checkNum = true;
								checkDate = true;
								Console.WriteLine(response.Message);
								Console.Write("Press any key to continue...");
								Console.ReadKey();

							}
						}
						else
						{
							Console.WriteLine("Try that again");
						}
					}
				}
				else
				{
					Console.WriteLine("Try that again");
				}
			}
		}
	}
}
