using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class Classes
    {
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        public string Name { get; set; }

        [Display(Name = "Day of Week")]
        public string DayOfWeek { get; set; }

        [Display(Name = "Class Time")]
        public string Time { get; set; }
    }
}