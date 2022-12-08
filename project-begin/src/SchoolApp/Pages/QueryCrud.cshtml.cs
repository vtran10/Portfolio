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
        private readonly ProgramServices ProgramServices;
		private readonly SchoolServices SchoolServices;
		public QueryCrudModel(ProgramServices programServices, 
		SchoolServices schoolServices)
		{
			ProgramServices = programServices;
			SchoolServices = schoolServices;
		}
		public string SuccessMessage { get; set; }
		public string ErrorMessage { get; set; }
		public List<Exception> Errors {get; set;} = new();
		[BindProperty]
		public string ButtonPressed {get; set;}
		[BindProperty]
		public string FilterType {get;set;}
		[BindProperty]
		public string PartialProgramName {get;set;}
		[BindProperty]
		public string SelectedSchoolCode {get;set;}
		[BindProperty]
		public List<ProgramList> SearchedProgram { get; set; }
		[BindProperty]
		public ProgramItem Program {get;set;} = new();
		[BindProperty]
		public List<SelectionList> SelectListOfSchool {get;set;}
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

				if(ButtonPressed == "SearchByPartialProgramName")
				{
					SuccessMessage = "Product Search by Partial Program Name";
					FilterType = "PartialString";
				}
				else if(ButtonPressed == "SearchBySchoolCode")
				{
					SuccessMessage = "Product Search by School Code Dropdown";
					FilterType = "DropDown";
				}
				else if(ButtonPressed == "Add")
				{					
					FormValidation();					
					Program.ProgramID = ProgramServices.Add(Program);
					SuccessMessage = "Add Successful";
				}
				else if(ButtonPressed == "Update")
				{						
					FormValidation();
					ProgramServices.Edit(Program);
					SuccessMessage = "Update Successful";
				}
				else if(ButtonPressed == "Delete")
				{
					ProgramServices.Delete(Program);
					Program = new ProgramItem();
					SuccessMessage = "Delete Successful";
				}
				else if(ButtonPressed == "Clear")
				{					
					Program = new ProgramItem();
					SuccessMessage = "Clear Successful";
				}
				else if(Program.ProgramID != 0)
				{
					Program = ProgramServices.Retrieve(Program.ProgramID);
					SuccessMessage = "Product Retrieve Successful";
				}
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
				SelectListOfSchool = SchoolServices.ListOfSchool();
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
				Console.WriteLine($"QueryCrudModel: GetPrograms:filtertype= {filterType}");
				if(filterType == "PartialString")
					SearchedProgram = ProgramServices.FindProgramsByPartialProgramName(PartialProgramName);
				else if(filterType == "DropDown")
					SearchedProgram = ProgramServices.FindProgramsBySchoolCode(SelectedSchoolCode);
			}
			catch (Exception ex)
			{ 
				ErrorMessage = GetInnerException(ex);
			}
		}
		public void FormValidation()
		{
			if(string.IsNullOrEmpty(Program.ProgramName))
				Errors.Add(new Exception("ProgramName"));	

			if(Program.Tuition < 0)
				Errors.Add(new Exception("Tuition < 0"));	
			if(Program.InternationalTuition < 0)
				Errors.Add(new Exception("InternationalTuition < 0"));	
					
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
