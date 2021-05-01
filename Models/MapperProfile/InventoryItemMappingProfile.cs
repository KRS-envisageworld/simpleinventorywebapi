using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using InventoryManagement.AppContext.Entites;

namespace InventoryManagement.Models.MapperProfile
{
	public class InventoryItemMappingProfile : Profile
	{
		public InventoryItemMappingProfile()
		{
			CreateMap<InventoryItem, InventoryItemViewModel>()
				.ReverseMap();
		}
	}
}