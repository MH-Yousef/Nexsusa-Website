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

namespace Services.HomePageServices.WorkShowCaseServices
{
    public class WorkShowCaseService : BaseService, IWorkShowCaseService
    {
        public WorkShowCaseService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkShowCaseDTO>>> GetAll()
        {
            try
            {
                var workShowCases = await _dbContext.WorkShowCases
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                var workShowCaseDTOs = _mapper.Map<List<WorkShowCaseDTO>>(workShowCases);

                return Success(workShowCaseDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<WorkShowCaseDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> GetById(int id)
        {
            try
            {
                var workShowCase = await _dbContext.WorkShowCases
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (workShowCase == null)
                {
                    return Error<WorkShowCaseDTO>("WorkShowCase not found", HttpStatusCode.NotFound);
                }

                var workShowCaseDTO = _mapper.Map<WorkShowCaseDTO>(workShowCase);
                return Success(workShowCaseDTO);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> Create(WorkShowCaseDTO dto)
        {
            try
            {
                var workShowCase = _mapper.Map<WorkShowCase>(dto);
                await _dbContext.WorkShowCases.AddAsync(workShowCase);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> Update(WorkShowCaseDTO dto)
        {
            try
            {
                var workShowCase = await _dbContext.WorkShowCases
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (workShowCase == null)
                {
                    return Error<WorkShowCaseDTO>("WorkShowCase not found", HttpStatusCode.NotFound);
                }

                _mapper.Map(dto, workShowCase);
                _dbContext.Update(workShowCase);
                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkShowCaseDTO>> Delete(int id)
        {
            try
            {
                var workShowCase = await _dbContext.WorkShowCases.FirstOrDefaultAsync(x => x.Id == id);

                if (workShowCase == null)
                {
                    return Error<WorkShowCaseDTO>("WorkShowCase not found", HttpStatusCode.NotFound);
                }

                workShowCase.IsDeleted = true;
                _dbContext.Update(workShowCase);
                await _dbContext.SaveChangesAsync();
                return Success<WorkShowCaseDTO>();
            }
            catch (Exception ex)
            {
                return Error<WorkShowCaseDTO>(ex);
            }
        }
    }

}
