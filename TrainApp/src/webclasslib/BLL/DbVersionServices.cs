using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional Namespaces
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL 
{
	public class DbVersionServices 
	{
		#region Constructor Dependency Injection
		private readonly Context Context;
		public  DbVersionServices(Context context) 
		{
				if (context == null)
					throw new ArgumentNullException();
				Context = context;
		}
		#endregion

		#region Queries
		public DbVersion GetDbVersion() 
		{
				Console.WriteLine($"DbServices: GetDbVersion;");
				var result = Context.DbVersions.ToList();
				return result.First();
		}
		#endregion 
	}
}