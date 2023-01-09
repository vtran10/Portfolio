using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional Namespaces
using DAL;
using ViewModels;

namespace BLL
{
	public class RailCarTypeServices
	{
		#region Constructor Dependency Injection
		private readonly Context Context;
		public RailCarTypeServices(Context context)
		{
			if (context == null)
				throw new ArgumentNullException();
			Context = context;
		}
		#endregion

		#region Queries
		public List<SelectionList> ListRailCarType()
		{
			List<SelectionList> info = 
				Context.RailCarTypes
				.Select(x => new SelectionList
				{
					ValueField = x.RailCarTypeID,
					DisplayField = x.Name
				})
				.OrderBy(x => x.DisplayField)
				.ToList();
			return info;
		}		
		#endregion

	}
}