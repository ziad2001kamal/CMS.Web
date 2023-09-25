using AutoMapper;
using CMS.Data.Models;
using CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CMS.Core.Exceptions;
using CMS.Core.Dtos;
using System.IO;
using CMS.Infrastructure.Services;
using CMS.Core.ViewModel;

namespace CMS.Infrastructure.Services.Users
{
public interface IUserService
{
    Task<ResponseDto> GetAll(Pagination pagination,Query quere);
    Task<string> Create(CreateUserDto dto);
    Task<string> Update(UpdateUserDto dto);
    Task<string> Delete(string Id );
        Task<List<UserViewModel>> GetAuthorList();
        UserViewModel GetUserByUserName(string username);
        Task<UpdateUserDto> Get(string Id);


}
}