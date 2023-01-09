using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[Table("RailCarTypes")]
	public partial class RailCarType
	{
		[Key]
		[Column("RailCarTypeID")]
		public int RailCarTypeID { get; set; }
		[Required]
		[Column("Name")]
		[StringLength(30)]
		public string Name { get; set; }
		[Required]
		[Column("Commodity")]
		[StringLength(100)]
		public string Commodity { get; set; }

		[InverseProperty(nameof(RollingStock.RailCarType))]
		public virtual ICollection<RollingStock> RollingStocks { get; set; }
	}
}
