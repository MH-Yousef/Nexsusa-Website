using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.RegularBlogsDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.RegularBlogsItemServices
{
    public class RegularBlogsItemService : BaseService, IRegularBlogsItemService
    {
        public RegularBlogsItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<RegularBlogsItemDTO>>> Get()
        {
            try
            {
                var items = await _dbContext.RegularBlogsItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (items == null || !items.Any())
                {
                    return Error<List<RegularBlogsItemDTO>>("No blog items found", HttpStatusCode.NotFound);
                }

                var itemDtos = _mapper.Map<List<RegularBlogsItemDTO>>(items);
                return Success(itemDtos);
            }
            catch (Exception ex)
            {
                return Error<List<RegularBlogsItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsItemDTO>> GetById(int id)
        {
            try
            {
                var item = await _dbContext.RegularBlogsItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (item == null)
                {
                    return Error<RegularBlogsItemDTO>("Blog item not found", HttpStatusCode.NotFound);
                }

                var itemDto = _mapper.Map<RegularBlogsItemDTO>(item);
                return Success(itemDto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsItemDTO>> Create(RegularBlogsItemDTO dto)
        {
            try
            {
                var blogItem = _mapper.Map<RegularBlogsItem>(dto);
                await _dbContext.RegularBlogsItems.AddAsync(blogItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = blogItem.Id;  // Ensure the DTO gets the newly assigned Id
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsItemDTO>> Update(RegularBlogsItemDTO dto)
        {
            try
            {
                var blogItem = await _dbContext.RegularBlogsItems
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (blogItem == null)
                {
                    return Error<RegularBlogsItemDTO>("Blog item not found", HttpStatusCode.NotFound);
                }

                blogItem = _mapper.Map(dto, blogItem);  // Update entity with new data
                _dbContext.Update(blogItem);

                await _dbContext.SaveChangesAsync();
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsItemDTO>> Delete(int id)
        {
            try
            {
                var blogItem = await _dbContext.RegularBlogsItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (blogItem == null)
                {
                    return Error<RegularBlogsItemDTO>("Blog item not found", HttpStatusCode.NotFound);
                }

                _dbContext.RegularBlogsItems.Remove(blogItem);
                await _dbContext.SaveChangesAsync();

                return Success<RegularBlogsItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsItemDTO>(ex);
            }
        }
    }

}
