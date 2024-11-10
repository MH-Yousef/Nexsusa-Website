using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.NavBarDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.NavBarSubItemServices
{
    public class NavBarItemSubItemService : BaseService, INavBarItemSubItemService
    {
        public NavBarItemSubItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<NavBarItemSubItemDTO>>> Get()
        {
            try
            {
                var subItems = await _dbContext.NavBarItemSubItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (subItems == null || !subItems.Any())
                {
                    return Error<List<NavBarItemSubItemDTO>>("NavBar item subitems not found", HttpStatusCode.NotFound);
                }

                var subItemsDtos = _mapper.Map<List<NavBarItemSubItemDTO>>(subItems);
                return Success(subItemsDtos);
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemSubItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemSubItemDTO>> GetById(int id)
        {
            try
            {
                var subItem = await _dbContext.NavBarItemSubItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (subItem == null)
                {
                    return Error<NavBarItemSubItemDTO>("NavBar item subitem not found", HttpStatusCode.NotFound);
                }

                var subItemDto = _mapper.Map<NavBarItemSubItemDTO>(subItem);
                return Success(subItemDto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemSubItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemSubItemDTO>> Create(NavBarItemSubItemDTO dto)
        {
            try
            {
                var subItem = _mapper.Map<NavBarItemSubItem>(dto);
                await _dbContext.NavBarItemSubItems.AddAsync(subItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = subItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemSubItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemSubItemDTO>> Update(NavBarItemSubItemDTO dto)
        {
            try
            {
                var subItem = await _dbContext.NavBarItemSubItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (subItem == null)
                {
                    return Error<NavBarItemSubItemDTO>("NavBar item subitem not found", HttpStatusCode.NotFound);
                }

                subItem = _mapper.Map(dto, subItem);
                _dbContext.Update(subItem);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<NavBarItemSubItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<NavBarItemSubItemDTO>> Delete(int id)
        {
            try
            {
                var subItem = await _dbContext.NavBarItemSubItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (subItem == null)
                {
                    return Error<NavBarItemSubItemDTO>("NavBar item subitem not found", HttpStatusCode.NotFound);
                }

                _dbContext.NavBarItemSubItems.Remove(subItem);
                await _dbContext.SaveChangesAsync();

                return Success<NavBarItemSubItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<NavBarItemSubItemDTO>(ex);
            }
        }
    }

}
