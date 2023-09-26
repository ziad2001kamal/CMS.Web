using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Users;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices) : base(userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetUserData(Pagination pagination, Core.Dtos.Query query)
        {
            var result = await _userServices.GetAll(pagination, query);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> AddFCM(string fcm)
        {
            await _userService.SetFCMToUser(userId, fcm);
            return Ok("Update Fcm User");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                await _userServices.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userServices.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto dto)
        {

            if (ModelState.IsValid)
            {
                await _userServices.Update(dto);
                return Ok(Result.EditSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _userServices.Delete(id);
            return Ok(Result.DeleteSuccessResult());

        }

    }
}
