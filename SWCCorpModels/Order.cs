using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpModels
{
	public class Order
	{
		public DateTime OrderDate { get; set; }
		public int OrderNumber { get; set; }

		public string CustomerName { get; set; }
		public string State { get; set; }
		public decimal TaxRate { get; set; }
		public string ProductType { get; set; }
		public decimal Area { get; set; }
		public decimal CostPerSquareFoot { get; set; }
		public decimal LaborCostPerSquareFoot { get; set; }

		public decimal MaterialCost => Area * CostPerSquareFoot;
		public decimal LaborCost => Area * LaborCostPerSquareFoot;
		public decimal Tax => (MaterialCost + LaborCost) * (TaxRate / 100);
		public decimal Total => (MaterialCost + LaborCost + Tax);

		public override string ToString()
		{
			return $"{OrderNumber},{CustomerName},{State},{TaxRate},{ProductType},{Area},{CostPerSquareFoot},{LaborCostPerSquareFoot},{MaterialCost},{LaborCost},{Tax},{Total}";
		}
	}

}
