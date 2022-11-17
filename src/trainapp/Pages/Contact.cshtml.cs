using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#nullable disable

namespace MyApp.Namespace
{
    public class ContactModel : PageModel
    {
		[BindProperty]
        public string Text1{get; set;}
		[BindProperty]
        public string Email{get; set;}
        public DateTime MyDate{get;set;}
		[BindProperty]
        public string CheckBox{get;set;}
        public List<string> SelectListOfSubjects{get;set;}
        [BindProperty]
        public string MessageBody{get;set;}
        [BindProperty]
        public int SelectedSubjectId {get;set;}
        [BindProperty]
        public string ButtonPressed {get; set;}
        public string SuccessMessage {get; set;}
        public string ErrorMessage{get; set;}
        public List<Exception> errors {get; set;} = new();

        public void OnGet()
        {
            Console.WriteLine($"ContactModel: OnGet");
            PopulateSelectLists();
        }
        public IActionResult OnPost()
		{
			try
			{
				Console.WriteLine($"ContactModel: OnPost");
				PopulateSelectLists();
				if(ButtonPressed == "Submit")
				{
					Console.WriteLine($"checkbox= {CheckBox}");
					// Client Side Validation
					if (string.IsNullOrEmpty(Text1))
						errors.Add(new Exception("Text1"));
                    if (string.IsNullOrEmpty(Email))
						errors.Add(new Exception("Email"));	
                    if (SelectedSubjectId == 0)
                        errors.Add(new Exception("DropDown")); 
					if (errors.Count() > 0)
						throw new AggregateException("Missing Data: ", errors);

					SuccessMessage = $"T1={Text1}, Email={Email}, Subject={SelectListOfSubjects[SelectedSubjectId]}, MessageBody={MessageBody}, CheckBox={CheckBox}";
				} else if(ButtonPressed == "Clear")
				{	
					Text1 = null;	
					Email = null;
					MessageBody = null;
					SelectedSubjectId = 0;
					CheckBox = null;
					SuccessMessage = "Clear Successful";
					//throw new Exception("Clear button just threw an exception but lucky we caught it.");
				}
			}
			catch (AggregateException e)
			{
				ErrorMessage = e.Message;
			}
			catch (Exception e)
			{
				ErrorMessage = GetInnerException(e);
			}
			return Page();
		}
        private void PopulateSelectLists()
		{
			try{
                SelectListOfSubjects = new List<string>(){"Select Subject ...", "Contributing", "Request Membership", "Bug Report"};
            }
            catch(Exception e){
                ErrorMessage = GetInnerException(e);
            }
		}
		public string GetInnerException(Exception e)
		{
            Exception rootCause = e;
            while(rootCause.InnerException != null)
                rootCause = rootCause.InnerException;
			return rootCause.Message;
		}
    }
}
