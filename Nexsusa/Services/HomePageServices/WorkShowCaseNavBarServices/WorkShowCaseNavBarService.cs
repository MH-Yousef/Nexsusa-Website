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

namespace Services.HomePageServices.WorkShowCaseNavBarServices
{
    public class WorkShowCaseNavBarService : BaseService, IWorkShowCaseNavBarService
    {
        public WorkShowCaseNavBarService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkShowCaseNavBarDTO>>> GetAll()
        {
            try
            {
                var navBars = await _dbContext.WorkShowCaseNavBars
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                var navBarDTOs = _mapper.Map<List<WorkShowCaseNavBarDTO>>(navBars);
                return Success(navBarDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<WorkShowCaseNavBarDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarDTO>> GetById(int id)
        {
            try
            {
                var navBar = await _dbContext.WorkShowCaseNavBars
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (navBar == null)
                {
                    return Error<WorkShowCaseNavBarDTO>("NavBar not found", HttpStatusCode.NotFound);
                }

                var navBarDTO = _mapper.Map<WorkShowCaseNavBarDTO>(navBar);
                return Success(navBarDTO);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarDTO>> Create(WorkShowCaseNavBarDTO dto)
        {
            try
            {
                var navBar = _mapper.Map<WorkShowCaseNavBar>(dto);
                await _dbContext.WorkShowCaseNavBars.AddAsync(navBar);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarDTO>> Update(WorkShowCaseNavBarDTO dto)
        {
            try
            {
                var navBar = await _dbContext.WorkShowCaseNavBars
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (navBar == null)
                {
                    return Error<WorkShowCaseNavBarDTO>("NavBar not found", HttpStatusCode.NotFound);
                }

                _mapper.Map(dto, navBar);
                _dbContext.Update(navBar);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseNavBarDTO>> Delete(int id)
        {
            try
            {
                var navBar = await _dbContext.WorkShowCaseNavBars.FirstOrDefaultAsync(x => x.Id == id);

                if (navBar == null)
                {
                    return Error<WorkShowCaseNavBarDTO>("NavBar not found", HttpStatusCode.NotFound);
                }

                navBar.IsDeleted = true;
                _dbContext.Update(navBar);
                await _dbContext.SaveChangesAsync();
                return Success<WorkShowCaseNavBarDTO>();
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseNavBarDTO>(ex);
            }
        }
    }

}
