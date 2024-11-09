using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WorkShowCaseDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseNavBarItemServices
{
    public class WorkShowCaseNavBarItemService : BaseService, IWorkShowCaseNavBarItemService
    {
        public WorkShowCaseNavBarItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkShowCaseNavBarItemDTO>>> GetAll()
        {
            try
            {
                var navBarItems = await _dbContext.WorkShowCaseNavBarItems
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                var navBarItemDTOs = _mapper.Map<List<WorkShowCaseNavBarItemDTO>>(navBarItems);
                return Success(navBarItemDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<WorkShowCaseNavBarItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarItemDTO>> GetById(int id)
        {
            try
            {
                var navBarItem = await _dbContext.WorkShowCaseNavBarItems
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<WorkShowCaseNavBarItemDTO>("NavBarItem not found", HttpStatusCode.NotFound);
                }

                var navBarItemDTO = _mapper.Map<WorkShowCaseNavBarItemDTO>(navBarItem);
                return Success(navBarItemDTO);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Create(WorkShowCaseNavBarItemDTO dto)
        {
            try
            {
                var navBarItem = _mapper.Map<WorkShowCaseNavBarItem>(dto);
                await _dbContext.WorkShowCaseNavBarItems.AddAsync(navBarItem);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Update(WorkShowCaseNavBarItemDTO dto)
        {
            try
            {
                var navBarItem = await _dbContext.WorkShowCaseNavBarItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (navBarItem == null)
                {
                    return Error<WorkShowCaseNavBarItemDTO>("NavBarItem not found", HttpStatusCode.NotFound);
                }

                _mapper.Map(dto, navBarItem);
                _dbContext.Update(navBarItem);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarItemDTO>> Delete(int id)
        {
            try
            {
                var navBarItem = await _dbContext.WorkShowCaseNavBarItems.FirstOrDefaultAsync(x => x.Id == id);

                if (navBarItem == null)
                {
                    return Error<WorkShowCaseNavBarItemDTO>("NavBarItem not found", HttpStatusCode.NotFound);
                }

                navBarItem.IsDeleted = true;
                _dbContext.Update(navBarItem);
                await _dbContext.SaveChangesAsync();
                return Success<WorkShowCaseNavBarItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarItemDTO>(ex);
            }
        }
    }

}
