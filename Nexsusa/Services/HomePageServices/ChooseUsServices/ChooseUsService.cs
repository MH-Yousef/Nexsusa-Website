using AutoMapper;
using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ChooseUsDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using Services._GenericServices.Test;

namespace Services.HomePageServices.ChooseUsServices
{
    public class ChooseUsService : BaseService, IChooseUsService
    {
        private readonly ITestGeneric<List<ChooseUsDTO>> testGeneric;
        public ChooseUsService(AppDbContext context, IMapper mapper, ITestGeneric<List<ChooseUsDTO>> testGeneric) : base(context, mapper)
        {
            this.testGeneric = testGeneric;
        }
        public async Task<ResponseResult<List<ChooseUsDTO>>> Create(List<ChooseUsDTO> dtos)
        {
            try
            {
                var result = await testGeneric.Create(dtos);



                var defultLang = await _dbContext.Languages.FirstOrDefaultAsync(x => x.IsDefault);
                var dto = dtos.Where(x => x.LangId == defultLang?.Id).FirstOrDefault();
                var chooseUs = _mapper.Map<ChooseUsDTO, ChooseUs>(dto);
                await _dbContext.AddAsync(chooseUs);

                foreach (var item in dtos)
                {
                    _dbContext.StringResources.Add(new StringResource
                    {
                        Key = nameof(item.Title),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsDeleted = false,
                        LangId = item.LangId,
                        ResourceId = item.Id,
                        Value = item.Title,
                    });
                    _dbContext.StringResources.Add(new StringResource
                    {
                        Key = nameof(item.Description),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsDeleted = false,
                        LangId = item.LangId,
                        ResourceId = item.Id,
                        Value = item.Description,
                    });
                }


                await _dbContext.SaveChangesAsync();
                return Success(dtos);

            }
            catch (Exception ex)
            {

                return Error<List<ChooseUsDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ChooseUsDTO>> Delete(int id)
        {
            try
            {
                var chooseUs = await _dbContext.ChooseUs.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (chooseUs == null)
                {
                    return Error<ChooseUsDTO>("ChooseUs Not Found");
                }
                _dbContext.Remove(chooseUs);
                await _dbContext.SaveChangesAsync();
                return Success<ChooseUsDTO>();
            }
            catch (Exception ex)
            {

                return Error<ChooseUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<List<ChooseUsDTO>>> Get()
        {
            try
            {
                var chooseUs = await _dbContext.ChooseUs.AsNoTracking().ToListAsync();
                var dtos = chooseUs.Select(x => _mapper.Map<ChooseUs, ChooseUsDTO>(x));
                return Success(dtos.ToList());
            }
            catch (Exception ex)
            {

                return Error<List<ChooseUsDTO>>(ex);
            }


        }

        public async Task<ResponseResult<ChooseUsDTO>> GetById(int id)
        {
            try
            {
                var chooseUs = await _dbContext.ChooseUs
                 .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Id == id);

                if (chooseUs == null)
                {
                    return Error<ChooseUsDTO>("ChooseUs Not Found");
                }

                var dto = _mapper.Map<ChooseUs, ChooseUsDTO>(chooseUs);
                return Success(dto);
            }
            catch (Exception ex)
            {

                return Error<ChooseUsDTO>(ex);
            }

        }

        public async Task<ResponseResult<ChooseUsDTO>> Update(ChooseUsDTO dto)
        {
            try
            {
                var chooseUs = await _dbContext.ChooseUs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (chooseUs == null)
                {
                    return Error<ChooseUsDTO>("ChooseUs Not Found");
                }

                chooseUs = _mapper.Map<ChooseUsDTO, ChooseUs>(dto);

                _dbContext.ChooseUs.Update(chooseUs);
                await _dbContext.SaveChangesAsync();


                return Success<ChooseUsDTO>();
            }
            catch (Exception ex)
            {

                return Error<ChooseUsDTO>(ex);
            }

        }
    }
}
