using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorpModels;
using SWCCorpData;
using SWCCorp.BLL;

namespace SWCCorpTests
{
	[TestFixture]
	public class UnitTests
	{
        private static string _filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Repos", "Orders");
        private static string _editFilepath = Path.Combine(_filepath, "Orders_");
        private static string _orderFilePath = Path.Combine(_filepath, "Orders_21211212.txt");
		private static string _originalData = Path.Combine(_filepath, "TestOrdersSeed.txt");

		public Order testWriteOrder = new Order
		{
			OrderDate = new DateTime (2121, 12, 12),
			CustomerName = "Homer Simpson",
			State = "OH",
			TaxRate = 6.25m,
			ProductType = "Tile",
			Area = 100,
			CostPerSquareFoot = 3.50m,
			LaborCostPerSquareFoot = 4.15m
		};

		public Order testRemoveOrder = new Order
		{
			OrderNumber = 2,
			OrderDate = new DateTime(2121, 12, 12),
			CustomerName = "Brian Griffin",
			State = "OH",
            TaxRate = 6.25m,
			ProductType = "Carpet",
			Area = 100,
            CostPerSquareFoot = 2.25m,
            LaborCostPerSquareFoot = 2.10m
		};

		[SetUp]
		public void Setup()
		{
			if (File.Exists(_orderFilePath))
			{
				File.Delete(_orderFilePath);
			}

			File.Copy(_originalData, _orderFilePath);
		}

		public void TestEditOrder(Order order)
		{
			FileOrdersRepo repo = new FileOrdersRepo(_editFilepath);
            order.CustomerName = "Kyle Broflovski";
            repo.Edit(order);
		}

		[Test]
		public void CanReadDataFromFile()
		{
			FileOrdersRepo repo = new FileOrdersRepo(_filepath);

			List<Order> orders = repo.LoadAllOrders(_orderFilePath);

			Assert.AreEqual(3, orders.Count());

			Order check = orders[2];

			Assert.AreEqual(2, check.OrderNumber);
			Assert.AreEqual("Brian Griffin", check.CustomerName);
			Assert.AreEqual("OH", check.State);
			Assert.AreEqual(6.25, check.TaxRate);
			Assert.AreEqual("Carpet", check.ProductType);
			Assert.AreEqual(100, check.Area);
			Assert.AreEqual(2.25, check.CostPerSquareFoot);
			Assert.AreEqual(2.10, check.LaborCostPerSquareFoot);
			Assert.AreEqual(225.00, check.MaterialCost);
			Assert.AreEqual(210.00, check.LaborCost);
			Assert.AreEqual(27.187500, check.Tax);
			Assert.AreEqual(462.187500, check.Total);

		}
		[Test]
		public void CanWriteDataToFile()
		{
			FileOrdersRepo repo = new FileOrdersRepo(_editFilepath);
			repo.Add(testWriteOrder);
			List<Order> orders = repo.LoadAllOrders(_orderFilePath);
			Assert.AreEqual(4, orders.Count());
			Order check = orders[3];

			Assert.AreEqual(3, check.OrderNumber);
			Assert.AreEqual("Homer Simpson", check.CustomerName);
			Assert.AreEqual("OH", check.State);
			Assert.AreEqual("Tile", check.ProductType);
			Assert.AreEqual(100, check.Area);
		}
		[Test]
		public void CanRemoveDataFromFile()
		{
			FileOrdersRepo repo = new FileOrdersRepo(_editFilepath);
			repo.Remove(testRemoveOrder);
			List<Order> orders = repo.LoadAllOrders(_orderFilePath);
			Assert.AreEqual(2, orders.Count());
		}
		[Test]
		public void CanEditDataInFile()
		{
            var repo = new FileOrdersRepo(_filepath);
			TestEditOrder(testRemoveOrder);
			List<Order> orders = repo.LoadAllOrders(_orderFilePath);
			Assert.AreEqual(3, orders.Count());
			Assert.AreEqual("Kyle Broflovski", orders[2].CustomerName);
		}
	}
}
