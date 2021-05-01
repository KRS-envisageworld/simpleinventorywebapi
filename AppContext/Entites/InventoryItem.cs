using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.AppContext.Entites
{
	public class InventoryItem
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(8)]
		[Required]
		public string Moniker { get; set; }
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }
		[MaxLength(500)]
		public string Description { get; set; }
		public double Price { get; set; } = 0;
		public int AvailableQuantity { get; set; } = 0;
		public Category Category { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;

	}
}