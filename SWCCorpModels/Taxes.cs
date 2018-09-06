using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpModels
{
	public class Taxes
	{
		public string StateAbbreviation { get; set; }
		public string StateName { get; set; }
		public decimal TaxRate { get; set; }
	}
}
