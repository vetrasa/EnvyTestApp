using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace EnvyTestApp.Models
{
    public class Members
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }      

        [Display(Name="Join Date")]
        public DateTime? DateJoin { get; set; }

        public DateTime? DateLastRankChange { get; set; }

        public string LastChangedBy { get; set; }
        
        [Display(Name="Rank")]
        public byte RankId { get; set; }
        [Display(Name="Previous Rank")]
        public byte? LastRankId { get; set; }

        [StringLength(280)]
        public string Notes { get; set; }

        [ForeignKey("LastRankId")]       
        public Rank PreviousRank { get; set; }

        [ForeignKey("RankId")]
        public Rank Rank { get; set; }






    }
}