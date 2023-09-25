using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Data.Migrations;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Categorys;
using CMS.Infrastructure.Services.Tracks;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    public class TrackController : BaseController
    {
        private readonly ITrackService _trackService;
        private readonly ICategoryServices _categoryservices;


        public TrackController(ITrackService trackService , ICategoryServices categoryservices, IUserService userService) : base(userService) { 
            _trackService = trackService;
            _categoryservices = categoryservices;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetTrackData(Pagination pagination, Query query)
        {
            var result = await _trackService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"]=new SelectList(await _categoryservices.GetCategoryList(), "Id", "Name") ;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackDto dto)
        {
         
            if (ModelState.IsValid)
            {
                await _trackService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var track = await _trackService.Get(Id);
            ViewData["categories"] = new SelectList(await _categoryservices.GetCategoryList(), "Id", "Name");

            return View(track);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Update(dto);
                ViewData["categories"] = new SelectList(await _categoryservices.GetCategoryList(), "Id", "Name");

                return Ok(Result.EditSuccessResult());
            }
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _trackService.Delete(id);
            return Ok(Result.DeleteSuccessResult());
        }
    }
}
