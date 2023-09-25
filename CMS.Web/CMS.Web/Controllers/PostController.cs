using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Data.Migrations;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Categorys;
using CMS.Infrastructure.Services.Posts;
using CMS.Infrastructure.Services.Tracks;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly ICategoryServices _categoryService;
        private readonly IUserService _userService;


        public PostController(IPostService PostService, ICategoryServices categoryservices, IUserService userService) : base(userService)
        {
            _postService = PostService;
            _categoryService = categoryservices;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetPostData(Pagination pagination, Query query)
        {
            var result = await _postService.GetAll(pagination, query);
            return Json(result);
        }
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _postService.Getlog(Id);
            return View(logs);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            ViewData["authores"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
         
            if (ModelState.IsValid)
            {
                await _postService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var track = await _postService.Get(Id);
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            ViewData["authores"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
            return View(track);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(dto);
                ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
                ViewData["authores"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
                return Ok(Result.EditSuccessResult());
            }
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _postService.Delete(Id);
            return Ok(Result.DeleteSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _postService.UpdateStatus(id,status);
            return Ok(Result.EditStataesSuccessResult());
        }
      
    }
}
