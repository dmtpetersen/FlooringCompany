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
	public class FileProductDataRepo : IProductRepo
	{
        //private const string _filePath = @"C:\Users\apprentice\Desktop\Repos\david-petersen-individual-work\FlooringMasteryProject\SWCCorp.BLL\Repos\Products.txt";

        static string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public string _filepath = Path.Combine(folder, "Repos", "Products.txt");

		public List<Products> AllProducts()
		{
			List<Products> toReturn = new List<Products>();
			using (StreamReader sr = new StreamReader(_filepath))
			{
				sr.ReadLine();
				string line;
				while((line = sr.ReadLine()) != null)
				{
					Products toAdd = new Products();
					var toParse = line.Split(',');

					toAdd.ProductType = toParse[0];
					toAdd.CostPerSquareFoot = decimal.Parse(toParse[1]);
					toAdd.LaborCostPerSquareFoot = decimal.Parse(toParse[2]);

					toReturn.Add(toAdd);
				}
			}
			return toReturn;
		}

	}
}
