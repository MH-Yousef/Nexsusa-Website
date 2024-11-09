using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.OurCompanyDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.OurCompanyServices
{
    public class OurCompanyService : BaseService, IOurCompanyService
    {
        public OurCompanyService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<OurCompanyDTO>>> Get()
        {
            try
            {
                var companies = await _dbContext.OurCompanys
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (companies == null || !companies.Any())
                {
                    return Error<List<OurCompanyDTO>>("Our companies not found", HttpStatusCode.NotFound);
                }

                var companyDtos = _mapper.Map<List<OurCompanyDTO>>(companies);
                return Success(companyDtos);
            }
            catch (Exception ex)
            {
                return Error<List<OurCompanyDTO>>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> GetById(int id)
        {
            try
            {
                var company = await _dbContext.OurCompanys
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (company == null)
                {
                    return Error<OurCompanyDTO>("Our company not found", HttpStatusCode.NotFound);
                }

                var companyDto = _mapper.Map<OurCompanyDTO>(company);
                return Success(companyDto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> Create(OurCompanyDTO dto)
        {
            try
            {
                var company = _mapper.Map<OurCompany>(dto);
                await _dbContext.OurCompanys.AddAsync(company);
                await _dbContext.SaveChangesAsync();

                dto.Id = company.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> Update(OurCompanyDTO dto)
        {
            try
            {
                var company = await _dbContext.OurCompanys
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (company == null)
                {
                    return Error<OurCompanyDTO>("Our company not found", HttpStatusCode.NotFound);
                }

                company = _mapper.Map(dto, company);
                _dbContext.Update(company);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }

        public async Task<ResponseResult<OurCompanyDTO>> Delete(int id)
        {
            try
            {
                var company = await _dbContext.OurCompanys
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (company == null)
                {
                    return Error<OurCompanyDTO>("Our company not found", HttpStatusCode.NotFound);
                }

                _dbContext.OurCompanys.Remove(company);
                await _dbContext.SaveChangesAsync();

                return Success<OurCompanyDTO>();
            }
            catch (Exception ex)
            {
                return Error<OurCompanyDTO>(ex);
            }
        }
    }

}
