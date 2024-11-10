using AutoMapper;
using Data.Context;
using Data.Dtos.WorkShowCaseDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkShowCaseServiceServices
{
    using AutoMapper;
    using Core.HomePage.HomePageItems;
    using Data.Context;
    using Data.Dtos.WorkShowCaseDTOs;
    using global::Services.HomePageServices.WorkShowCaseServiceServices;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    namespace Services.WorkShowCaseServices
    {
        public class WorkShowCaseService : BaseService, IWorkShowCaseServiceServices
        {
            public WorkShowCaseService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
            {
            }

            public async Task<ResponseResult<List<WorkShowCaseServiceDTO>>> GetAll()
            {
                try
                {
                    var services = await _dbContext.WorkShowCaseServices
                        .Where(x => !x.IsDeleted)
                        .Include(x => x.WorkShowCaseNavBarItem)  // Include related data
                        .ToListAsync();

                    if (services == null || !services.Any())
                    {
                        return Error<List<WorkShowCaseServiceDTO>>("No services found", HttpStatusCode.NotFound);
                    }

                    var servicesDtos = _mapper.Map<List<WorkShowCaseServiceDTO>>(services);
                    return Success(servicesDtos);
                }
                catch (Exception ex)
                {
                    return Error<List<WorkShowCaseServiceDTO>>(ex);
                }
            }

            public async Task<ResponseResult<WorkShowCaseServiceDTO>> GetById(int id)
            {
                try
                {
                    var service = await _dbContext.WorkShowCaseServices
                        .Where(x => x.Id == id && !x.IsDeleted)
                        .Include(x => x.WorkShowCaseNavBarItem)  // Include related data
                        .FirstOrDefaultAsync();

                    if (service == null)
                    {
                        return Error<WorkShowCaseServiceDTO>("Service not found", HttpStatusCode.NotFound);
                    }

                    var serviceDto = _mapper.Map<WorkShowCaseServiceDTO>(service);
                    return Success(serviceDto);
                }
                catch (Exception ex)
                {
                    return Error<WorkShowCaseServiceDTO>(ex);
                }
            }

            public async Task<ResponseResult<WorkShowCaseServiceDTO>> Create(WorkShowCaseServiceDTO dto)
            {
                try
                {
                    var service = _mapper.Map<Core.HomePage.HomePageItems.WorkShowCaseService>(dto);
                    await _dbContext.WorkShowCaseServices.AddAsync(service);
                    await _dbContext.SaveChangesAsync();

                   
                    return Success(dto);
                }
                catch (Exception ex)
                {
                    return Error<WorkShowCaseServiceDTO>(ex);
                }
            }

            public async Task<ResponseResult<WorkShowCaseServiceDTO>> Update(WorkShowCaseServiceDTO dto)
            {
                try
                {
                    var service = await _dbContext.WorkShowCaseServices
                        .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                    if (service == null)
                    {
                        return Error<WorkShowCaseServiceDTO>("Service not found", HttpStatusCode.NotFound);
                    }

                    service = _mapper.Map(dto, service);
                    _dbContext.Update(service);

                    await _dbContext.SaveChangesAsync();

                    return Success(dto);
                }
                catch (Exception ex)
                {
                    return Error<WorkShowCaseServiceDTO>(ex);
                }
            }

            public async Task<ResponseResult<WorkShowCaseServiceDTO>> Delete(int id)
            {
                try
                {
                    var service = await _dbContext.WorkShowCaseServices
                        .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                    if (service == null)
                    {
                        return Error<WorkShowCaseServiceDTO>("Service not found", HttpStatusCode.NotFound);
                    }

                    _dbContext.WorkShowCaseServices.Remove(service);
                    await _dbContext.SaveChangesAsync();

                    return Success<WorkShowCaseServiceDTO>();
                }
                catch (Exception ex)
                {
                    return Error<WorkShowCaseServiceDTO>(ex);
                }
            }
        }
    }



}
