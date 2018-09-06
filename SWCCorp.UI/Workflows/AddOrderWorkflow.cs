using SWCCorp.BLL;
using SWCCorpData;
using SWCCorpModels;
using SWCCorpModels.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
	public class AddOrderWorkflow
	{
		public OrderManager manager = OrderManagerFactory.Create();

		public void Execute()
		{
			Console.Clear();
			Order userOrder = GenerateOrder();
			ConsoleIO.DisplayOrders(userOrder);
			Console.Write("Is this okay? Press 'y' to submit the order, or press any other key to return to the main menu.\n");
			string goAhead = Console.ReadLine();
			if (goAhead == "y")
			{
				AddOrderResponse response = manager.AddOrder(userOrder);
				if (response.Success)
				{
					Console.WriteLine($"Order for {userOrder.CustomerName} successfully scheduled for {userOrder.OrderDate}. Thank you for your patronage.");
					Console.WriteLine($"Full details:\n");
					ConsoleIO.DisplayOrders(userOrder);
					Console.ReadLine();
				} else
				{
					Console.WriteLine(response.Message);
					Console.ReadLine();
				}

			}
		}

		public Order GenerateOrder()
		{
			DateTime date = ObtainValidDateTime();
			string name = ObtainValidName();
			Taxes state = ObtainValidState();
			Products product = ObtainValidProduct();
			decimal area = ObtainValidArea();
			Order userOrder = new Order()
			{
				OrderDate = date,

				CustomerName = name,
				State = state.StateAbbreviation,
				TaxRate = state.TaxRate,
				ProductType = product.ProductType,
				Area = area,
				CostPerSquareFoot = product.CostPerSquareFoot,
				LaborCostPerSquareFoot = product.LaborCostPerSquareFoot

			};
			return userOrder;
		}
		public decimal ObtainValidArea()
		{
			Console.Clear();
			decimal area = 0;
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter the square footage of the job. Minimum order is 100ft\xB2\n");
				string strInput = Console.ReadLine();
				if (decimal.TryParse(strInput, out area))
				{
					if (area >= 100)
					{
						valid = true;
					}
					else
					{
						Console.WriteLine("That order is not more than 100ft\xB2");
						Console.ReadLine();
					}
				}
				else
				{
					Console.WriteLine("That is not a valid number.");
					Console.ReadLine();
				}
			}
			return area;
		}

		public Products ObtainValidProduct()
		{
			Console.Clear();
			Products product = new Products();
			bool valid = false;
			while (!valid)
			{
				List<Products> display = manager.DisplayProductSelection();

				foreach (var item in display)
				{
					Console.WriteLine($"{item.ProductType} | {item.CostPerSquareFoot} | {item.LaborCostPerSquareFoot}");
				}
				Console.Write("Please enter the floor type you would like.\n");
				string strInput = Console.ReadLine();
				if (manager.ProcessProductInput(strInput) != null)
				{
					valid = true;
					product = manager.ProcessProductInput(strInput);
				}
				else
				{
					Console.WriteLine("That product does not exist. Try again.");
				}
			}
			return product;
		}

		public Taxes ObtainValidState()
		{
			Console.Clear();
			Taxes state = new Taxes();
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter the state in which the job is located by entering the 2-character state abbreviation.\n");
				string strInput = Console.ReadLine();
				if (manager.ProcessStateInput(strInput) != null)
				{
					valid = true;
					state = manager.ProcessStateInput(strInput);
				}
				else
				{
					Console.WriteLine("Invalid state entry. Try again.");
				}

			}
			return state;
		}

		public string ObtainValidName()
		{
			Console.Clear();
			string name = null;
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter your name. Permitted characters: letters, numbers, spaces, periods and commas.\n");
				name = Console.ReadLine();
				Regex rg = new Regex(@"^[a-zA-Z0-9\s,.]*$");
				if (rg.IsMatch(name) && name.Length > 0)
				{
					valid = true;
				}
				else
				{
					Console.WriteLine("You have entered one or more invalid characters. Try again.");
				}

			}
			return name;
		}

		public DateTime ObtainValidDateTime()
		{
			Console.Clear();
			DateTime dt = new DateTime();
			bool valid = false;
			while (!valid)
			{
				Console.Write("Please enter the date of the job. (yyyy/mm/dd)\n");
				string strInput = Console.ReadLine();
				if (DateTime.TryParse(strInput, out dt))
				{
					if (dt > DateTime.Today)
					{
						valid = true;
					}
					else
					{
						Console.WriteLine("That date has already passed. Try again.");
					}
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
