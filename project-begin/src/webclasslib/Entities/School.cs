using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[Table("Schools")]
	public partial class School
	{
		[Key]
		[Column("SchoolCode")]
        [StringLength(10)]
		public string SchoolCode { get; set; }
		[Required]
		[Column("SchoolName")]
		[StringLength(50)]
		public string SchoolName { get; set; }

		[InverseProperty(nameof(Program.School))]
		public virtual ICollection<Program> Programs { get; set; }
	}
}
