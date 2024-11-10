using AutoMapper;
using Core.Domains;
using Data.Context;
using Data.Dtos.BaseDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;

namespace Services._GenericServices.Test
{
    public class TestGeneric<T, C> : BaseService, ITestGeneric<T> where T : BaseDTO<int>, new() where C : _Base<int>, new()
    {
        public TestGeneric(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<T>> Create(T dto)

        {
            try
            {
                //var defultKeyModel = dto.Keys.Where(x=> x.LangId == 1).FirstOrDefault();
                //var defultModel = dto.Keys.Where(x => x.DtoId == defultKeyModel.DtoId).FirstOrDefault();
                //var entity = _mapper.Map<C>(defultModel);
                //await _dbContext.AddAsync(entity);
                //entity = _dbContext.Set<C>().OrderByDescending(x => x.Id).FirstOrDefault();

                //foreach (var key in dto.Keys)

                //{

                //    await _dbContext.StringResources.AddAsync(new Core.Domains.Languages.StringResource

                //    {

                //        CreatedDate = DateTime.Now,

                //        IsDeleted = false,

                //        UpdatedDate = DateTime.Now,

                //        Key = key.Key,

                //        ResourceId = entity.Id,

                //        LangId = key.LangId,

                //        Value = key.Value,

                //    });

                //}



                await _dbContext.SaveChangesAsync();



                return Success<T>();

            }

            catch (Exception ex)

            {

                return Error<T>(ex);

            }

        }

        public async Task<ResponseResult<T>> Delete(int id)

        {

            try

            {

                var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)

                {

                    return Error<T>("Not Found...");

                }

                _dbContext.Remove(entity);

                await _dbContext.SaveChangesAsync();

                return Success<T>();

            }

            catch (Exception ex)

            {

                return Error<T>(ex);

            }

        }

        public async Task<ResponseResult<List<T>>> Get()

        {

            try

            {

                var entities = await _dbContext.Set<T>().ToListAsync();

                if (entities == null)

                {

                    return Error<List<T>>("Not Found...");

                }

                return Success(entities);

            }

            catch (Exception ex)

            {

                return Error<List<T>>(ex);

            }

        }

        public async Task<ResponseResult<T>> GetById(int id)

        {

            try

            {

                var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)

                {

                    return Error<T>("Not Found...");

                }

                return Success<T>(entity);

            }

            catch (Exception ex)

            {

                return Error<T>(ex);

            }

        }

        public async Task<ResponseResult<T>> Update(T entity)

        {

            try

            {

                _dbContext.Update(entity);

                await _dbContext.SaveChangesAsync();

                return Success<T>();

            }

            catch (Exception ex)

            {

                return Error<T>(ex);

            }


        }
    }
}

