using SWCCorpModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorpModels;
using System.IO;

namespace SWCCorpData
{
	public class FileOrdersRepo : IOrderRepo
	{
        public string _filepath;

        public FileOrdersRepo(string filepath)
		{
			_filepath = filepath;
		}

		public Order LoadOrder(int orderNum, DateTime orderDate)
		{

			Order toReturn = new Order();
			try
			{
                string specifiedDate = _filepath + orderDate.ToString("yyyyMMdd") + ".txt";

				using (StreamReader sr = new StreamReader(specifiedDate))
				{
					sr.ReadLine();
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						Order toCheck = new Order();
						var toParse = line.Split(',');

						if (orderNum.ToString() == toParse[0])
						{
							toReturn.OrderDate = orderDate;
							toReturn.OrderNumber = int.Parse(toParse[0]);
							toReturn.CustomerName = toParse[1];
							toReturn.State = toParse[2];
							toReturn.TaxRate = decimal.Parse(toParse[3]);
							toReturn.ProductType = toParse[4];
							toReturn.Area = decimal.Parse(toParse[5]);
							toReturn.CostPerSquareFoot = decimal.Parse(toParse[6]);
							toReturn.LaborCostPerSquareFoot = decimal.Parse(toParse[7]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
				toReturn = null;
			}

			return toReturn;

		}

		public bool Remove(Order order)
		{
            string specifiedDate = _filepath + order.OrderDate.ToString("yyyyMMdd") + ".txt";
            bool success = false;
			try
			{
				List<Order> toWrite = LoadAllOrders(specifiedDate);

				File.Delete(specifiedDate);

				using (StreamWriter sw = new StreamWriter(specifiedDate))
				{
					sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
					foreach (Order item in toWrite)
					{
						if (item.OrderNumber != order.OrderNumber)
						{
							sw.WriteLine(CreateOrderString(item));
						}
					}
				}
				if (DoesContain(order, specifiedDate) == false)
				{
					success = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
			return success;
		}

		public Order Add(Order order)
		{
			try
			{
                string specifiedDate = _filepath + order.OrderDate.ToString("yyyyMMdd") + ".txt";
                if (File.Exists(specifiedDate))
				{
					WriteNewToFile(order, specifiedDate);
				}
				else
				{
					using (StreamWriter sw = new StreamWriter(specifiedDate))
					{
						sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
						sw.WriteLine(CreateOrderString(order));

					}
				}
			}
			catch (Exception ex)
			{
				order = null;
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
			return order;
		}

		public bool Edit(Order order)
		{
            string specifiedDate = _filepath + order.OrderDate.ToString("yyyyMMdd") + ".txt";
            Order exists = LoadOrder(order.OrderNumber, order.OrderDate);
			bool success = false;
			try
			{
				if (exists != null)
				{
					WriteEditToFile(order, specifiedDate);
				}
				else
				{
					return success;
				}

				success = DoesContain(order, specifiedDate);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
			return success;
		}

		public void WriteNewToFile(Order order, string specifiedDate)
		{

			order.OrderNumber = GenerateOrderNumber(order, specifiedDate);

			List<Order> addTo = LoadAllOrders(specifiedDate);
			addTo.Add(order);
			try
			{
				using (StreamWriter sw = new StreamWriter(specifiedDate))
				{
					sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

					foreach (Order item in addTo)
					{
						if (item.OrderNumber != order.OrderNumber)
						{
							sw.WriteLine(item);
						}
						else
						{
							string line = CreateOrderString(order);
							sw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
		}

		public void WriteEditToFile(Order order, string specifiedDate)
		{
			try
			{
				List<Order> writeTo = LoadAllOrders(specifiedDate);

				using (StreamWriter sw = new StreamWriter(specifiedDate))
				{
					sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

					foreach (Order item in writeTo)
					{
						if (item.OrderNumber != order.OrderNumber)
						{
							sw.WriteLine(item);
						}
						else
						{
							string line = CreateOrderString(order);
							sw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
		}
		public string CreateOrderString(Order order)
		{
			return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber, order.CustomerName,
				order.State, order.TaxRate.ToString(), order.ProductType,
				order.Area.ToString(), order.CostPerSquareFoot.ToString(),
				order.LaborCostPerSquareFoot.ToString(), order.MaterialCost.ToString(),
				order.LaborCost.ToString(), order.Tax.ToString(), order.Total.ToString());
		}
		public List<Order> LoadAllOrders(string filepath)
		{
			List<Order> toWrite = new List<Order>();
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					sr.ReadLine();
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						Order toAdd = new Order();
						var toParse = line.Split(',');

						toAdd.OrderNumber = int.Parse(toParse[0]);
						toAdd.CustomerName = toParse[1];
						toAdd.State = toParse[2];
						toAdd.TaxRate = decimal.Parse(toParse[3]);
						toAdd.ProductType = toParse[4];
						toAdd.Area = decimal.Parse(toParse[5]);
						toAdd.CostPerSquareFoot = decimal.Parse(toParse[6]);
						toAdd.LaborCostPerSquareFoot = decimal.Parse(toParse[7]);

						toWrite.Add(toAdd);
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"A {ex.GetType()} exception was caught with message: {ex.Message}");
				Console.Write("Press any key to continue...");
			}
			return toWrite;
		}

		public int GenerateOrderNumber(Order order, string filepath)
		{
			int toReturn = -1;
			List<Order> findHighest = LoadAllOrders(filepath);
			order.OrderNumber = findHighest.Max(o => o.OrderNumber) + 1;
			toReturn = order.OrderNumber;
			return toReturn;
		}

		public bool DoesContain(Order order, string filepath)
		{
			bool success = false;

			List<Order> toWrite = LoadAllOrders(filepath);

			foreach (var check in toWrite)
			{
				if (check.OrderNumber == order.OrderNumber)
				{
					success = true;
				}
			}

			return success;
		}
	}
}
