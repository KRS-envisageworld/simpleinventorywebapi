using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.Models
{
	public class InventoryItemViewModel
	{
		private double price;

		[MinLength(8, ErrorMessage = "Required length is 8.")]
		[MaxLength(8, ErrorMessage = "Max length is 8.")]
		[Required(ErrorMessage = "This field is mandatory.")]
		[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Moniker value should have only alphanumeric characters without space.")]
		public string Moniker { get; set; }
		[MaxLength(100, ErrorMessage = "Max length is 100.")]
		[Required(ErrorMessage = "This field is mandatory.")]
		public string Name { get; set; }
		[MaxLength(500, ErrorMessage = "Max length is 500.")]
		public string Description { get; set; }
		public double Price { get => price; set => price = Math.Round(value,2); }
		public int AvailableQuantity { get; set; }
	}
}