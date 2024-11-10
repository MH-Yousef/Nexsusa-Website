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

namespace Services.HomePageServices.WorkingProcessServices
{
    public class WorkingProcessService : BaseService, IWorkingProcessService
    {
        public WorkingProcessService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WorkingProcessDTO>>> GetAll()
        {
            try
            {
                var workingProcesses = await _dbContext.WorkingProcesses
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.WorkingProcessItems)
                    .ToListAsync();

                if (workingProcesses == null || !workingProcesses.Any())
                {
                    return Error<List<WorkingProcessDTO>>("No records found", HttpStatusCode.NotFound);
                }

                var workingProcessesDto = _mapper.Map<List<WorkingProcessDTO>>(workingProcesses);
                return Success(workingProcessesDto);
            }
            catch (Exception ex)
            {
                return Error<List<WorkingProcessDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessDTO>> GetById(int id)
        {
            try
            {
                var workingProcess = await _dbContext.WorkingProcesses
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .Include(x => x.WorkingProcessItems)
                    .FirstOrDefaultAsync();

                if (workingProcess == null)
                {
                    return Error<WorkingProcessDTO>("Record not found", HttpStatusCode.NotFound);
                }

                var workingProcessDto = _mapper.Map<WorkingProcessDTO>(workingProcess);
                return Success(workingProcessDto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessDTO>> Create(WorkingProcessDTO dto)
        {
            try
            {
                var workingProcess = _mapper.Map<WorkingProcess>(dto);
                await _dbContext.WorkingProcesses.AddAsync(workingProcess);
                await _dbContext.SaveChangesAsync();

                dto.Id = workingProcess.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessDTO>> Update(WorkingProcessDTO dto)
        {
            try
            {
                var workingProcess = await _dbContext.WorkingProcesses
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (workingProcess == null)
                {
                    return Error<WorkingProcessDTO>("Record not found", HttpStatusCode.NotFound);
                }

                workingProcess = _mapper.Map(dto, workingProcess);
                _dbContext.WorkingProcesses.Update(workingProcess);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }

        public async Task<ResponseResult<WorkingProcessDTO>> Delete(int id)
        {
            try
            {
                var workingProcess = await _dbContext.WorkingProcesses
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (workingProcess == null)
                {
                    return Error<WorkingProcessDTO>("Record not found", HttpStatusCode.NotFound);
                }

                _dbContext.WorkingProcesses.Remove(workingProcess);
                await _dbContext.SaveChangesAsync();

                return Success<WorkingProcessDTO>();
            }
            catch (Exception ex)
            {
                return Error<WorkingProcessDTO>(ex);
            }
        }
    }


}
