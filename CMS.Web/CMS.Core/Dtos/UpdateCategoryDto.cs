using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using CMS.Core.Enums;

namespace CMS.Core.Dtos
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم التصنيف")]
        public string Name { get; set; }
    }
}
