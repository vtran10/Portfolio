using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional Namespaces
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using BLL;

namespace webclasslib
{
	public static class BackendExtensions
	{
		public static void AddBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
		{	
			services.AddDbContext<Context>(options);

			services.AddTransient<DbVersionServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetRequiredService<Context>();
				return new DbVersionServices(context);
			});

			services.AddTransient<RailCarTypeServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetRequiredService<Context>();
				return new RailCarTypeServices(context);
			});

			services.AddTransient<RollingStockServices >((serviceProvider) =>
			{
				var context = serviceProvider.GetRequiredService<Context>();
				return new RollingStockServices (context);
			});
		}
	}
}