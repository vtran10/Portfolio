using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Additional Namespaces
using DAL;
using Entities;
using ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL
{
	public class RollingStockServices
	{
		#region Constructor Dependency Injection
		private readonly Context Context;
		public RollingStockServices(Context context)
		{
			if (context == null)
				throw new ArgumentNullException();
			Context = context;
		}
		#endregion

		#region Queries
		public List<RollingStockList> FindRollingStockByPartialMark(string partialName)
		{
			Console.WriteLine($"RollingStockServices: FindRollingStockByPartialMark(); partialName= {partialName}");
			var info =
				Context.RollingStocks
				.Where(x => x.ReportingMark.Contains(partialName))
				.Select(x => new RollingStockList
				{
					ReportingMark = x.ReportingMark,
					Owner = x.Owner,
					LightWeight = x.LightWeight,
					LoadLimit = x.LoadLimit,
					Capacity = x.Capacity,
					RailCarType = x.RailCarType.Name,
					YearBuilt = x.YearBuilt,
					InService = x.InService,
					Notes = x.Notes
				})
				.OrderBy(x => x.ReportingMark);
			return info.ToList();
		}   

		public List<RollingStockList> FindRollingStocksByType(int? id)
		{
			Console.WriteLine($"RollingStockServices: FindRollingStocksByType(); id= {id}");
			var info =
				Context.RollingStocks
				.Select(x => new RollingStockList
				{
					ReportingMark = x.ReportingMark,
					Owner = x.Owner,
					LightWeight = x.LightWeight,
					LoadLimit = x.LoadLimit,
					Capacity = x.Capacity,
					RailCarType = x.RailCarType.Name,
					YearBuilt = x.YearBuilt,
					InService = x.InService,
					Notes = x.Notes
				})
				.OrderBy(x => x.ReportingMark);
			return info.ToList();
		}

		public RollingStock Retrieve(string id)
		{
			var info = 
				Context.RollingStocks
				.Where(x => x.ReportingMark == id)
				.Select(x => new RollingStock
				{
					ReportingMark = x.ReportingMark,
					Owner = x.Owner,
					LightWeight = x.LightWeight,
					LoadLimit = x.LoadLimit,
					Capacity = x.Capacity,
					RailCarTypeId = x.RailCarTypeId,
					YearBuilt = x.YearBuilt,
					InService = x.InService,
					Notes = x.Notes
				}).FirstOrDefault();
			return info;
		}
		#endregion

		#region Commands: Add, Edit, Delete

		public int Add(RollingStockList item)
		{
			Console.WriteLine($"RollingStockServices: Add; ReportingMark= {item.ReportingMark}");

			//BLL Validation
			//for no ReportingMark duplicates
			var exists = 
				Context.RollingStocks.FirstOrDefault(x => 
				x.ReportingMark == item.ReportingMark);
			if (exists != null)
				throw new Exception("A railcar with the same Reporting Mark already exists");

			var newRollingStock = new RollingStock();
				newRollingStock.ReportingMark = item.ReportingMark;
				newRollingStock.Owner = item.Owner;
				newRollingStock.LightWeight = item.LightWeight;
				newRollingStock.LoadLimit = item.LoadLimit;
				newRollingStock.Capacity = item.Capacity;
				newRollingStock.RailCarType.Name = item.Name;
				newRollingStock.YearBuilt = item.YearBuilt;
				newRollingStock.InService = item.InService;
				newRollingStock.Notes = item.Notes;

			Context.RollingStocks.Add(newRollingStock);
			Context.SaveChanges();
			return newRollingStock.RailCarTypeId;
		}

		public void Edit(RollingStockList item)
		{
				Console.WriteLine($"RollingStockServices: Edit; ReportingMark= {item.ReportingMark}");

				//BLL Validation
				RollingStock existing = Context.RollingStocks.Find(item.ReportingMark);
					if (existing == null)
						throw new Exception("Product does not exist");
					
				existing.ReportingMark = item.ReportingMark;
				existing.Owner = item.Owner;
				existing.LightWeight = item.LightWeight;
				existing.LoadLimit = item.LoadLimit;
				existing.Capacity = item.Capacity;
				existing.RailCarType.Name = item.Name;
				existing.YearBuilt = item.YearBuilt;
				existing.InService = item.InService;
				existing.Notes = item.Notes;

				EntityEntry<RollingStock> updating = Context.Entry(existing);
				updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				Context.SaveChanges();
		}

		public void Delete(RollingStockList item)
		{
			Console.WriteLine($"RollingStockServices: Delete; ReportingMark= {item.ReportingMark}");

			//BLL Validation
			RollingStock existing = Context.RollingStocks.Find(item.ReportingMark);
				if (existing == null)
			 		throw new Exception("Product does not exist");

			//BLL Validation
			
			EntityEntry<RollingStock> removing = Context.Entry(existing);
			removing.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
			Context.SaveChanges();
		}
		#endregion
	}
}
