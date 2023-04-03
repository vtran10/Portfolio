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
	public class ProgramServices
	{
		#region Constructor Dependency Injection
		private readonly Context Context;
		public ProgramServices(Context context)
		{
			if (context == null)
				throw new ArgumentNullException();
			Context = context;
		}
		#endregion

        #region Queries

        public List<ProgramList> FindProgramsByPartialProgramName(string partialProgramName)
		{
			Console.WriteLine($"ProgramServices: FindByPartialName(); partialName= {partialProgramName}");
			var info =
				Context.Programs
				.Where(x => x.ProgramName.Contains(partialProgramName))
				.Select(x => new ProgramList
				{
					ProgramID = x.ProgramID,
                    ProgramName = x.ProgramName,
                    DiplomaName = x.DiplomaName,
                    SchoolName = x.School.SchoolName,
                    Tuition = x.Tuition,
                    InternationalTuition = x.InternationalTuition
				})
				.OrderBy(x => x.ProgramName);
			return info.ToList();
		}   

        public List<ProgramList> FindProgramsBySchoolCode(string SchoolCode)
		{
			Console.WriteLine($"ProgramServices: FindProgramsBySchoolCode(); partialName= {SchoolCode}");
			var info =
				Context.Programs
				.Where(x => x.SchoolCode == SchoolCode)
				.Select(x => new ProgramList
				{
					ProgramID = x.ProgramID,
                    ProgramName = x.ProgramName,
                    DiplomaName = x.DiplomaName,
                    SchoolName = x.School.SchoolName,
                    Tuition = x.Tuition,
                    InternationalTuition = x.InternationalTuition
				})
				.OrderBy(x => x.ProgramName);
			return info.ToList();
		}   

        #endregion

		#region READ - Retrieve, Edit, Add, Delete
		public ProgramItem Retrieve(int id)
		{
			Console.WriteLine($"ProgramServices: Retrieve; ProgramID= {id}");
			var info = 
				Context.Programs
				.Where(x => x.ProgramID == id)
				.Select(x => new ProgramItem
				{
					ProgramID = x.ProgramID,
                    ProgramName = x.ProgramName,
                    DiplomaName = x.DiplomaName,
                    SchoolCode = x.SchoolCode,
                    Tuition = x.Tuition,
                    InternationalTuition = x.InternationalTuition
				}).FirstOrDefault();
				Console.WriteLine($"End");
			return info;
		}
		
		public void Edit(ProgramItem item)
		{
			Console.WriteLine($"ProgramServices: Edit; ProgramID= {item.ProgramID}");

			//BLL Validation for non existing rolling stock
			Program existing = Context.Programs.Find(item.ProgramID);
				if (existing == null)
				throw new Exception("Program does not exist");

				existing.ProgramID = item.ProgramID;
				existing.ProgramName = item.ProgramName;
				existing.DiplomaName = item.DiplomaName;
				existing.SchoolCode = item.SchoolCode;
				existing.Tuition = item.Tuition;
				existing.InternationalTuition = item.InternationalTuition;

			EntityEntry<Program> updating = Context.Entry(existing);
			updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			Context.SaveChanges();
		}

		public int Add(ProgramItem item)
		{
			Console.WriteLine($"ProgramServices: Add; ProgramID= {item.ProgramID}");

			//BLL Validation
			//for no ReportingMark duplicates
			var exists = 
				Context.Programs.FirstOrDefault(x => 
				x.ProgramID == item.ProgramID &&
				x.ProgramName == item.ProgramName &&
				x.DiplomaName == item.DiplomaName &&
				x.SchoolCode == item.SchoolCode &&
				x.Tuition == item.Tuition &&
				x.InternationalTuition == item.InternationalTuition);
			if (exists != null)
				throw new Exception("A program with the same product id and program name etc already exists");

			var newProgram = new Program();
			newProgram.ProgramID = item.ProgramID;
			newProgram.ProgramName = item.ProgramName;
			newProgram.DiplomaName = item.DiplomaName;
			newProgram.SchoolCode = item.SchoolCode;
			newProgram.Tuition = item.Tuition;
			newProgram.InternationalTuition = item.InternationalTuition;
			
			Context.Programs.Add(newProgram);
			Context.SaveChanges();
			return newProgram.ProgramID;
		}

		public void Delete(ProgramItem item)
		{
		Console.WriteLine($"ProgramServices: Delete; ProgramID= {item.ProgramID}");

		//BLL Validation
		Program existing = Context.Programs.Find(item.ProgramID);
			if (existing == null)
			throw new Exception("Rolling Stock does not exist");

		EntityEntry<Program> removing = Context.Entry(existing);
		removing.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
		Context.SaveChanges();
		}

		#endregion
    }
}