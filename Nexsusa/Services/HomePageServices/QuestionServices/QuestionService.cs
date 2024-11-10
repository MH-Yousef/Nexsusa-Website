using AutoMapper;
using Core.HomePage.HomePageItems;
using Data.Context;
using Data.Dtos.QuestionDTOs;
using Microsoft.EntityFrameworkCore;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.HomePageServices.QuestionServices
{
    public class QuestionService : BaseService, IQuestionService
    {
        public QuestionService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ResponseResult<List<QuestionDTO>>> Get()
        {
            try
            {
                var questions = await _dbContext.Questions
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                if (questions == null || !questions.Any())
                {
                    return Error<List<QuestionDTO>>("Questions not found", HttpStatusCode.NotFound);
                }

                var questionDtos = _mapper.Map<List<QuestionDTO>>(questions);
                return Success(questionDtos);
            }
            catch (Exception ex)
            {
                return Error<List<QuestionDTO>>(ex);
            }
        }

        public async Task<ResponseResult<QuestionDTO>> GetById(int id)
        {
            try
            {
                var question = await _dbContext.Questions
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (question == null)
                {
                    return Error<QuestionDTO>("Question not found", HttpStatusCode.NotFound);
                }

                var questionDto = _mapper.Map<QuestionDTO>(question);
                return Success(questionDto);
            }
            catch (Exception ex)
            {
                return Error<QuestionDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuestionDTO>> Create(QuestionDTO dto)
        {
            try
            {
                var question = _mapper.Map<Question>(dto);
                await _dbContext.Questions.AddAsync(question);
                await _dbContext.SaveChangesAsync();

                dto.Id = question.Id;
                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<QuestionDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuestionDTO>> Update(QuestionDTO dto)
        {
            try
            {
                var question = await _dbContext.Questions
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.IsDeleted);

                if (question == null)
                {
                    return Error<QuestionDTO>("Question not found", HttpStatusCode.NotFound);
                }

                question = _mapper.Map(dto, question);
                _dbContext.Update(question);

                await _dbContext.SaveChangesAsync();

                return Success(dto);
            }
            catch (Exception ex)
            {
                return Error<QuestionDTO>(ex);
            }
        }

        public async Task<ResponseResult<QuestionDTO>> Delete(int id)
        {
            try
            {
                var question = await _dbContext.Questions
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (question == null)
                {
                    return Error<QuestionDTO>("Question not found", HttpStatusCode.NotFound);
                }

                _dbContext.Questions.Remove(question);
                await _dbContext.SaveChangesAsync();

                return Success<QuestionDTO>();
            }
            catch (Exception ex)
            {
                return Error<QuestionDTO>(ex);
            }
        }
    }

}
