using SWCCorpModels;
using SWCCorpModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpData
{
	public class MemoTaxDataRepo : ITaxRepo
	{
		public List<Taxes> AllStateTaxes()
		{
			List<Taxes> stateTaxes = new List<Taxes>
			{
			new Taxes
			{
				StateName = "Ohio",
				StateAbbreviation = "OH",
				TaxRate = 6.25m
			},

			new Taxes
			{
				StateName = "Pennsylvania",
				StateAbbreviation = "PA",
				TaxRate = 6.75m
			},

			new Taxes
			{
				StateName = "Michigan",
				StateAbbreviation = "MI",
				TaxRate = 5.75m
			},

			new Taxes
			{
				StateName = "Indiana",
				StateAbbreviation = "IN",
				TaxRate = 6.00m
			}
			};
			return stateTaxes;
		}


	}
}
