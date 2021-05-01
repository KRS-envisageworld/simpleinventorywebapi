using System;
using System.Data.Entity.Migrations;
using System.Linq;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.AppContext
{
	internal sealed class InitialDataLoader : DbMigrationsConfiguration<InventoryContext>
	{
		public InitialDataLoader()
		{
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "InventoryManagement.AppContext.Entites.AppContext";
		}

		protected override void Seed(InventoryContext context)
		{
			if (!context.InventoryItem.Any())
			{
				context.Category.AddOrUpdate(i => i.Id,
					new Category
					{
						Id = 1,
						Moniker = "CAT123AB",
						Name = "Category 1",
						Items = new InventoryItem[]{
						new InventoryItem{
									Id = 1,
									Moniker = "ITM111AB",
									Name = "Item 1",
									AvailableQuantity = 10,
									Description = "This is for demo only.",
									Price = 500,
								},
						new InventoryItem{
									Id = 2,
									Moniker = "ITM122BC",
									Name = "Item 2",
									AvailableQuantity = 10,
									Description = "This is for demo only.",
									Price = 45,
								}
						}
					},
					new Category
					{
						Id = 2,
						Moniker = "CAT122BA",
						Name = "Category 2",
						Items = new InventoryItem[]{
						new InventoryItem{
									Id = 3,
									Moniker = "ITM333AC",
									Name = "Item 3",
									AvailableQuantity = 10,
									Description = "This is for demo only.",
									Price = 100,
								},
						new InventoryItem{
									Id = 4,
									Moniker = "ITM444BC",
									Name = "Item 4",
									AvailableQuantity = 10,
									Description = "This is for demo only.",
									Price = 200,
								}
						}
					});
			}
		}
	}
}