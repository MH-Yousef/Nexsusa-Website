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

namespace Services.WhoWeAreServices
{
    public class WhoWeAreService : BaseService, IWhoWeAreService
    {
        public WhoWeAreService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<WhoWeAreDTO>>> GetAll()
        {
            try
            {
                var whoWeAreList = await _dbContext.WhoWeAres
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (whoWeAreList == null || !whoWeAreList.Any())
                {
                    return Error<List<WhoWeAreDTO>>("No records found", HttpStatusCode.NotFound);
                }

                var whoWeAreDtos = _mapper.Map<List<WhoWeAreDTO>>(whoWeAreList);
                return Success(whoWeAreDtos);
            }
            catch (Exception ex)
            {
                return Error<List<WhoWeAreDTO>>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> GetById(int id)
        {
            try
            {
                var whoWeAre = await _dbContext.WhoWeAres
                    .Include(x => x.WhoWeAreItem)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (whoWeAre == null)
                {
                    return Error<WhoWeAreDTO>("Record not found", HttpStatusCode.NotFound);
                }

                var whoWeAreDto = _mapper.Map<WhoWeAreDTO>(whoWeAre);
                return Success(whoWeAreDto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> Create(WhoWeAreDTO dto)
        {
            try
            {
                var whoWeAre = _mapper.Map<WhoWeAre>(dto);
                await _dbContext.WhoWeAres.AddAsync(whoWeAre);
                await _dbContext.SaveChangesAsync();

                dto.Id = whoWeAre.Id;  // Return the generated Id
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> Update(WhoWeAreDTO dto)
        {
            try
            {
                var whoWeAre = await _dbContext.WhoWeAres
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (whoWeAre == null)
                {
                    return Error<WhoWeAreDTO>("Record not found", HttpStatusCode.NotFound);
                }

                whoWeAre = _mapper.Map(dto, whoWeAre);
                _dbContext.WhoWeAres.Update(whoWeAre);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }

        public async Task<ResponseResult<WhoWeAreDTO>> Delete(int id)
        {
            try
            {
                var whoWeAre = await _dbContext.WhoWeAres
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (whoWeAre == null)
                {
                    return Error<WhoWeAreDTO>("Record not found", HttpStatusCode.NotFound);
                }

                _dbContext.WhoWeAres.Remove(whoWeAre);
                await _dbContext.SaveChangesAsync();

                return Success<WhoWeAreDTO>();
            }
            catch (Exception ex)
            {
                return Error<WhoWeAreDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<WhoWeAreItemDTO>>> GetItemsByWhoWeAreId(int whoWeAreId)
        {
            try
            {
                var items = await _dbContext.WhoWeAreItems
                    .Where(x => x.WhoWeAreId == whoWeAreId && !x.IsDeleted)
                    .ToListAsync();

                if (items == null || !items.Any())
                {
                    return Error<List<WhoWeAreItemDTO>>("No items found", HttpStatusCode.NotFound);
                }

                var itemsDto = _mapper.Map<List<WhoWeAreItemDTO>>(items);
                return Success(itemsDto);
            }
            catch (Exception ex)
            {
                return Error<List<WhoWeAreItemDTO>>(ex);
            }
        }
    }

}
