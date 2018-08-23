using DHTMLX.Scheduler;
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
        [DHXJson(Alias ="Id")]
        public int Id { get; set; }
        [DHXJson(Alias = "text")]
        public string Text { get; set; }
        [DHXJson(Alias = "start_date")]
        public DateTime Start_Date { get; set; }
        [DHXJson(Alias = "end_date")]
        public DateTime End_Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}