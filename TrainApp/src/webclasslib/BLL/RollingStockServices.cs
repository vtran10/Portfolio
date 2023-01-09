using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Additional Namespaces
using DAL;
using Entities;
using ViewModels;
//using Microsoft.EntityFrameworkCore;
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
		public List<RollingStockList> FindRollingStocksByPartialReportingMark(string partialReportingMark)
		{
			Console.WriteLine($"RollingStockServices: FindByPartialName(); partialName= {partialReportingMark}");
			var info =
				Context.RollingStocks
				.Where(x => x.ReportingMark.Contains(partialReportingMark))
				.Select(x => new RollingStockList
				{
					ReportingMark = x.ReportingMark,
					Owner = x.Owner,
					LightWeight = x.LightWeight,
					LoadLimit = x.LoadLimit,
					Capacity = x.Capacity,
					RailCarTypeName = x.RailCarType.Name,
					YearBuilt = x.YearBuilt,
					InService = x.InService,
					Notes = x.Notes
				})
				.OrderBy(x => x.ReportingMark);
			return info.ToList();
		}   

		public List<RollingStockList> FindRollingStocksByRailCarType(int? railCarTypeID)
		{
			Console.WriteLine($"RollingStockServices: FindRollingStocksByRailCarType; railCarTypeId= {railCarTypeID}");
			var info = 
				Context.RollingStocks
				.Where(x=>x.RailCarTypeID == railCarTypeID)
				.Select(x => new RollingStockList
				{
					ReportingMark = x.ReportingMark,
					Owner = x.Owner,
					LightWeight = x.LightWeight,
					LoadLimit = x.LoadLimit,
					Capacity = x.Capacity,
					RailCarTypeName = x.RailCarType.Name,
					YearBuilt = x.YearBuilt,
					InService = x.InService,
					Notes = x.Notes
				})
				.OrderBy(x => x.ReportingMark);
			return info.ToList();
		}
		#endregion

		#region READ - Retrieve, Edit, Add, Delete
		public RollingStockItem Retrieve(string reportingMark)
		{
			Console.WriteLine($"RollingStockServices: Retrieve; reportingMark= {reportingMark}");
			var info = 
				Context.RollingStocks
				.Where(x => x.ReportingMark == reportingMark)
				.Select(x => new RollingStockItem
				{
				ReportingMark = x.ReportingMark,
				Owner = x.Owner,
				LightWeight = x.LightWeight,
				LoadLimit = x.LoadLimit,
				Capacity = x.Capacity,
				RailCarTypeID = x.RailCarTypeID,
				YearBuilt = x.YearBuilt,
				InService = x.InService,
				Notes = x.Notes
				}).FirstOrDefault();
				Console.WriteLine($"End");
			return info;
		}
		
		public void Edit(RollingStockItem item)
		{
			Console.WriteLine($"RollingStockServices: Edit; ReportingMark= {item.ReportingMark}");

			//BLL Validation for non existing rolling stock
			RollingStock existing = Context.RollingStocks.Find(item.ReportingMark);
				if (existing == null)
				throw new Exception("Rolling Stock does not exist");

			existing.ReportingMark = item.ReportingMark;
			existing.Owner = item.Owner;
			existing.LightWeight = item.LightWeight;
			existing.LoadLimit = item.LoadLimit;
			existing.Capacity = item.Capacity;
			existing.RailCarTypeID = item.RailCarTypeID;
			existing.YearBuilt = item.YearBuilt;
			existing.InService = item.InService;
			existing.Notes = item.Notes;

			EntityEntry<RollingStock> updating = Context.Entry(existing);
			updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			Context.SaveChanges();
		}

		public string Add(RollingStockItem item)
		{
			Console.WriteLine($"RollingStockServices: Add; ReportingMark= {item.ReportingMark}");

			//BLL Validation
			//for no ReportingMark duplicates
			var exists = 
				Context.RollingStocks.FirstOrDefault(x => 
				x.ReportingMark == item.ReportingMark &&
				x.Owner == item.Owner &&
				x.LightWeight == item.LightWeight &&
				x.LoadLimit == item.LoadLimit &&
				x.Capacity == item.Capacity &&
				x.RailCarTypeID == item.RailCarTypeID);
			if (exists != null)
				throw new Exception("A railcar with the same ReportingMark, Owner and RailCarTypeID already exists");

			var newRollingStock = new RollingStock();
				newRollingStock.ReportingMark = item.ReportingMark;
				newRollingStock.Owner = item.Owner;
				newRollingStock.LightWeight = item.LightWeight;
				newRollingStock.LoadLimit = item.LoadLimit;
				newRollingStock.Capacity = item.Capacity;
				newRollingStock.RailCarTypeID = item.RailCarTypeID;
				newRollingStock.YearBuilt = item.YearBuilt;
				newRollingStock.InService = item.InService;
				newRollingStock.Notes = item.Notes;

			Context.RollingStocks.Add(newRollingStock);
			Context.SaveChanges();
			return newRollingStock.ReportingMark;
		}

		public void Delete(RollingStockItem item)
		{
		Console.WriteLine($"RollingStockServices: Delete; reportingMark= {item.ReportingMark}");

		//BLL Validation
		RollingStock existing = Context.RollingStocks.Find(item.ReportingMark);
			if (existing == null)
			throw new Exception("Rolling Stock does not exist");

		EntityEntry<RollingStock> removing = Context.Entry(existing);
		removing.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
		Context.SaveChanges();
		}

		#endregion
	}
}