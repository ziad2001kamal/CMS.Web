using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using CMS.Core.Dtos;

namespace CMS.Core.Dtos
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان التسجيل")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الوقت ")]
        public int TimeInMinute { get; set; }
        [Display(Name = "ملف الصوت" )]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public IFormFile Track { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "صاحب الملف ")]
        public string OwnerName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "تصنيف التسجيل")]
        public int CategoryId { get; set; }
      

    }
}











