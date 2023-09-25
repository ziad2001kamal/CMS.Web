using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.Exceptions;
using CMS.Core.ViewModel;
using CMS.Data;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Categorys
{
    public class CategoryService : ICategoryServices
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        //private readonly IFileService _fileService;
        //private readonly IEmailService _emailService;
        public CategoryService(CMSDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Categorys.Where(x => !x.IsDelete && (x.Name.Contains(query.GeneralSearch)||string.IsNullOrEmpty(query.GeneralSearch))).AsQueryable();
            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var categorys = _mapper.Map<List<CategoryViewModels>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = categorys,
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
        public async Task<List<CategoryViewModels>> GetCategoryList()
        {

            var Categories = await _db.Categorys.Where(x => !x.IsDelete).ToListAsync();
            var result = _mapper.Map<List<CategoryViewModels>>(Categories);
            return result;
        }
        public async Task<int> Create(CreateCategoryDto dto)
        {
            var emailOrphoneNumberIsExsit = _db.Categorys.Any(x => !x.IsDelete && (x.Name== dto.Name));
            if (emailOrphoneNumberIsExsit)
            {
                throw new DuplicateEmailOrPhoneException();

            }
            var category = _mapper.Map<Category>(dto);
            await _db.Categorys.AddAsync(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var category = await _db.Categorys.SingleOrDefaultAsync(x =>  !x.IsDelete && x.Id == dto.Id);
            if (category==null)
            {
                throw new EntityNotFoundException();

            }
            var updatedCategory = _mapper.Map<UpdateCategoryDto, Category>(dto, category);
            _db.Categorys.Update(updatedCategory);
            await _db.SaveChangesAsync();
            return updatedCategory.Id;

        }
        public async Task<int> Delete(int Id)
        {
            var category = await _db.Categorys.SingleOrDefaultAsync(u => u.Id == Id && !u.IsDelete);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }
            category.IsDelete = true;
            _db.Categorys.Update(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }
        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var category = await _db.Categorys.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateCategoryDto>(category);
        }
      
       

       
    }

}
