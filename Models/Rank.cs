using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnvyTestApp.Models
{
    public class Rank
    {
        [Required]
        public byte RankId { get; set; }
        [Required]
        public string RankName { get; set; }
    }
}