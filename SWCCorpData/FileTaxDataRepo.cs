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
	public class FileTaxDataRepo : ITaxRepo
	{
        static string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public string _filepath = Path.Combine(folder, "Repos", "Taxes.txt");

        public List<Taxes> AllStateTaxes()
		{
			List<Taxes> toReturn = new List<Taxes>();
			using (StreamReader sr = new StreamReader(_filepath))
			{
				sr.ReadLine();
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					Taxes toAdd = new Taxes();
					var toParse = line.Split(',');

					toAdd.StateAbbreviation = toParse[0];
					toAdd.StateName = toParse[1];
					toAdd.TaxRate = decimal.Parse(toParse[2]);

					toReturn.Add(toAdd);
				}
			}
			return toReturn;
		}
	}
}
