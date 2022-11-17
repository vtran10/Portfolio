using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[Table("RollingStock")]
	public partial class RollingStock
	{
		[Key]
		[StringLength(24)]
		public string ReportingMark { get; set; }
		[Required]
		[StringLength(50)]
		public string Owner { get; set; }
		[Required]		
		public int LightWeight { get; set; }
		[Required]
		public int LoadLimit { get; set; }
		[Required]
		public int Capacity { get; set; }
		public int RailCarTypeId { get; set; }
		public int YearBuilt { get; set; }
		[Required]
		public bool InService { get; set; }
		[StringLength(250)]
		public string Notes { get; set; }

		[ForeignKey(nameof(RailCarTypeId))]
		[InverseProperty("RollingStocks")]
		public virtual RailCarType RailCarType { get; set; }
	}
}
