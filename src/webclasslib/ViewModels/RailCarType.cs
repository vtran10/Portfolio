using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ViewModels
{

	public class RailCarType
	{
		public string ReportingMark { get; set; }
		public string Owner { get; set; }
		public int LightWeight { get; set; }
		public int LoadLimit { get; set; }
		public int Capacity { get; set; }
		public int RailCarTypeId { get; set; }
        public string Name { get; set; }
		public int YearBuilt { get; set; }
		public bool InService { get; set; }
		public string Notes { get; set; }
	}
}