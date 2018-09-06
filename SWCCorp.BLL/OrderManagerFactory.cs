using SWCCorpData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
	public static class OrderManagerFactory
	{
        static string relativePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        static string absolutePath = @"\Repos\Orders\Orders_";

        public static string _filepath = Path.GetFullPath(relativePath + absolutePath);

        public static OrderManager Create()
		{
			string mode = ConfigurationManager.AppSettings["Mode"].ToString();

			switch (mode)
			{
				case "Prod":
					return new OrderManager(new FileOrdersRepo(_filepath), new FileProductDataRepo(), new FileTaxDataRepo());
				case "Test":
					return new OrderManager(new MemoOrdersRepo(), new MemoProductDataRepo(), new MemoTaxDataRepo());
				default:
					throw new Exception("No such value");
			}

		}
	}
}
