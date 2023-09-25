using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModel;
using CMS.Data;
using CMS.Data.Migrations;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Advertisements
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public AdvertisementService(CMSDbContext db, IMapper mapper, IUserService userService, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
        }
    

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).AsQueryable();
            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var advertisement = _mapper.Map<List<AdvertisementViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = advertisement,
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

        public async Task<List<UserViewModel>> GetAdvertisementOwners()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<int> Create(CreateAdvertisementDto dto)
        {

            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var advertisement = _mapper.Map<Advertisement>(dto);
            if (dto.Image != null)
            {
                try
                {
                     advertisement.ImageUrl = await _fileService.SaveFile(dto.Image, "Images");

                }
                catch (Exception ex)
                {

                    throw;
                }
            }


            await _db.Advertisements.AddAsync(advertisement);
            await _db.SaveChangesAsync();

            if (advertisement.OwnerId == null)
            {
                var userId = await _userService.Create(dto.Owner);
                advertisement.OwnerId = userId;

                _db.Advertisements.Update(advertisement);
                await _db.SaveChangesAsync();

            }

            return advertisement.Id;
        }

        public async Task<int> Update(UpdateAdvertisementDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();

            }
            var updatedadvertisement = _mapper.Map(dto, advertisement);
            if (dto.Image != null)
            {
              
                    advertisement.ImageUrl = await _fileService.SaveFile(dto.Image, "Images");

            }

                _db.Advertisements.Update(updatedadvertisement);
            await _db.SaveChangesAsync();
            return updatedadvertisement.Id;

        }


        public async Task<int> Delete(int id)
        {
            var advertisemen = await _db.Advertisements.SingleOrDefaultAsync(u => u.Id == id && !u.IsDelete);
            if (advertisemen == null)
            {
                throw new EntityNotFoundException();
            }
            advertisemen.IsDelete = true;
            _db.Advertisements.Update(advertisemen);
            await _db.SaveChangesAsync();
            return advertisemen.Id;
        }
        public async Task<UpdateAdvertisementDto> Get(int Id)
        {
            var addvertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (addvertisement == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateAdvertisementDto>(addvertisement);
        }
    }
}