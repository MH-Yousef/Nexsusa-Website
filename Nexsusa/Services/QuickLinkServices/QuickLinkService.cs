using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.QuickLinkDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.QuickLinkServices
{
    public class QuickLinkService : BaseService, IQuickLinkService
    {
        public QuickLinkService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<QuickLinkDTO>>> Get()
        {
            try
            {
                var quickLinks = await _dbContext.QuickLinks
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (quickLinks == null || !quickLinks.Any())
                {
                    return Error<List<QuickLinkDTO>>("QuickLinks not found", HttpStatusCode.NotFound);
                }

                var quickLinksDto = _mapper.Map<List<QuickLinkDTO>>(quickLinks);
                return Success(quickLinksDto);
            }
            catch (Exception ex)
            {
                return Error<List<QuickLinkDTO>>(ex);
            }
        }

        public async Task<ResponseResult<QuickLinkDTO>> GetById(int id)
        {
            try
            {
                var quickLink = await _dbContext.QuickLinks
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (quickLink == null)
                {
                    return Error<QuickLinkDTO>("QuickLink not found", HttpStatusCode.NotFound);
                }

                var quickLinkDto = _mapper.Map<QuickLinkDTO>(quickLink);
                return Success(quickLinkDto);
            }
            catch (Exception ex)
            {
                return Error<QuickLinkDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuickLinkDTO>> Create(QuickLinkDTO dto)
        {
            try
            {
                var quickLink = _mapper.Map<QuickLink>(dto);
                await _dbContext.QuickLinks.AddAsync(quickLink);
                await _dbContext.SaveChangesAsync();

                dto.Id = quickLink.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<QuickLinkDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuickLinkDTO>> Update(QuickLinkDTO dto)
        {
            try
            {
                var quickLink = await _dbContext.QuickLinks
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (quickLink == null)
                {
                    return Error<QuickLinkDTO>("QuickLink not found", HttpStatusCode.NotFound);
                }

                quickLink = _mapper.Map(dto, quickLink);
                _dbContext.Update(quickLink);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<QuickLinkDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuickLinkDTO>> Delete(int id)
        {
            try
            {
                var quickLink = await _dbContext.QuickLinks
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (quickLink == null)
                {
                    return Error<QuickLinkDTO>("QuickLink not found", HttpStatusCode.NotFound);
                }

                _dbContext.QuickLinks.Remove(quickLink);
                await _dbContext.SaveChangesAsync();

                return Success<QuickLinkDTO>();
            }
            catch (Exception ex)
            {
                return Error<QuickLinkDTO>(ex);
            }
        }
    }

}
