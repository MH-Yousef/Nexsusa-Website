using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WorkingProcessDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkingProcessItemServices
{
    public class WorkingProcessItemService : BaseService, IWorkingProcessItemService
    {
        public WorkingProcessItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkingProcessItemDTO>>> GetAll()
        {
            try
            {
                var workingProcessItems = await _dbContext.WorkingProcessItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (workingProcessItems == null || !workingProcessItems.Any())
                {
                    return Error<List<WorkingProcessItemDTO>>("No records found", HttpStatusCode.NotFound);
                }

                var workingProcessItemsDto = _mapper.Map<List<WorkingProcessItemDTO>>(workingProcessItems);
                return Success(workingProcessItemsDto);
            }
            catch (Exception ex)
            {
                return Error<List<WorkingProcessItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessItemDTO>> GetById(int id)
        {
            try
            {
                var workingProcessItem = await _dbContext.WorkingProcessItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (workingProcessItem == null)
                {
                    return Error<WorkingProcessItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                var workingProcessItemDto = _mapper.Map<WorkingProcessItemDTO>(workingProcessItem);
                return Success(workingProcessItemDto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessItemDTO>> Create(WorkingProcessItemDTO dto)
        {
            try
            {
                var workingProcessItem = _mapper.Map<WorkingProcessItem>(dto);
                await _dbContext.WorkingProcessItems.AddAsync(workingProcessItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = workingProcessItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessItemDTO>> Update(WorkingProcessItemDTO dto)
        {
            try
            {
                var workingProcessItem = await _dbContext.WorkingProcessItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (workingProcessItem == null)
                {
                    return Error<WorkingProcessItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                workingProcessItem = _mapper.Map(dto, workingProcessItem);
                _dbContext.WorkingProcessItems.Update(workingProcessItem);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessItemDTO>> Delete(int id)
        {
            try
            {
                var workingProcessItem = await _dbContext.WorkingProcessItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (workingProcessItem == null)
                {
                    return Error<WorkingProcessItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                _dbContext.WorkingProcessItems.Remove(workingProcessItem);
                await _dbContext.SaveChangesAsync();

                return Success<WorkingProcessItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessItemDTO>(ex);
            }
        }
    }

}
