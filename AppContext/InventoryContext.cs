using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.AppContext
{
	public class InventoryContext : DbContext
	{
		public InventoryContext():base("InventoryConnection")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<InventoryContext, InitialDataLoader>());
		}

		public DbSet<Category> Category { get; set; }
		public DbSet<InventoryItem> InventoryItem { get; set; }

	}
}