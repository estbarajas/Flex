using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class CustomerProgress
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Progress Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime ProgressDate { get; set; }

        [Display(Name = "Current Weight")]
        public string CurrentWeight { get; set; }

        [Display(Name = "Weight Goal")]
        public string WeightGoal { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}