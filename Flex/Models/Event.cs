using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}