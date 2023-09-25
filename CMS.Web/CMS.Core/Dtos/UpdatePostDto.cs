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
    public class UpdatePostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الاعلان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "محتوى الخبر")]
        public string Body { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "محتوى الخبر")]
        public int TimeInMinute { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "وقت القراءة ")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المحرر")]
        public string AuthorId { get; set; }
        [Display(Name = "المرفقات")]
        public List<IFormFile> Attachments { get; set; }

    }
}











