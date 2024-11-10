using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.FooterDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.FooterServices
{
    public class FooterService : BaseService, IFooterService
    {
        public FooterService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<ResponseResult<List<FooterDTO>>> Get()
        {
            try
            {
                var footers = await _dbContext.Footers
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.services)
                    .Include(x => x.QuickLinks)
                    .ToListAsync();

                if (footers == null || !footers.Any())
                {
                    return Error<List<FooterDTO>>("Footers not found", HttpStatusCode.NotFound);
                }

                var footerDtos = _mapper.Map<List<FooterDTO>>(footers);
                return Success(footerDtos);
            }
            catch (Exception ex)
            {
                return Error<List<FooterDTO>>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> GetById(int id)
        {
            try
            {
                var footer = await _dbContext.Footers
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .Include(x => x.services)
                    .Include(x => x.QuickLinks)
                    .FirstOrDefaultAsync();

                if (footer == null)
                {
                    return Error<FooterDTO>("Footer not found", HttpStatusCode.NotFound);
                }

                var footerDto = _mapper.Map<FooterDTO>(footer);
                return Success(footerDto);
            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> Create(FooterDTO dto)
        {
            try
            {
                var footer = _mapper.Map<Footer>(dto);
                await _dbContext.Footers.AddAsync(footer);
                await _dbContext.SaveChangesAsync();

                dto.Id = footer.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> Update(FooterDTO dto)
        {
            try
            {
                var footer = await _dbContext.Footers
                    .Where(x => x.Id == dto.Id && !x.IsDeleted)
                    .Include(x => x.services)
                    .Include(x => x.QuickLinks)
                    .FirstOrDefaultAsync();

                if (footer == null)
                {
                    return Error<FooterDTO>("Footer not found", HttpStatusCode.NotFound);
                }

                footer = _mapper.Map(dto, footer);
                _dbContext.Update(footer);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }

        public async Task<ResponseResult<FooterDTO>> Delete(int id)
        {
            try
            {
                var footer = await _dbContext.Footers
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (footer == null)
                {
                    return Error<FooterDTO>("Footer not found", HttpStatusCode.NotFound);
                }

                _dbContext.Footers.Remove(footer);
                await _dbContext.SaveChangesAsync();

                return Success<FooterDTO>();
            }
            catch (Exception ex)
            {
                return Error<FooterDTO>(ex);
            }
        }
    }
}
