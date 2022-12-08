using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[Table("Programs")]
	public partial class Program
	{
		[Key]
		[Column("ProgramID")]
		public int ProgramID { get; set; }
		[Required]
		[Column("ProgramName")]		
		[StringLength(100)]
		public string ProgramName { get; set; }		
		[Column("DiplomaName")]		
		[StringLength(100)]
		public string? DiplomaName { get; set; }
		[Required]
		[Column("SchoolCode")]
		[StringLength(10)]
		public string SchoolCode { get; set; }
		[Required]
		[Column("Tuition")]
		public decimal Tuition { get; set; }
		[Required]
		[Column("InternationalTuition")]
		public decimal InternationalTuition { get; set; }

		[ForeignKey(nameof(SchoolCode))]
		[InverseProperty("Programs")]
		public virtual School School { get; set; }
	}
}
