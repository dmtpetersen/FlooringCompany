using SWCCorpModels;
using SWCCorpModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpData
{
	public class MemoProductDataRepo : IProductRepo
	{
		public List<Products> AllProducts()
		{
			List<Products> allProducts = new List<Products> {
			new Products
			{
				ProductType = "Carpet",
				CostPerSquareFoot = 2.25m,
				LaborCostPerSquareFoot = 2.10m
			},

			new Products
			{
				ProductType = "Laminate",
				CostPerSquareFoot = 1.75m,
				LaborCostPerSquareFoot = 2.10m
			},

			new Products
			{
				ProductType = "Tile",
				CostPerSquareFoot = 3.50m,
				LaborCostPerSquareFoot = 4.15m
			},

			new Products
			{
				ProductType = "Wood",
				CostPerSquareFoot = 5.15m,
				LaborCostPerSquareFoot = 4.75m
			}
		};
			return allProducts;
		}
	}
}
