using SWCCorp.BLL;
using SWCCorpModels;
using SWCCorpModels.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
	public class EditOrderWorkflow
	{
		public OrderManager manager = OrderManagerFactory.Create();

		public AddOrderWorkflow validator = new AddOrderWorkflow();

		public void Execute()
		{
			Console.Clear();
			DateTime date = DateCheck();
			int orderNo = NumCheck();
			LookupOrderResponse response = manager.LookupOrder(orderNo, date);
			if (response.Success)
			{
				Console.WriteLine("Order located:\n");
				ConsoleIO.DisplayOrders(response.Order);
				Console.Write("Is this the order you want to edit? Press 'y' for yes, or any other key to return to the main menu.\n");
				string cont = Console.ReadLine();
				if (cont == "y")
				{
					Order generatedOrder = GenerateAmendedOrder(response.Order);
					if (generatedOrder != null)
					{
						Console.Clear();
						ConsoleIO.DisplayOrders(generatedOrder);
						Console.Write("Is the above information correct? Press 'y' for yes, or any other key to return to the main menu.\n");
						string correct = Console.ReadLine();
						if (correct == "y")
						{
							EditOrderResponse editRespo = manager.EditOrder(generatedOrder);
							if (editRespo.Success)
							{
								Console.Clear();
								Console.WriteLine("Changes saved successfully.");
								Console.ReadKey();
							}
						}
					}
					else
					{
						Console.WriteLine();
						Console.ReadKey();
					}
				}
			}
			else
			{
				Console.WriteLine("That order doesn't appear to exist in our records.");
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
		}

		private Order GenerateAmendedOrder(Order currentOrder)
		{
			try
			{
				string name = AlterName(currentOrder.CustomerName);
				Taxes state = AlterState(currentOrder.State);
				Products product = AlterProduct(currentOrder.ProductType);
				decimal area = AlterArea(currentOrder.Area);

				Order toReturn = new Order()
				{
					OrderDate = currentOrder.OrderDate,
					OrderNumber = currentOrder.OrderNumber,

					CustomerName = name,
					State = state.StateAbbreviation,
					TaxRate = state.TaxRate,
					ProductType = product.ProductType,
					Area = area,
					CostPerSquareFoot = product.CostPerSquareFoot,
					LaborCostPerSquareFoot = product.LaborCostPerSquareFoot


				};

				return toReturn;
			}
			catch (Exception ex)
			{
				Order nullReturn = new Order();
				nullReturn = null;
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
				return nullReturn;
			}
		}

		private decimal AlterArea(decimal currentArea)
		{
			Console.Clear();
			decimal toReturn;
			Console.Write("Would you like to alter the square footage of your job? Press enter to retain the current data, or press any other key to change it.");
			string change = Console.ReadLine();
			if (change == "")
			{
				toReturn = currentArea;
			}
			else
			{
				decimal area = validator.ObtainValidArea();
				toReturn = area;
			}
			return toReturn;
		}

		private Products AlterProduct(string currentProduct)
		{
			Console.Clear();
			Products toReturn = new Products();
			Console.Write("Would you like to alter the product type of your job? Press enter to retain the current data, or press any other key to change it.");
			string change = Console.ReadLine();
			if (change == "")
			{
				toReturn = manager.ProcessProductInput(currentProduct);
			}
			else
			{
				Products product = validator.ObtainValidProduct();
				toReturn = product;
			}
			return toReturn;
		}

		private Taxes AlterState(string currentState)
		{
			Console.Clear();
			Taxes toReturn = new Taxes();
			Console.Write("Would you like to alter the State of your job? Press enter to retain the current data, or press any other key to change it.");
			string change = Console.ReadLine();
			if (change == "")
			{
				toReturn = manager.ProcessStateInput(currentState);
			}
			else
			{
				toReturn = validator.ObtainValidState();
			}
			return toReturn;
		}

		private string AlterName(string currentName)
		{
			Console.Clear();
			string toReturn;
			Console.Write("Would you like to alter the customer name on your job? Press enter to retain the current data, or press any other key to change it.");
			string change = Console.ReadLine();
			if (change == "")
			{
				toReturn = currentName;
			}
			else
			{
				toReturn = validator.ObtainValidName();
				
			}
			return toReturn;
		}

		public int NumCheck()
		{
			int num = -1;
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter the order number of the job.\n");
				string strInput = Console.ReadLine();
				if (int.TryParse(strInput, out num))
				{
					valid = true;
				}
				else
				{
					Console.WriteLine("That is not a number.");
				}
			}
			return num;
		}

		public DateTime DateCheck()
		{
			DateTime dt = new DateTime();
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter the date of the job. (yyyy/mm/dd)\n");
				string strInput = Console.ReadLine();
				if (DateTime.TryParse(strInput, out dt))
				{
					valid = true;
				}
				else
				{
					Console.WriteLine("That is not a date.");
				}
			}
			return dt;
		}
		
	}
}

