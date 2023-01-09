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
		[Column("ReportingMark")]
		[StringLength(24)]
		public string ReportingMark { get; set; }
		[Required]
		[Column("Owner")]
		[StringLength(50)]
		public string Owner { get; set; }
		[Required]		
		[Column("LightWeight")]
		public int LightWeight { get; set; }
		[Required]
		[Column("LoadLimit")]
		public int LoadLimit { get; set; }
		[Required]
		[Column("Capacity")]		
		public int Capacity { get; set; }
		[Column("RailCarTypeID")]		
		public int? RailCarTypeID { get; set; }
		[Column("YearBuilt")]		
		public int? YearBuilt { get; set; }
		[Required]
		[Column("InService")]		
		public bool InService { get; set; }
		[Column("Notes")]		
		[StringLength(250)]
		public string? Notes { get; set; }

		[ForeignKey(nameof(RailCarTypeID))]
		[InverseProperty("RollingStocks")]
		public virtual RailCarType RailCarType { get; set; }
	}
}
