﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class About
    {
        public Guid Id { get; set; }
        [Required]
        public string  Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
