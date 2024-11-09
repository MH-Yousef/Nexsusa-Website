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

namespace Services.ServiceItemServices
{
    public class ServiceItemService : BaseService, IServiceItemService
    {
        public ServiceItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<ServiceItemDTO>>> GetAll()
        {
            try
            {
                var serviceItems = await _dbContext.ServiceItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (serviceItems == null || !serviceItems.Any())
                {
                    return Error<List<ServiceItemDTO>>("No service items found", HttpStatusCode.NotFound);
                }

                var serviceItemDtos = _mapper.Map<List<ServiceItemDTO>>(serviceItems);
                return Success(serviceItemDtos);
            }
            catch (Exception ex)
            {
                return Error<List<ServiceItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ServiceItemDTO>> GetById(int id)
        {
            try
            {
                var serviceItem = await _dbContext.ServiceItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (serviceItem == null)
                {
                    return Error<ServiceItemDTO>("Service item not found", HttpStatusCode.NotFound);
                }

                var serviceItemDto = _mapper.Map<ServiceItemDTO>(serviceItem);
                return Success(serviceItemDto);
            }
            catch (Exception ex)
            {
                return Error<ServiceItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceItemDTO>> Create(ServiceItemDTO dto)
        {
            try
            {
                var serviceItem = _mapper.Map<ServiceItem>(dto);
                await _dbContext.ServiceItems.AddAsync(serviceItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = serviceItem.Id;  // Assign the new service item Id to the DTO
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceItemDTO>> Update(ServiceItemDTO dto)
        {
            try
            {
                var serviceItem = await _dbContext.ServiceItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (serviceItem == null)
                {
                    return Error<ServiceItemDTO>("Service item not found", HttpStatusCode.NotFound);
                }

                serviceItem = _mapper.Map(dto, serviceItem);
                _dbContext.Update(serviceItem);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ServiceItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ServiceItemDTO>> Delete(int id)
        {
            try
            {
                var serviceItem = await _dbContext.ServiceItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (serviceItem == null)
                {
                    return Error<ServiceItemDTO>("Service item not found", HttpStatusCode.NotFound);
                }

                _dbContext.ServiceItems.Remove(serviceItem);
                await _dbContext.SaveChangesAsync();

                return Success<ServiceItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<ServiceItemDTO>(ex);
            }
        }
    }

}
