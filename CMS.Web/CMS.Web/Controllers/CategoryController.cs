using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Categorys;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryServices _categoryservices;
        public CategoryController(ICategoryServices categoryservices, IUserService userService) : base(userService)
        {
            _categoryservices = categoryservices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetCategoryData(Pagination pagination, Core.Dtos.Query query)
        {
            var result = await _categoryservices.GetAll(pagination, query);
            return Json(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                  await _categoryservices.Create(dto);
            return Ok(Result.AddSuccessResult());
            }
           
            return View(dto);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var cat = await _categoryservices.Get(Id);
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryservices.Update(dto);
                return Ok(Result.EditSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var cat = await _categoryservices.Delete(Id);
            return Ok(Result.DeleteSuccessResult());
        }
    }
}
