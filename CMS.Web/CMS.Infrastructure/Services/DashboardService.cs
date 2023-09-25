using CMS.Core.Enums;
using CMS.Core.ViewModel;
using CMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly CMSDbContext _db;

        public DashboardService(CMSDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardViewModel> GetAll()
        {
            var data = new DashboardViewModel();
            data.NumberOfUser = await _db.Users.CountAsync(x => !x.IsDelete);
            data.NumberOfPost = await _db.Posts.CountAsync(x => !x.IsDelete);
            data.NumberOfTrack = await _db.Tracks.CountAsync(x => !x.IsDelete);
            data.NumberOfAdvertisement = await _db.Advertisements.CountAsync(x => !x.IsDelete);
            return data;

        }

        public async Task<List<PieChartViewModel>> GetUserTypeChart()
        {
            var data = new List<PieChartViewModel>();

            data.Add(new PieChartViewModel() {
                Key = "Administrator",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.Administrator),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Article Autor",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.ArticleAuthor),
                Color = "#FF9934",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Task Administrator",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.TrackAdministrator),
                Color = "#D1D9E2",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Administrator Owner",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner),
                Color = "#E6E9EC",
            });

            return data;
        }


        public async Task<List<PieChartViewModel>> GetContentTypeChart()
        {
            var data = new List<PieChartViewModel>();

            data.Add(new PieChartViewModel()
            {
                Key = "Track",
                Value = await _db.Tracks.CountAsync(x => !x.IsDelete ),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Post",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete ),
                Color = "#D1D9E2",
            });
           

            return data;
        }

        public async Task<List<PieChartViewModel>> GetContentByMonthChart()
        {
            var data = new List<PieChartViewModel>();          
            data.Add(new PieChartViewModel()
            {
                Key = "Jan",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month==1),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Feb",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month==2),
                Color = "#FF9934",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "March ",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month==3),
                Color = "#D1D9E2",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "April",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete &&  x.CreatedAt.Date.Month==4),
                Color = "#E6E9EC",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "May",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month==5),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "June",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 6),
                Color = "#FF9934",

            });
            data.Add(new PieChartViewModel()
            {
                Key = "July",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 7),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "August ",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 8),
                Color = "#E6E9EC",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "September ",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 9),
                Color = "#133A5E",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "October ",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 10),
                Color = "#FF9934",
            });
            data.Add(new PieChartViewModel()
            {
                Key = "November",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 11),
                Color = "#133A5E",

            });
            data.Add(new PieChartViewModel()
            {
                Key = "December",
                Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Date.Month == 12),
                Color = "#E6E9EC",

            });
            return data;
        }

    }
}
