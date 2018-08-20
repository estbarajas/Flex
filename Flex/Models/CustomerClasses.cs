using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class CustomerClasses
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public Classes Class { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<Classes> Classes { get; set; }
    }
}