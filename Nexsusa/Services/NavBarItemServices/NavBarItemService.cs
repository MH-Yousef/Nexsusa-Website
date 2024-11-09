using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.NavBarDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services.NavBarServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.NavBarItemServices
{
    public class NavBarItemService : BaseService, INavBarItemService
    {
        public NavBarItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<NavBarItemDTO>>> Get()
        {
            try
            {
                var navBarItems = await _dbContext.NavBarItems
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.NavBarItemSubItems) // SubItem'ları dahil ediyoruz
                    .ToListAsync();

                if (navBarItems == null || !navBarItems.Any())
                {
                    return Error<List<NavBarItemDTO>>("NavBar items not found", HttpStatusCode.NotFound);
                }

                var navBarItemDtos = _mapper.Map<List<NavBarItemDTO>>(navBarItems);
                return Success(navBarItemDtos);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> GetById(int id)
        {
            try
            {
                var navBarItem = await _dbContext.NavBarItems
                    .Include(x => x.NavBarItemSubItems) // SubItem'ları dahil ediyoruz
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<NavBarItemDTO>("NavBar item not found", HttpStatusCode.NotFound);
                }

                var navBarItemDto = _mapper.Map<NavBarItemDTO>(navBarItem);
                return Success(navBarItemDto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> Create(NavBarItemDTO dto)
        {
            try
            {
                var navBarItem = _mapper.Map<NavBarItem>(dto);
                await _dbContext.NavBarItems.AddAsync(navBarItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = navBarItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> Update(NavBarItemDTO dto)
        {
            try
            {
                var navBarItem = await _dbContext.NavBarItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<NavBarItemDTO>("NavBar item not found", HttpStatusCode.NotFound);
                }

                navBarItem = _mapper.Map(dto, navBarItem);
                _dbContext.Update(navBarItem);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemDTO>> Delete(int id)
        {
            try
            {
                var navBarItem = await _dbContext.NavBarItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<NavBarItemDTO>("NavBar item not found", HttpStatusCode.NotFound);
                }

                _dbContext.NavBarItems.Remove(navBarItem);
                await _dbContext.SaveChangesAsync();

                return Success<NavBarItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<NavBarItemDTO>(ex);
            }
        }
    }

}
