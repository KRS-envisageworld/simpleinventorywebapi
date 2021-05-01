using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.Repository
{
	public interface ICategoryRespository: IItemRespository
	{
		void AddCategory(Category cat);
		void RemoveCategory(Category cat);
		Task<Category[]> GetCatgoryAsync(bool IncludeItems = false);
		Task<Category> GetCatgoryByMonikerAsync(string moniker, bool IncludeItems = false);
		Task<bool> CheckCategoryMonikerExist(string moniker);
		new Task<bool> SaveContextChangesAsync();

	}
}
