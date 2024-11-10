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

namespace Services.HomePageServices.RegularBlogsServices
{
    public class RegularBlogsService : BaseService, IRegularBlogsService
    {
        public RegularBlogsService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<RegularBlogsDTO>>> Get()
        {
            try
            {
                var blogs = await _dbContext.RegularBlogs
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (blogs == null || !blogs.Any())
                {
                    return Error<List<RegularBlogsDTO>>("Regular blogs not found", HttpStatusCode.NotFound);
                }

                var blogDtos = _mapper.Map<List<RegularBlogsDTO>>(blogs);
                return Success(blogDtos);
            }
            catch (Exception ex)
            {
                return Error<List<RegularBlogsDTO>>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsDTO>> GetById(int id)
        {
            try
            {
                var blog = await _dbContext.RegularBlogs
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (blog == null)
                {
                    return Error<RegularBlogsDTO>("Regular blog not found", HttpStatusCode.NotFound);
                }

                var blogDto = _mapper.Map<RegularBlogsDTO>(blog);
                return Success(blogDto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsDTO>> Create(RegularBlogsDTO dto)
        {
            try
            {
                var blog = _mapper.Map<RegularBlogs>(dto);
                await _dbContext.RegularBlogs.AddAsync(blog);
                await _dbContext.SaveChangesAsync();

                dto.Id = blog.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsDTO>> Update(RegularBlogsDTO dto)
        {
            try
            {
                var blog = await _dbContext.RegularBlogs
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (blog == null)
                {
                    return Error<RegularBlogsDTO>("Regular blog not found", HttpStatusCode.NotFound);
                }

                blog = _mapper.Map(dto, blog);
                _dbContext.Update(blog);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsDTO>(ex);
            }
        }

        public async Task<ResponseResult<RegularBlogsDTO>> Delete(int id)
        {
            try
            {
                var blog = await _dbContext.RegularBlogs
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (blog == null)
                {
                    return Error<RegularBlogsDTO>("Regular blog not found", HttpStatusCode.NotFound);
                }

                _dbContext.RegularBlogs.Remove(blog);
                await _dbContext.SaveChangesAsync();

                return Success<RegularBlogsDTO>();
            }
            catch (Exception ex)
            {
                return Error<RegularBlogsDTO>(ex);
            }
        }
    }

}
