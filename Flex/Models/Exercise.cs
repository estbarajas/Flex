using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flex.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Exercise Name")]
        public string ExerciseName { get; set; }

        [Display(Name = "Muscle Group")]
        public string MuscleGroup { get; set; }

        [Display(Name = "Equipment Needed")]
        public bool EquipmentNeeded { get; set; }
    }
}