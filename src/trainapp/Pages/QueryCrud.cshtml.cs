using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//Additional namespaces
using BLL;
using ViewModels;

namespace MyApp.Namespace
{
	public class QueryCrudModel : PageModel
	{
		private readonly RailCarTypeServices RailCarTypeServices;
		private readonly RollingStockServices RollingStockServices;
		public QueryCrudModel(RailCarTypeServices railCartypeservices, 
		RollingStockServices rollingstockservices)
		{
			RailCarTypeServices = railCartypeservices;
			RollingStockServices = rollingstockservices;
		}
		public string SuccessMessage { get; set; }
		public string ErrorMessage { get; set; }
		public List<Exception> Errors {get; set;} = new();
		[BindProperty]
		public string ButtonPressed {get; set;}
		[BindProperty]
		public string FilterType {get;set;}
		[BindProperty]
		public string PartialReportingMark {get;set;}
		[BindProperty]
		public int? SelectedRailCarTypeId {get;set;}
		[BindProperty]
		public List<RollingStockList> SearchedRollingStock { get; set; }
		[BindProperty]
		public RollingStockList RollingStock {get;set;} = new();
		[BindProperty]
		public string InService {get;set;} 

		[BindProperty]
		public List<SelectionList> SelectListRailCarType {get;set;}
		
		public IActionResult OnGet()
		{
			try
			{
				Console.WriteLine("QueryModel: OnGet");
				PopulateSelectLists();
			}
			catch (Exception ex)
			{
				ErrorMessage = GetInnerException(ex);
			}
			return Page();
		}

		public IActionResult OnPost()
		{
			try
			{
				Console.WriteLine("QueryModel: OnPost");

				if(ButtonPressed == "SearchByPartialReportingMark")
				{
					SuccessMessage = "Product Search by Partial ReportingMark";
					FilterType = "PartialString";
				}
				else if(ButtonPressed == "SearchByRailCarType")
				{
					SuccessMessage = "Product Search by RailCarType Dropdown";
					FilterType = "DropDown";
				}
				else if(ButtonPressed == "Add")
				{
					if(InService == "on")
						RollingStock.InService = true;
					else
						RollingStock.InService = false;
					FormValidation();
					//RollingStock.ReportingMark = RollingStockServices.Add(RollingStock);
					SuccessMessage = "Add Successful r u sure vic";
				}
				else if(ButtonPressed == "Update")
				{
					if(InService == "on")
						RollingStock.InService = true;
					else
						RollingStock.InService = false;
					FormValidation();
					RollingStockServices.Edit(RollingStock);
					SuccessMessage = "Update Successful";
				}
				else if(ButtonPressed == "Delete")
				{
					RollingStockServices.Delete(RollingStock);
					RollingStock = new RollingStockList();
					SuccessMessage = "Delete Successful";
				}
				else if(ButtonPressed == "Clear")
				{
					RollingStock = new RollingStockList();
					SuccessMessage = "Clear Successful";
				}
				// else if(RollingStock.ReportingMark != 0)
				// {
				// 		RollingStock = RollingStockServices.Retrieve(RollingStock.ProductId);
				// 		SuccessMessage = "Product Retrieve Successful";
				// }
				else 
				{
					ErrorMessage = "Danger: At the end of our ropes!";
				}
			}
			catch (AggregateException ex)
			{
				ErrorMessage = ex.Message;
			}
			catch (Exception ex)
			{
				ErrorMessage = GetInnerException(ex);
			}
			PopulateSelectLists();
			GetProducts(FilterType);
			return Page();
			
		}

		private void PopulateSelectLists()
		{
			try
			{
				Console.WriteLine("Querymodel: PopulateSelectLists");
				SelectListRailCarType = RailCarTypeServices.ListRailCarType();
			}
			catch (Exception ex)
			{ 
				ErrorMessage = GetInnerException(ex);
			}
		}

		private void GetProducts(string filterType)
		{
			try
			{
				Console.WriteLine($"QueryCrudModel: GetProducts:filtertype= {filterType}");
				if(filterType == "PartialString")
					SearchedRollingStock = RollingStockServices.FindRollingStockByPartialMark(PartialReportingMark);
				else if(filterType == "DropDown")
					SearchedRollingStock = RollingStockServices.FindRollingStocksByType(SelectedRailCarTypeId);
			}
			catch (Exception ex)
			{ 
				ErrorMessage = GetInnerException(ex);
			}
		}

		public void FormValidation()
		{
			if(string.IsNullOrEmpty(RollingStock.ReportingMark))
				Errors.Add(new Exception("ReportingMark"));
			if(string.IsNullOrEmpty(RollingStock.Owner))
				Errors.Add(new Exception("Owner"));
							
			if(RollingStock.LightWeight < 0)
				Errors.Add(new Exception("LightWeight < 0"));
			if(RollingStock.LoadLimit < 0)
				Errors.Add(new Exception("LoadLimit < 0"));
			if(RollingStock.Capacity < 0)
				Errors.Add(new Exception("Capacity < 0"));
			if(RollingStock.RailCarTypeID < 0)
				Errors.Add(new Exception("RailCarTypeID < 0"));
			if(RollingStock.YearBuilt < 0)
				Errors.Add(new Exception("YearBuilt < 0"));

			if(RollingStock.InService == false)
				Errors.Add(new Exception("InService"));

			if(string.IsNullOrEmpty(RollingStock.Notes))
				Errors.Add(new Exception("Notes"));

			if (Errors.Count() > 0)
					throw new AggregateException("Invalid Data: ", Errors);

			if(RollingStock.ReportingMark.Length > 24)
				Errors.Add(new Exception("ReportingMark > 40"));
			
			if (Errors.Count() > 0)
					throw new AggregateException("Invalid Data: ", Errors);
		}

		public string GetInnerException(Exception e)
		{
			Exception rootCause = e;
			while (rootCause.InnerException != null)
					rootCause = rootCause.InnerException;
			return rootCause.Message;
		}
	}
}

