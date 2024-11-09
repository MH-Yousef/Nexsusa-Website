using Data.Dtos.QuestionDTOs;
using Services._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.QuestionServices
{
    public interface IQuestionService
    {
        Task<ResponseResult<List<QuestionDTO>>> Get();
        Task<ResponseResult<QuestionDTO>> GetById(int id);
        Task<ResponseResult<QuestionDTO>> Create(QuestionDTO dto);
        Task<ResponseResult<QuestionDTO>> Update(QuestionDTO dto);
        Task<ResponseResult<QuestionDTO>> Delete(int id);
    }
}
