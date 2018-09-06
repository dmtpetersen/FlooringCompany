using SWCCorpModels;
using SWCCorpModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorpModels.Reponses;

namespace SWCCorp.BLL
{
	public class OrderManager
	{
		private IOrderRepo _orderRepo;
		private IProductRepo _productRepo;
		private ITaxRepo _taxRepo;

		public OrderManager(IOrderRepo orderRepo, IProductRepo productRepo, ITaxRepo taxRepo)
		{
			_orderRepo = orderRepo;
			_productRepo = productRepo;
			_taxRepo = taxRepo;
		}

		public LookupOrderResponse LookupOrder(int orderNum, DateTime orderDate)
		{
			LookupOrderResponse response = new LookupOrderResponse();

			response.Order = _orderRepo.LoadOrder(orderNum, orderDate);
			if (response.Order == null || response.Order.OrderDate != orderDate)
			{
				response.Success = false;
				response.Message = $"{orderNum} is not a valid order number.";
			}
			else
			{
				response.Success = true;
			}
			return response;
		}

		public AddOrderResponse AddOrder(Order userOrder)
		{
			AddOrderResponse response = new AddOrderResponse();
			response.Order = _orderRepo.Add(userOrder);
			if(response.Order != null)
			{
				response.Success = true;
			}
			else
			{
				response.Success = false;
				response.Message = $"There was a problem with saving your order. Please contact IT.";
			}
			return response;
		}

		public RemoveOrderResponse RemoveOrder(Order order)
		{
			RemoveOrderResponse response = new RemoveOrderResponse();
			response.Order = LookupOrder(order.OrderNumber, order.OrderDate).Order;
			if (response.Order != null)
			{
				if (_orderRepo.Remove(response.Order) == true)
				{
					response.Success = true;
					
				}
			}
			else
			{
				response.Message = $"Something went wrong";
			}

			return response;
		}

		public EditOrderResponse EditOrder(Order order)
		{
			EditOrderResponse response = new EditOrderResponse();
			bool complete = _orderRepo.Edit(order);
			if (complete == true)
			{
				response.Success = true;
			}
			else
			{
				response.Message = $"Something went wrong";
			}

			return response;
		}

		public Taxes ProcessStateInput(string userInput)
		{
			Taxes response = _taxRepo.AllStateTaxes().SingleOrDefault(t => t.StateAbbreviation == userInput);
			
			return response;
		}

		public List<Products> DisplayProductSelection()
		{
			List<Products> response = new List<Products>();

			response = _productRepo.AllProducts();

			return response;
		}

		public Products ProcessProductInput(string userInput)
		{
			Products response = _productRepo.AllProducts().SingleOrDefault(p => p.ProductType == userInput);

			return response;
		}
	}
}
