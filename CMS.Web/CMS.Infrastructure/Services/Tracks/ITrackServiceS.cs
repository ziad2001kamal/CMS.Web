using CMS.Core.Dtos;
using CMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Tracks
{
    public interface ITrackService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query quere);
        Task<int> Delete(int id);
        Task<int> Create(CreateTrackDto dto);
        Task<int> Update(UpdateTrackDto dto);
        Task<UpdateTrackDto> Get(int Id);

      }
}
