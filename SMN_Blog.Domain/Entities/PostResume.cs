﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Entities
{
    [NotMapped]
    public class PostResume
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CountComments { get; set; }
    }
}
