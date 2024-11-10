using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.NavBarDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.NavBarItemServices
{
    public class NavBarItemService : BaseService, INavBarItemService
    {
        private readonly IGenericService genericService;
        public NavBarItemService(AppDbContext dbContext, IMapper mapper, IGenericService genericService) : base(dbContext, mapper)
        {
            this.genericService = genericService;
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

        public async Task<ResponseResult<List<NavBarItemDTO>>> Create(List<NavBarItemDTO> dtos)
        {
            try
            {
                var defultLanguage = await _dbContext.Languages.FirstAsync(x => x.IsDefault);
                var defultModel = dtos.Where(x => x.LangId == defultLanguage.Id).FirstOrDefault();
                var all = _mapper.Map<List<NavBarItem>>(dtos);
                var navBarItem = _mapper.Map<NavBarItemDTO,NavBarItem>(defultModel);
                var result = await genericService.CreateAync(dtos);
                // Add Translations
                foreach (var item in dtos)
                {
                    var translations = new List<(string ColumnName, string ColumnValue)>
                            {
                                ("Name", item.Name),
                            };
                    await genericService.AddTranslationsAsync(translations, navBarItem.Id, item.LangId);
                }
                return result;
            }
            catch (Exception ex)
            {
                return Error<List<NavBarItemDTO>>(ex);
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
