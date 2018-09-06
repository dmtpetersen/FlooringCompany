using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpModels.Interfaces
{
	public interface IProductRepo
	{
		List<Products> AllProducts();
	}
}
