using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
	public class CategoryViewModel
	{
		private string moniker;

		[MinLength(8, ErrorMessage ="Required length is 8.")]
		[MaxLength(8, ErrorMessage = "Max length is 8.")]
		[Required(ErrorMessage ="This field is mandatory.")]
		[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Moniker value should have only alphanumeric characters without space.")]
		public string Moniker { get => moniker; set => moniker = value.ToUpper(); }

		[MaxLength(100)]
		[Required(ErrorMessage ="This field is mandatory.")]
		public string Name { get; set; }

		public List<InventoryItemViewModel> Items { get; set; }
	}
}