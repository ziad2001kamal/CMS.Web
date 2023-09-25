﻿using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.ViewModel
{
    public class ContentChangeLogViewModel
    {

        public DateTime ChangeAt { get; set; }
        public ContentStatus New { get; set; }
        public ContentStatus Old { get; set; }

        public ContentChangeLogViewModel()
        {
            ChangeAt = DateTime.Now;
        }
    }
}
