using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using InventoryManagement.AppContext.Entites;
using InventoryManagement.Models;
using InventoryManagement.Repository;

namespace InventoryManagement.Controllers
{
	[RoutePrefix("api/category/{categoryMoniker}/Items")]
	public class InventoryItemsController : ApiController
	{
		private readonly IItemRespository _itemRespository;
		private readonly IMapper _mapper;
		public InventoryItemsController(ICategoryRespository repository, IMapper mapper)
		{
			_itemRespository = repository;
			_mapper = mapper;
		}

		[Route(Name = "GetItems")]
		public async Task<IHttpActionResult> Get(string categoryMoniker)
		{
			try
			{
				var result = await _itemRespository.GetItemByCategoryAsync(categoryMoniker);
				if (result == null) return NotFound();

				var returnData = _mapper.Map<List<InventoryItemViewModel>>(result);
				return Ok(new { success = true, items = returnData });
			}
			catch (Exception ex)
			{
				return InternalServerError();
			}
		}

		[Route("{moniker}")]
		public async Task<IHttpActionResult> Get(string categoryMoniker, string moniker)
		{
			try
			{
				var result = await _itemRespository.GetItemByMonikerAsync(categoryMoniker, moniker);
				if (result == null) return NotFound();

				var returnData = _mapper.Map<InventoryItemViewModel>(result);
				return Ok(new { success = true, items = returnData });
			}
			catch (Exception ex)
			{
				return InternalServerError();
			}
		}

		[Route()]
		public async Task<IHttpActionResult> Post(string categoryMoniker, InventoryItemViewModel model)
		{
			try
			{
				if (await _itemRespository.CheckItemMonikerExist(model.Moniker))
				{
					ModelState.AddModelError("Item", $"Item moniker '{model.Moniker}' already in use.");
				}

				if (ModelState.IsValid)
				{
					var item = _mapper.Map<InventoryItem>(model);

					_itemRespository.AddItem(item);
					if (await _itemRespository.SaveContextChangesAsync())
					{
						var newItem = _mapper.Map<InventoryItemViewModel>(item);
						return CreatedAtRoute("GetItems", new { categoryMoniker }, newItem);
					}
				}
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
			return BadRequest(ModelState);
		}

		[Route("{moniker}")]
		public async Task<IHttpActionResult> Put(string categoryMoniker, string moniker, InventoryItemViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await _itemRespository.GetItemByMonikerAsync(categoryMoniker, moniker);
					if (result == null) return NotFound();

					_mapper.Map(model, result);

					if (await _itemRespository.SaveContextChangesAsync())
					{
						return RedirectToRoute("GetItems", new { categoryMoniker });
					}
					else
					{
						return Ok(new { message = "No modified properties to update." });
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		[Route("{moniker}")]
		public async Task<IHttpActionResult> Delete(string categoryMoniker, string moniker)
		{
			try
			{
				var result = await _itemRespository.GetItemByMonikerAsync(categoryMoniker, moniker);
				if (result == null) return NotFound();

				_itemRespository.RemoveItem(result);

				if (await _itemRespository.SaveContextChangesAsync())
				{
					return RedirectToRoute("GetItems", new { categoryMoniker });
				}
				return Ok(new { success = false, message = "Process completed without deleting any record." });
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}
