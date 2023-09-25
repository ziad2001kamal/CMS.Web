using CMS.Core.Dtos;
using CMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Categorys
{
    public interface ICategoryServices
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query quere);
        Task<int> Create(CreateCategoryDto dto);
        Task<int> Update(UpdateCategoryDto dto);
        Task<int> Delete(int Id);
        Task<List<CategoryViewModels>> GetCategoryList();

        Task<UpdateCategoryDto> Get(int Id);
    }
}
