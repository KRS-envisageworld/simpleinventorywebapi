using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.AppContext.Entites
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(8)]
		[Required]
		public string Moniker { get; set; }
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public ICollection<InventoryItem> Items { get; set; }
	}
}