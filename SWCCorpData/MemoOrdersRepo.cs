using SWCCorpModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorpModels;

namespace SWCCorpData
{
	public class MemoOrdersRepo : IOrderRepo
	{
		static List<Order> _allOrders = new List<Order>
		{

		new Order
		{
			OrderDate = new DateTime(2016, 09, 09),
			OrderNumber = 1,

			CustomerName = "This Fella",
			State = "PA",
			TaxRate = 6.75m,
			ProductType = "Laminate",
			Area = 50.0m,
			CostPerSquareFoot = 1.75m,
			LaborCostPerSquareFoot = 2.10m,
		},

		new Order
		{
			OrderDate = new DateTime(2016, 09, 09),
			OrderNumber = 2,

			CustomerName = "That Fella",
			State = "OH",
			TaxRate = 6.25m,
			ProductType = "Wood",
			Area = 75.0m,
			CostPerSquareFoot = 5.15m,
			LaborCostPerSquareFoot = 4.75m,
		},

		new Order
		{
			OrderDate = new DateTime(2016, 09, 09),
			OrderNumber = 3,

			CustomerName = "This Lady",
			State = "PA",
			TaxRate = 6.75m,
			ProductType = "Tile",
			Area = 150.0m,
			CostPerSquareFoot = 3.50m,
			LaborCostPerSquareFoot = 4.15m,
		},

			new Order
		{
			OrderDate = new DateTime(2016, 09, 09),
			OrderNumber = 4,

			CustomerName = "That Lady",
			State = "IN",
			TaxRate = 6.00m,
			ProductType = "Laminate",
			Area = 90.0m,
			CostPerSquareFoot = 1.75m,
			LaborCostPerSquareFoot = 2.10m,
		}
	};

		public Order Add(Order order)
		{
			order.OrderNumber = -1;
			order.OrderNumber = _allOrders.Max(o => o.OrderNumber) + 1;
			_allOrders = SaveOrder(order);
			return order;
		}

		public bool Edit(Order order)
		{
			bool edit = Remove(LoadOrder(order.OrderNumber, order.OrderDate));
			if (edit == true)
			{
				_allOrders = SaveOrder(order);
			}
			return edit;

		}

		public Order LoadOrder(int orderNum, DateTime orderDate)
		{

			Order toReturn = _allOrders.SingleOrDefault(o => o.OrderDate == orderDate && o.OrderNumber == orderNum);
			return toReturn;
		}

		public bool Remove(Order order)
		{
			Order toRemove = LoadOrder(order.OrderNumber, order.OrderDate);
			bool success = false;
			if (toRemove != null)
			{
				_allOrders.Remove(toRemove);
				success = true;
			}
			return success;


		}

		public List<Order> SaveOrder(Order order)
		{
			List<Order> toReturn = new List<Order>();
			toReturn = _allOrders;
			toReturn.Add(order);
			return toReturn;
		}
	}
}
