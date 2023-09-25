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

namespace CMS.Infrastructure.Services.Tracks
{
    public class TrackService : ITrackService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public TrackService(CMSDbContext db, IMapper mapper, IUserService userService, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
        }
    

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Tracks.Where(x => !x.IsDelete).AsQueryable();
            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var track = _mapper.Map<List<TrackViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = track,
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

     
       

        public async Task<int> Create(CreateTrackDto dto)
        {
           var track = _mapper.Map<Track>(dto);

            if (dto.Track != null)
            {
                try
                {
                    track.TrackUrl = await _fileService.SaveFile(dto.Track, "Tracks");

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            await _db.Tracks.AddAsync(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }

        public async Task<int> Update(UpdateTrackDto dto)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (track == null)
            {
                throw new EntityNotFoundException();

            }
            var updatedtrack = _mapper.Map(dto, track);
            if (dto.Track != null)
            {
                try
                {
                    track.TrackUrl = await _fileService.SaveFile(dto.Track, "Tracks");

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            _db.Tracks.Update(updatedtrack);
            await _db.SaveChangesAsync();
            return updatedtrack.Id;

        }


        public async Task<int> Delete(int id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(u => u.Id == id && !u.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }
            track.IsDelete = true;
            _db.Tracks.Update(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }
        public async Task<UpdateTrackDto> Get(int Id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }    
            return _mapper.Map<UpdateTrackDto>(track);
        }
    }
}