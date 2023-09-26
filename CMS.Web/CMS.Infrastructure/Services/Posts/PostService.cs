﻿using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModel;
using CMS.Data;
using CMS.Data.Migrations;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Notifications;
using CMS.Infrastructure.Services.Users;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationservice;


        public PostService(CMSDbContext db, IMapper mapper, IUserService userService, IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
            _emailService = emailService;
        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Posts
                .Include(x => x.Attachments)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Where(x => !x.IsDelete).AsQueryable();
            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var posts = _mapper.Map<List<PostViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = posts,
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
        public async Task<int> Create(CreatePostDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            if (dto.Attachments != null)
            {
                foreach (var a in dto.Attachments)
                {
                    var postattachment = new PostAttachment();
                    postattachment.AttachmentUrl = await _fileService.SaveFile(a, "Images");
                    postattachment.PostId = post.Id;
                    await _db.PostAttachments.AddAsync(postattachment);
                    await _db.SaveChangesAsync();


                }
            }

            return post.Id;
        }

        public async Task<int> Update(UpdatePostDto dto)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (post == null)
            {
                throw new EntityNotFoundException();

            }
            var updatedpost = _mapper.Map(dto, post);
            _db.Posts.Update(updatedpost);
            await _db.SaveChangesAsync();

            if (dto.Attachments != null)
            {
                foreach (var a in dto.Attachments)
                {
                    var postattachment = new PostAttachment();
                    postattachment.AttachmentUrl = await _fileService.SaveFile(a, "Images");
                    postattachment.PostId = post.Id;
                    await _db.PostAttachments.AddAsync(postattachment);
                    await _db.SaveChangesAsync();


                }
            }
            return updatedpost.Id;


        }

        public async Task<List<ContentChangeLogViewModel>> Getlog(int id)
        {
            var change = await _db.ContentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Post).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(change);
        }
        public async Task<int> Delete(int id)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(u => u.Id == id && !u.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }
            post.IsDelete = true;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            return post.Id;
        }
        public async Task<UpdatePostDto> Get(int Id)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdatePostDto>(post);
        }

        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var post = await _db.Posts.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }
            var changeLog = new ContentChangeLog();
            changeLog.ContentId = post.Id;
            changeLog.Type = ContentType.Post;
            changeLog.Old = post.Status;
            changeLog.New = status;
            changeLog.ChangeAt = DateTime.Now;
            await _db.ContentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();
            post.Status = status;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            await _emailService.Send(post.Author.Email, "Update Post Status !", $" YOUR POST Now IS {status.ToString()}");
            _notificationservice.SendByFCM(post.Author.FCMToken, new NotificationDto()
            {
                Title = "Update Post",
                Body = "By Ahmed",
                Action = NotificationAction.General,
                ActionId = ""
            });
            return post.Id;
        }
    }
}