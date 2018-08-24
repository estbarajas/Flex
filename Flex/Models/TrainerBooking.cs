using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class TrainerBooking
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "Time")]
        public string Time { get; set; }

        [Display(Name = "Accept Session")]
        public bool AcceptSession { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}