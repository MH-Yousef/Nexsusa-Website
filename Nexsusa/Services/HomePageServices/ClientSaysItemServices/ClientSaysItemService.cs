using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.ClientSaysItemDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.ClientSaysItemServices
{
    public class ClientSaysItemService : BaseService, IClientSaysItemService
    {
        public ClientSaysItemService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<ResponseResult<List<ClientSaysItemDTO>>> Get()
        {
            try
            {
                var clientSaysItems = await _dbContext.ClientSaysItems
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (clientSaysItems == null || !clientSaysItems.Any())
                {
                    return Error<List<ClientSaysItemDTO>>("ClientSaysItems not found", HttpStatusCode.NotFound);
                }

                var clientSaysItemsDto = _mapper.Map<List<ClientSaysItemDTO>>(clientSaysItems);
                return Success(clientSaysItemsDto);
            }
            catch (Exception ex)
            {
                return Error<List<ClientSaysItemDTO>>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysItemDTO>> GetById(int id)
        {
            try
            {
                var clientSaysItem = await _dbContext.ClientSaysItems
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (clientSaysItem == null)
                {
                    return Error<ClientSaysItemDTO>("ClientSaysItem not found", HttpStatusCode.NotFound);
                }

                var clientSaysItemDto = _mapper.Map<ClientSaysItemDTO>(clientSaysItem);
                return Success(clientSaysItemDto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysItemDTO>> Create(ClientSaysItemDTO dto)
        {
            try
            {
                var clientSaysItem = _mapper.Map<ClientSaysItem>(dto);
                await _dbContext.ClientSaysItems.AddAsync(clientSaysItem);
                await _dbContext.SaveChangesAsync();

                dto.Id = clientSaysItem.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysItemDTO>> Update(ClientSaysItemDTO dto)
        {
            try
            {
                var clientSaysItem = await _dbContext.ClientSaysItems.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);
                if (clientSaysItem == null)
                {
                    return Error<ClientSaysItemDTO>("ClientSaysItem not found", HttpStatusCode.NotFound);
                }

                clientSaysItem = _mapper.Map(dto, clientSaysItem);
                _dbContext.Update(clientSaysItem);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<ClientSaysItemDTO>(ex);
            }
        }

        public async Task<ResponseResult<ClientSaysItemDTO>> Delete(int id)
        {
            try
            {
                var clientSaysItem = await _dbContext.ClientSaysItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (clientSaysItem == null)
                {
                    return Error<ClientSaysItemDTO>("ClientSaysItem not found", HttpStatusCode.NotFound);
                }

                _dbContext.ClientSaysItems.Remove(clientSaysItem);
                await _dbContext.SaveChangesAsync();

                return Success<ClientSaysItemDTO>();
            }
            catch (Exception ex)
            {
                return Error<ClientSaysItemDTO>(ex);
            }
        }
    }
}
