using CMS.Core.Dtos;
using CMS.Data.Migrations;
using CMS.Infrastructure.Services;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
	public class HomeController : BaseController
    {
		private IDashboardService _dashboardService;
        private readonly IUserService _userService;

        public HomeController(IDashboardService dashboardService, IUserService userService) : base(userService)
        {
			_dashboardService = dashboardService;
            _userService = userService;
		}
		public async Task<IActionResult> Index()
		{
			var data=await _dashboardService.GetAll();
			return View(data);
		}
		public async Task<IActionResult> GetChartData()
		{
           var data=await _dashboardService.GetUserTypeChart();
			return Ok(data);
        }
        public async Task<IActionResult> GetContentTypeChartData()
        {
            var data = await _dashboardService.GetContentTypeChart();
            return Ok(data);
        }
        public async Task<IActionResult> GetByMonthChart()
        {
            var data = await _dashboardService.GetContentByMonthChart();
            return Ok(data);
        }
        
    }
}
