using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ServiceDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ServiceServices
{
    public class ServiceService : BaseService, IServiceService
    {
        public ServiceService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<ServiceDTO>>> GetAll()
        {
            try
            {
                var services = await _dbContext.Services
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (services == null || !services.Any())
                {
                    return Error<List<ServiceDTO>>("No services found", HttpStatusCode.NotFound);
                }

                var serviceDtos = _mapper.Map<List<ServiceDTO>>(services);
                return Success(serviceDtos);
            }
            catch (Exception ex)
            {
                return Error<List<ServiceDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> GetById(int id)
        {
            try
            {
                var service = await _dbContext.Services
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (service == null)
                {
                    return Error<ServiceDTO>("Service not found", HttpStatusCode.NotFound);
                }

                var serviceDto = _mapper.Map<ServiceDTO>(service);
                return Success(serviceDto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> Create(ServiceDTO dto)
        {
            try
            {
                var service = _mapper.Map<Service>(dto);
                await _dbContext.Services.AddAsync(service);
                await _dbContext.SaveChangesAsync();

                dto.Id = service.Id;  // Assign the new service Id to the DTO
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> Update(ServiceDTO dto)
        {
            try
            {
                var service = await _dbContext.Services
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (service == null)
                {
                    return Error<ServiceDTO>("Service not found", HttpStatusCode.NotFound);
                }

                service = _mapper.Map(dto, service);
                _dbContext.Update(service);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceDTO>> Delete(int id)
        {
            try
            {
                var service = await _dbContext.Services
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (service == null)
                {
                    return Error<ServiceDTO>("Service not found", HttpStatusCode.NotFound);
                }

                _dbContext.Services.Remove(service);
                await _dbContext.SaveChangesAsync();

                return Success<ServiceDTO>();
            }
            catch (Exception ex)
            {
                return Error<ServiceDTO>(ex);
            }
        }
    }

}
