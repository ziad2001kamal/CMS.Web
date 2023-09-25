using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Posts
{
    public interface IPostService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query quere);
        Task<int> Delete(int id);
        Task<int> Create(CreatePostDto dto);
        Task<int> Update(UpdatePostDto dto);
        Task<UpdatePostDto> Get(int Id);
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> Getlog(int id);


    }
}
