using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CoureLearner.WebApi.Models
{
    public class CourseCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(50,ErrorMessage = "Please enter the category name less than 50 words",MinimumLength = 3)]
        public string CategoryName { get; set; }
    }
}
