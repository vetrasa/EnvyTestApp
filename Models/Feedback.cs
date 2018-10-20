using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnvyTestApp.Models
{
    public class Feedback
    {
        [Required]
        [StringLength(2000, ErrorMessage ="Character Max is 2,000 characters")]
        public string Description { get; set; }
    }
}