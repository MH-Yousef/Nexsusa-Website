using Core.SocialLinks;
using Services._Base;

namespace Services.SocialLinkServices
{
    public interface ISocialLinkService
    {
        Task<ResponseResult<SocialLink>> Create(SocialLink socialLink);
        Task<ResponseResult<SocialLink>> Delete(int id);
        Task<ResponseResult<List<SocialLink>>> Get();
        Task<ResponseResult<SocialLink>> GetById(int id);
        Task<ResponseResult<SocialLink>> Update(SocialLink socialLink);
    }
}
