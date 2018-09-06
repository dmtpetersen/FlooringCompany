using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorpModels.Interfaces
{
	public interface IOrderRepo
	{

		Order LoadOrder(int orderNum, DateTime orderDate);
		bool Remove(Order order);
		Order Add(Order order);
		bool Edit(Order order);

	}
}
