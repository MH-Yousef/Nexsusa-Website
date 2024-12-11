using AutoMapper;
using Core.SocialLinks;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SocialLinkServices
{
    public class SocialLinkService : BaseService, ISocialLinkService
    {
        public SocialLinkService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<SocialLink>>> Get()
        {
            try
            {
                var socialLinks = await _dbContext.SocialLinks.AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();
                if (socialLinks == null || !socialLinks.Any())
                {
                    return Error<List<SocialLink>>("Social Links not found");
                }
                return Success(socialLinks);
            }
            catch (Exception ex)
            {
                return Error<List<SocialLink>>(ex);
            }
        }
        public async Task<ResponseResult<SocialLink>> GetById(int id)
        {
            try
            {
                var socialLink = await _dbContext.SocialLinks.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (socialLink == null)
                {
                    return Error<SocialLink>("Social Link not found");
                }
                return Success(socialLink);
            }
            catch (Exception ex)
            {
                return Error<SocialLink>(ex);
            }
        }
        public async Task<ResponseResult<SocialLink>> Create(SocialLink socialLink)
        {
            try
            {
                socialLink.CreatedDate = DateTime.Now;
                socialLink.UpdatedDate = DateTime.Now;
                socialLink.IsDeleted = false;
                await _dbContext.SocialLinks.AddAsync(socialLink);
                await _dbContext.SaveChangesAsync();
                return Success(socialLink);
            }
            catch (Exception ex)
            {
                return Error<SocialLink>(ex);
            }
        }
        public async Task<ResponseResult<SocialLink>> Update(SocialLink socialLink)
        {
            try
            {
                var entity = await _dbContext.SocialLinks.FirstOrDefaultAsync(x => x.Id == socialLink.Id);
                if (entity == null)
                {
                    return Error<SocialLink>("Social Link not found");
                }
                entity.Name = socialLink.Name;
                entity.Url = socialLink.Url;
                entity.IconClass = socialLink.IconClass;
                entity.UpdatedDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return Success(entity);
            }
            catch (Exception ex)
            {
                return Error<SocialLink>(ex);
            }
        }
        public async Task<ResponseResult<SocialLink>> Delete(int id)
        {
            try
            {
                var entity = await _dbContext.SocialLinks.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return Error<SocialLink>("Social Link not found");
                }
                entity.IsDeleted = true;
                entity.UpdatedDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return Success(entity);
            }
            catch (Exception ex)
            {
                return Error<SocialLink>(ex);
            }
        }
    }
}
