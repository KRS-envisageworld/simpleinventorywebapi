using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InventoryManagement.AppContext;
using InventoryManagement.AppContext.Entites;
using Microsoft.Ajax.Utilities;

namespace InventoryManagement.Repository
{
	public class CategoryRepository : ICategoryRespository
	{
		private readonly InventoryContext _context;

		public CategoryRepository(InventoryContext context)
		{
			_context = context;
		}

		#region Category
		public void AddCategory(Category cat)
		{
			_context.Category.Add(cat);
		}
		public void RemoveCategory(Category cat)
		{
			_context.Category.Remove(cat);
		}
		public async Task<Category[]> GetCatgoryAsync(bool IncludeItems = false)
		{
			IQueryable<Category> query = _context.Category;
			if (IncludeItems)
			{
				query = query.Include(i => i.Items);
			}
			query = query.OrderBy(o => o.Name);

			return await query.ToArrayAsync();
		}
		public async Task<Category> GetCatgoryByMonikerAsync(string moniker, bool IncludeItems = false)
		{
			IQueryable<Category> query = _context.Category;
			if (IncludeItems)
			{
				query = query.Include(i => i.Items);
			}
			return await (query.Where(w => w.Moniker == moniker).FirstOrDefaultAsync());
		}
		#endregion

		#region Items
		public void AddItem(InventoryItem item)
		{
			_context.InventoryItem.Add(item);
		}
		public void RemoveItem(InventoryItem item)
		{
			_context.InventoryItem.Remove(item);
		}
		public async Task<InventoryItem[]> GetItemsAsync(string category)
		{
			IQueryable<InventoryItem> query = _context.InventoryItem;
			query = query.Where(w => w.Category.Moniker == category).OrderBy(o => o.Id);

			return await query.ToArrayAsync();
		}
		public async Task<InventoryItem> GetItemByMonikerAsync(string category, string moniker)
		{
			IQueryable<InventoryItem> query = _context.InventoryItem;
			query = query.Where(w => w.Moniker == moniker && w.Category.Moniker == category);
			return await query.FirstOrDefaultAsync();
		}
		public async Task<bool> CheckItemMonikerExist(string moniker)
		{
			IQueryable<InventoryItem> query = _context.InventoryItem;
			return await query.Where(w => w.Moniker == moniker).AnyAsync();
		}
		public async Task<bool> CheckCategoryMonikerExist(string moniker)
		{
			IQueryable<Category> query = _context.Category;
			return await query.Where(w => w.Moniker == moniker).AnyAsync();
		}
		public async Task<InventoryItem[]> GetItemByCategoryAsync(string category)
		{
			IQueryable<InventoryItem> query = _context.InventoryItem;
			query = query.Where(w => w.Category.Moniker.ToUpper() == category.ToUpper());

			return await (query.ToArrayAsync());
		}
		#endregion

		public async Task<bool> SaveContextChangesAsync()
		{
			return (await _context.SaveChangesAsync()) > 0;
		}
	}
}