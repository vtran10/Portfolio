using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[Table("DbVersion")]
	public partial class DbVersion
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }
		[Required]
		[Column("Major")]
		public int Major { get; set; }
		[Required]
		[Column("Minor")]
		public int Minor { get; set; }
		[Required]
		[Column("Build")]
		public int Build { get; set; }
		[Column("ReleaseDate", TypeName = "datetime")]
		public DateTime ReleaseDate { get; set; }

		public override string ToString() 
		{
			return $"Id: {Id}, Major: {Major}, Minor: {Minor}, Build: {Build}, Release Date: {ReleaseDate}";
		}
	}
}
