using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.Exceptions;
using CMS.Core.ViewModel;
using CMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Data.Models;
using CMS.Core.Constants;
using CMS.Core.Enums;

namespace CMS.Infrastructure.Services.Users
{
    public class UserService: IUserService
    {

       
            private readonly CMSDbContext _db;
            private readonly IMapper _mapper;
            private readonly IFileService _fileService;
            private readonly UserManager<User> _userManager;
            private readonly IEmailService _emailService;



        public UserService(CMSDbContext db, IMapper mapper, UserManager<User> userManager, IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
        }
        public async Task<List<UserViewModel>> GetAuthorList()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == Core.Enums.UserType.ArticleAuthor).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
        public UserViewModel GetUserByUserName(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName== username && !x.IsDelete );
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
          
            return _mapper.Map<UserViewModel>(user); ;
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
            {
                var queryString = _db.Users.Where(x => !x.IsDelete && (x.FullName.Contains(query.GeneralSearch) || string.IsNullOrEmpty(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) ) ).AsQueryable();
                var dataCount = queryString.Count();
                var skipValue = pagination.GetSkipValue();
                var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
                var users = _mapper.Map<List<UserViewModel>>(dataList);
                var pages = pagination.GetPages(dataCount);
                var result = new ResponseDto
                {
                    data = users,
                    meta = new Meta
                    {
                        page = pagination.Page,
                        perpage = pagination.PerPage,
                        pages = pages,
                        total = dataCount,
                    }
                };
                return result;
            }
     
        public async Task<string> Create(CreateUserDto dto)
            {
                var emailOrphoneNumberIsExsit = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));
                if (emailOrphoneNumberIsExsit)
                {
                    throw new DuplicateEmailOrPhoneException();

                }
                var user = _mapper.Map<User>(dto);
                user.UserName = dto.Email;
                if (dto.Image != null)
                {
                    user.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
                }
                var password = GenratePassword();
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }
            }catch (Exception ex)
            {

            } 
                
                await _emailService.Send(user.Email, "New Acount !", $"Username is :{user.Email} and Password is :{password}");
                return user.Id;

            }

            public async Task<string> Update(UpdateUserDto dto)
            {
                var emailORphoneNumber = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber) && x.Id != dto.Id);
                if (emailORphoneNumber)
                {
                    throw new DuplicateEmailOrPhoneException();

                }
                var user = await _db.Users.FindAsync(dto.Id);
                var Updateuser = _mapper.Map<UpdateUserDto, User>(dto, user);
                if (dto.Image != null)
                {
                Updateuser.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
                }
                _db.Users.Update(Updateuser);
                await _db.SaveChangesAsync();
                return user.Id;

            }
            public async Task<string> Delete(string Id)
            {
                var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == Id && !u.IsDelete);
                if (user == null)
                {
                    throw new EntityNotFoundException();
                }
                user.IsDelete = true;
                _db.Users.Update(user);
            await _db.SaveChangesAsync();
             return user.Id;
            }
            public async Task<UpdateUserDto> Get(string id)
            {
                var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    throw new EntityNotFoundException();
                }

                return _mapper.Map<UpdateUserDto>(user);
            }
            private string GenratePassword()
            {
                return Guid.NewGuid().ToString().Substring(1, 8);
            }

        public async Task<string> SetFCMToUser(string userId,string fcmToken)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId && !x.IsDelete);
            if(user == null)
            {
                throw new EntityNotFoundException();
            }
            user.FCMToken= fcmToken;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return userId;
        }

    }
    }

