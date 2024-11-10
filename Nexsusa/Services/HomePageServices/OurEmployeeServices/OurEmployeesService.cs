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

namespace Services.HomePageServices.OurEmployeeServices
{
    public class OurEmployeesService : BaseService, IOurEmployeesService
    {
        public OurEmployeesService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<OurEmployeesDTO>>> Get()
        {
            try
            {
                var employees = await _dbContext.OurEmployees
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.OurEmployeesItems)  // Include to load related items
                    .ToListAsync();

                if (employees == null || !employees.Any())
                {
                    return Error<List<OurEmployeesDTO>>("Our employees not found", HttpStatusCode.NotFound);
                }

                var employeesDtos = _mapper.Map<List<OurEmployeesDTO>>(employees);
                return Success(employeesDtos);
            }
            catch (Exception ex)
            {
                return Error<List<OurEmployeesDTO>>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> GetById(int id)
        {
            try
            {
                var employee = await _dbContext.OurEmployees
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .Include(x => x.OurEmployeesItems)  // Include to load related items
                    .FirstOrDefaultAsync();

                if (employee == null)
                {
                    return Error<OurEmployeesDTO>("Our employee not found", HttpStatusCode.NotFound);
                }

                var employeeDto = _mapper.Map<OurEmployeesDTO>(employee);
                return Success(employeeDto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> Create(OurEmployeesDTO dto)
        {
            try
            {
                var employee = _mapper.Map<OurEmployees>(dto);
                await _dbContext.OurEmployees.AddAsync(employee);
                await _dbContext.SaveChangesAsync();

                dto.Id = employee.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> Update(OurEmployeesDTO dto)
        {
            try
            {
                var employee = await _dbContext.OurEmployees
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (employee == null)
                {
                    return Error<OurEmployeesDTO>("Our employee not found", HttpStatusCode.NotFound);
                }

                employee = _mapper.Map(dto, employee);
                _dbContext.Update(employee);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurEmployeesDTO>> Delete(int id)
        {
            try
            {
                var employee = await _dbContext.OurEmployees
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (employee == null)
                {
                    return Error<OurEmployeesDTO>("Our employee not found", HttpStatusCode.NotFound);
                }

                _dbContext.OurEmployees.Remove(employee);
                await _dbContext.SaveChangesAsync();

                return Success<OurEmployeesDTO>();
            }
            catch (Exception ex)
            {
                return Error<OurEmployeesDTO>(ex);
            }
        }
    }

}
