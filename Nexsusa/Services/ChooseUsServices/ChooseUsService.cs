using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ChooseUsDTOs;
using Data.Dtos.LanguageDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChooseUsServices
{
    public class ChooseUsService : BaseService, IChooseUsService
    {
        public ChooseUsService(AppDbContext context,IMapper mapper):base(context,mapper)
        {
                
        }
        public async Task<ResponseResult<ChooseUsDTO>> Create(ChooseUsDTO dto)
        {
            try
            {
                var chooseUs=_mapper.Map<ChooseUsDTO,ChooseUs>(dto);
                await _dbContext.AddAsync(chooseUs);
                await _dbContext.SaveChangesAsync();
                return Success<ChooseUsDTO>(dto);

            }
            catch (Exception ex)
            {

                return Error<ChooseUsDTO>(ex);
            }
        }

        public async Task<ResponseResult<ChooseUsDTO>> Delete(int id)
        {
            try
            {
               var chooseUs=await _dbContext.ChooseUs.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
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
