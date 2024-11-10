using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ClientSaysDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ClientSaysServices
{
    public class ClientSaysService : BaseService, IClientSaysService
    {
        public ClientSaysService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<ResponseResult<List<ClientSaysDTO>>> Get()
        {
            try
            {
                var clientSaysList = await _dbContext.ClientSays
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (clientSaysList == null || !clientSaysList.Any())
                {
                    return Error<List<ClientSaysDTO>>("ClientSays not found", HttpStatusCode.NotFound);
                }

                var clientSaysDtoList = _mapper.Map<List<ClientSaysDTO>>(clientSaysList);
                return Success(clientSaysDtoList);
            }
            catch (Exception ex)
            {
                return Error<List<ClientSaysDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysDTO>> GetById(int id)
        {
            try
            {
                var clientSay = await _dbContext.ClientSays
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (clientSay == null)
                {
                    return Error<ClientSaysDTO>("ClientSays not found", HttpStatusCode.NotFound);
                }

                var clientSaysDto = _mapper.Map<ClientSaysDTO>(clientSay);
                return Success(clientSaysDto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysDTO>> Create(ClientSaysDTO dto)
        {
            try
            {
                var clientSay = _mapper.Map<ClientSays>(dto);
                await _dbContext.ClientSays.AddAsync(clientSay);
                await _dbContext.SaveChangesAsync();

                dto.Id = clientSay.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysDTO>> Update(ClientSaysDTO dto)
        {
            try
            {
                var clientSay = await _dbContext.ClientSays.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
                if (clientSay == null)
                {
                    return Error<ClientSaysDTO>("ClientSays not found", HttpStatusCode.NotFound);
                }

                clientSay = _mapper.Map(dto, clientSay);
                _dbContext.Update(clientSay);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysDTO>> Delete(int id)
        {
            try
            {
                var clientSay = await _dbContext.ClientSays.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (clientSay == null)
                {
                    return Error<ClientSaysDTO>("ClientSays not found", HttpStatusCode.NotFound);
                }

                _dbContext.ClientSays.Remove(clientSay);
                await _dbContext.SaveChangesAsync();

                return Success<ClientSaysDTO>();
            }
            catch (Exception ex)
            {
                return Error<ClientSaysDTO>(ex);
            }
        }
    }
}
