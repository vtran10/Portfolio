using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ViewModels
{
	public class ProgramList
	{
		public int ProgramID { get; set; }
		public string ProgramName { get; set; }
		public string? DiplomaName { get; set; }
        public string SchoolName { get; set; }
        public decimal Tuition { get; set; }
        public decimal InternationalTuition { get; set; }
	}
}