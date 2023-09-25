using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.ViewModel
{
    public class TrackViewModel 
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int TimeInMinute { get; set; }
        public string TrackUrl { get; set; }
        [Required]
        public string OwnerName { get; set; }
        public CategoryViewModels Category { get; set; }
        public UserViewModel PublishedBy { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }
}
