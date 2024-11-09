using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.FooterServiceDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.FooterSubServices
{
    public class FooterServiceService : BaseService, IFooterServiceService
    {
        public FooterServiceService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<FooterServiceDTO>>> Get()
        {
            try
            {
                var footerServices = await _dbContext.FooterServices
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (footerServices == null || !footerServices.Any())
                {
                    return Error<List<FooterServiceDTO>>("Footer services not found", HttpStatusCode.NotFound);
                }

                var footerServicesDto = _mapper.Map<List<FooterServiceDTO>>(footerServices);
                return Success(footerServicesDto);
            }
            catch (Exception ex)
            {
                return Error<List<FooterServiceDTO>>(ex);
            }
        }

        public async Task<ResponseResult<FooterServiceDTO>> GetById(int id)
        {
            try
            {
                var footerService = await _dbContext.FooterServices
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (footerService == null)
                {
                    return Error<FooterServiceDTO>("Footer service not found", HttpStatusCode.NotFound);
                }

                var footerServiceDto = _mapper.Map<FooterServiceDTO>(footerService);
                return Success(footerServiceDto);
            }
            catch (Exception ex)
            {
                return Error<FooterServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterServiceDTO>> Create(FooterServiceDTO dto)
        {
            try
            {
                var footerService = _mapper.Map<FooterService>(dto);
                await _dbContext.FooterServices.AddAsync(footerService);
                await _dbContext.SaveChangesAsync();

                dto.Id = footerService.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<FooterServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterServiceDTO>> Update(FooterServiceDTO dto)
        {
            try
            {
                var footerService = await _dbContext.FooterServices
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (footerService == null)
                {
                    return Error<FooterServiceDTO>("Footer service not found", HttpStatusCode.NotFound);
                }

                footerService = _mapper.Map(dto, footerService);
                _dbContext.Update(footerService);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<FooterServiceDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterServiceDTO>> Delete(int id)
        {
            try
            {
                var footerService = await _dbContext.FooterServices
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (footerService == null)
                {
                    return Error<FooterServiceDTO>("Footer service not found", HttpStatusCode.NotFound);
                }

                _dbContext.FooterServices.Remove(footerService);
                await _dbContext.SaveChangesAsync();

                return Success<FooterServiceDTO>();
            }
            catch (Exception ex)
            {
                return Error<FooterServiceDTO>(ex);
            }
        }
    }

}
