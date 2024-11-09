using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.OurEmployeeDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.OurEmployeesItemServices
{
    public class OurEmployeesItemService : BaseService, IOurEmployeesItemService
    {
        public OurEmployeesItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<OurEmployeesItemDTO>>> Get()
        {
            try
            {
                var employeesItems = await _dbContext.OurEmployeesItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (employeesItems == null || !employeesItems.Any())
                {
                    return Error<List<OurEmployeesItemDTO>>("Our employees items not found", HttpStatusCode.NotFound);
                }

                var employeesItemsDtos = _mapper.Map<List<OurEmployeesItemDTO>>(employeesItems);
                return Success(employeesItemsDtos);
            }
            catch (Exception ex)
            {
                return Error<List<OurEmployeesItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesItemDTO>> GetById(int id)
        {
            try
            {
                var employeeItem = await _dbContext.OurEmployeesItems
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (employeeItem == null)
                {
                    return Error<OurEmployeesItemDTO>("Our employee item not found", HttpStatusCode.NotFound);
                }

                var employeeItemDto = _mapper.Map<OurEmployeesItemDTO>(employeeItem);
                return Success(employeeItemDto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesItemDTO>> Create(OurEmployeesItemDTO dto)
        {
            try
            {
                var employeeItem = _mapper.Map<OurEmployeesItem>(dto);
                await _dbContext.OurEmployeesItems.AddAsync(employeeItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = employeeItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesItemDTO>> Update(OurEmployeesItemDTO dto)
        {
            try
            {
                var employeeItem = await _dbContext.OurEmployeesItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (employeeItem == null)
                {
                    return Error<OurEmployeesItemDTO>("Our employee item not found", HttpStatusCode.NotFound);
                }

                employeeItem = _mapper.Map(dto, employeeItem);
                _dbContext.Update(employeeItem);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesItemDTO>> Delete(int id)
        {
            try
            {
                var employeeItem = await _dbContext.OurEmployeesItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (employeeItem == null)
                {
                    return Error<OurEmployeesItemDTO>("Our employee item not found", HttpStatusCode.NotFound);
                }

                _dbContext.OurEmployeesItems.Remove(employeeItem);
                await _dbContext.SaveChangesAsync();

                return Success<OurEmployeesItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesItemDTO>(ex);
            }
        }
    }

}
