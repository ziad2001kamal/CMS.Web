﻿using CMS.Core.Enums;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CMS.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
		protected readonly IUserService _userService;
		protected string userType;
        protected string userId;

        public BaseController(IUserService userservise) {
            _userService = userservise;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if(User.Identity.IsAuthenticated) { 
            var userName= User.Identity.Name;

                var user=_userService.GetUserByUserName(userName);
                userId=user.Id;
				userType = user.UserType;
				ViewBag.fullName = user.FullName;
                ViewBag.image = user.ImageUrl;
				ViewBag.UserType=user.UserType.ToString();



			}
        }
    }
}
