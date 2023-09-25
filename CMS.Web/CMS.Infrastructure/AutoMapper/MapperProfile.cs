﻿using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.ViewModel;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Infrastructure.AutoMapper
{
	public class MapperProfile : Profile
	{
        public MapperProfile()
        {


            CreateMap<User, UserViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.Image, x => x.Ignore());


            CreateMap<Category, CategoryViewModels>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();


            CreateMap<Advertisement, AdvertisementViewModel>().ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("yyyy:MM:dd"))).ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy:MM:dd")));
            CreateMap<CreateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<UpdateAdvertisementDto, Advertisement>().ForMember(x => x.ImageUrl, x => x.Ignore()).ForMember(x => x.Owner, x => x.Ignore());
            CreateMap<Advertisement, UpdateAdvertisementDto>().ForMember(x => x.Image, x => x.Ignore());


            CreateMap<Track, TrackViewModel>();
            CreateMap<CreateTrackDto, Track>();
            CreateMap<UpdateTrackDto, Track>();
            CreateMap<Track, UpdateTrackDto>();

            CreateMap<Post, PostViewModel>().ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")))
                            .ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString()));
            CreateMap<CreatePostDto, Post>().ForMember(x => x.Attachments, x => x.Ignore());
            CreateMap<UpdatePostDto, Post>().ForMember(x => x.Attachments, x => x.Ignore());
            CreateMap<Post, UpdatePostDto>();
            CreateMap<PostAttachment, PostAttachmentViewModel>();
            CreateMap <ContentChangeLog, ContentChangeLogViewModel> ();







        }
    }
}
