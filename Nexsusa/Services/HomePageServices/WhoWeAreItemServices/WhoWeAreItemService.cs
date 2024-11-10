using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.WhoWeAreDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.WhoWeAreItemServices
{
    public class WhoWeAreItemService : BaseService, IWhoWeAreItemService
    {
        public WhoWeAreItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WhoWeAreItemDTO>>> GetAll()
        {
            try
            {
                var items = await _dbContext.WhoWeAreItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (items == null || !items.Any())
                {
                    return Error<List<WhoWeAreItemDTO>>("No records found", HttpStatusCode.NotFound);
                }

                var itemsDto = _mapper.Map<List<WhoWeAreItemDTO>>(items);
                return Success(itemsDto);
            }
            catch (Exception ex)
            {
                return Error<List<WhoWeAreItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreItemDTO>> GetById(int id)
        {
            try
            {
                var item = await _dbContext.WhoWeAreItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (item == null)
                {
                    return Error<WhoWeAreItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                var itemDto = _mapper.Map<WhoWeAreItemDTO>(item);
                return Success(itemDto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreItemDTO>> Create(WhoWeAreItemDTO dto)
        {
            try
            {
                var whoWeAreItem = _mapper.Map<WhoWeAreItem>(dto);
                await _dbContext.WhoWeAreItems.AddAsync(whoWeAreItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = whoWeAreItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreItemDTO>> Update(WhoWeAreItemDTO dto)
        {
            try
            {
                var whoWeAreItem = await _dbContext.WhoWeAreItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (whoWeAreItem == null)
                {
                    return Error<WhoWeAreItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                whoWeAreItem = _mapper.Map(dto, whoWeAreItem);
                _dbContext.WhoWeAreItems.Update(whoWeAreItem);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreItemDTO>> Delete(int id)
        {
            try
            {
                var whoWeAreItem = await _dbContext.WhoWeAreItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (whoWeAreItem == null)
                {
                    return Error<WhoWeAreItemDTO>("Record not found", HttpStatusCode.NotFound);
                }

                _dbContext.WhoWeAreItems.Remove(whoWeAreItem);
                await _dbContext.SaveChangesAsync();

                return Success<WhoWeAreItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreItemDTO>(ex);
            }
        }
    }

}
