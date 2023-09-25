using CMS.Core.Dtos;
using CMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Advertisements
{
    public interface IAdvertisementService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query quere);
        Task<int> Delete(int id);
        Task<int> Create(CreateAdvertisementDto dto);
        Task<List<UserViewModel>> GetAdvertisementOwners();
        Task<UpdateAdvertisementDto> Get(int Id);
        Task<int> Update(UpdateAdvertisementDto dto);

    }
}
