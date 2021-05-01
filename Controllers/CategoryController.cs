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
	[RoutePrefix("api/category")]
	public class CategoryController : ApiController
	{
		private readonly ICategoryRespository _categoryRespository;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryRespository categoryRespository, IMapper mapper)
		{
			_categoryRespository = categoryRespository;
			_mapper = mapper;
		}

		[Route(Name ="GetAllCategory")]
		public async Task<IHttpActionResult> Get(bool IncludeItems = false)
		{
			try
			{
				var result = await _categoryRespository.GetCatgoryAsync(IncludeItems);
				if (result == null) return NotFound();

				var view = _mapper.Map<List<CategoryViewModel>>(result);
				return Ok(new { success = true, category = view });
			}
			catch (Exception ex)
			{
				return InternalServerError();
			}
		}

		[Route("{moniker}", Name = "GetCategory")]
		public async Task<IHttpActionResult> Get(string moniker, bool IncludeItems = false)
		{
			try
			{
				var result = await _categoryRespository.GetCatgoryByMonikerAsync(moniker, IncludeItems);
				if (result == null) return NotFound();

				var view = _mapper.Map<CategoryViewModel>(result);
				return Ok(new { success = true, category = view });
			}
			catch (Exception ex)
			{
				return InternalServerError();
			}
		}

		[Route()]
		public async Task<IHttpActionResult> Post(CategoryViewModel model)
		{
			try
			{
				var result = await _categoryRespository.GetCatgoryByMonikerAsync(model.Moniker);
				if (result != null)
				{
					ModelState.AddModelError("Category", $"Category Moniker '{model.Moniker}' value already in use");
				}
				if (model.Items != null)
				{
					foreach (InventoryItemViewModel item in model.Items)
					{
						if (await _categoryRespository.CheckItemMonikerExist(item.Moniker))
						{
							ModelState.AddModelError("Item", $"Item moniker '{item.Moniker}' already in use.");
						}
					}
				}
				if (ModelState.IsValid)
				{
					var category = _mapper.Map<Category>(model);
					_categoryRespository.AddCategory(category);
					if (await _categoryRespository.SaveContextChangesAsync())
					{
						var newCategory = _mapper.Map<CategoryViewModel>(category);
						return CreatedAtRoute("GetCategory", new { moniker = newCategory.Moniker, IncludeItems = category.Items.Count > 0 ? true : false }, newCategory);
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
		public async Task<IHttpActionResult> Put(string moniker, CategoryViewModel model)
		{
			try
			{
				var result = await _categoryRespository.GetCatgoryByMonikerAsync(moniker);
				if (result == null) return NotFound();
				foreach (InventoryItemViewModel item in model.Items)
				{
					if (await _categoryRespository.CheckItemMonikerExist(item.Moniker))
					{
						ModelState.AddModelError("Item", $"Item moniker '{item.Moniker}' already in use.");
					}
				}
				var validateItems = model.Items.GroupBy(g => g.Moniker).Where(w => w.Count() > 1).Select(s => s.Key).ToList();
				if (validateItems.Count > 0)
				{
					ModelState.AddModelError("Duplicate moniker", "Update model contains dulicate monikers.");
				}
				if (ModelState.IsValid)
				{
					_mapper.Map(model, result);

					if (await _categoryRespository.SaveContextChangesAsync())
					{
						return RedirectToRoute("GetCategory", new { moniker = result.Moniker, includeItems = result.Items.Count > 0 ? true : false });
					}
					else
					{
						return Ok(new { message = "No data to modify." });
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
		public async Task<IHttpActionResult> Delete(string moniker, bool forceFully = false)
		{
			try
			{
				var result = await _categoryRespository.GetCatgoryByMonikerAsync(moniker);
				if (result == null) return NotFound();

				var checkForItems = await _categoryRespository.GetItemByCategoryAsync(moniker);
				if (checkForItems.Count() > 0)
				{
					if (forceFully)
					{
						foreach (InventoryItem item in checkForItems)
						{
							_categoryRespository.RemoveItem(item);
						}
					}
					else
					{
						return Ok(new { success = false, message = "This category cann't be deleted as it has items." });
					}
				}

				_categoryRespository.RemoveCategory(result);
				if (await _categoryRespository.SaveContextChangesAsync())
				{
					return RedirectToRoute("GetAllCategory",new object());
				}
				return Ok(new { success = false, message= "Process completed without deleting any record."});
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}
