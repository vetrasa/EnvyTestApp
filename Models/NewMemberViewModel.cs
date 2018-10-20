using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvyTestApp.Models
{
    public class NewMemberViewModel
    {
        public IEnumerable<Rank> Ranks { get; set; }
        public Members Member { get; set; }
    }
}