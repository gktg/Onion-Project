﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Domain.Entities
{
    public class ErrorLogger
    {
        [Key]
        public int LoggerId { get; set; }
        [Required]
        public string ErrorDetails { get; set; } = String.Empty;
        public DateTime LogDate { get; set; } = DateTime.Now;

        public int StatusCode { get; set; }
    }
}
