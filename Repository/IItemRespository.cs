using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.Repository
{
	public interface IItemRespository
	{
		Task<InventoryItem[]> GetItemsAsync(string category);
		Task<InventoryItem> GetItemByMonikerAsync(string category, string moniker);
		Task<InventoryItem[]> GetItemByCategoryAsync(string category);
		void AddItem(InventoryItem item);
		void RemoveItem(InventoryItem item);
		Task<bool> CheckItemMonikerExist(string moniker);
		Task<bool> SaveContextChangesAsync();
	}
}
