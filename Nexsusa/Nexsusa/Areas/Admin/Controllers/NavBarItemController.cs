﻿using Core.HomePage.HomePageItems;
using Data.Dtos.NavBarDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services._Base;
using Services.HomePageServices.NavBarItemServices;

namespace Nexsusa_Api.Areas.Admin.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NavBarItemController(INavBarItemService _navBarItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<ResponseResult<List<NavBarItemDTO>>> Get(int languageId)
        {
            var result = await _navBarItemService.GetList(languageId);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult<NavBarItemDTO>> GetById(int id, int languageId)
        {
            var result = await _navBarItemService.GetById(id, languageId);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResult<List<NavBarItemDTO>>> Manage(List<NavBarItemDTO> dto)
        {
            var result = await _navBarItemService.Manage(dto);
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ResponseResult<NavBarItemDTO>> Delete(int id)
        {
            var result = await _navBarItemService.Delete(id);
            return result;
        }


    }
}
