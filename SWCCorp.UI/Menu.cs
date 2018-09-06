﻿using SWCCorp.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
	public static class Menu
	{
		public static void Start()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Flooring Program");
				Console.WriteLine("-----------------------");
				Console.WriteLine("1. Display Orders");
				Console.WriteLine("2. Add an Order");
				Console.WriteLine("3. Edit an Order");
				Console.WriteLine("4. Remove an Order");
				Console.WriteLine("5. Quit");
				Console.Write("\nEnter selection: ");
				string userInput = Console.ReadLine();

				switch (userInput)
				{
					case "1":
						LookupOrderWorkflow lookupWorkflow = new LookupOrderWorkflow();
						lookupWorkflow.Execute();
						break;
					case "2":
						AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
						addOrderWorkflow.Execute();
						break;
					case "3":
						EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
						editOrderWorkflow.Execute();
						break;
					case "4":
						RemoveOrderWorkflow removeWorkflow = new RemoveOrderWorkflow();
						removeWorkflow.Execute();
						break;
					default:
						break;

				}
			}
		}
	}
}
