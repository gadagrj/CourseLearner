using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoureLearner.WebApi.Models
{

    public class Course
    {
        private DateTime _dtTimestampl;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        
        public int Userid { get; set; }
        
        [Required(ErrorMessage = "Please enter the course name")]
        [Display(Name = "Course Name")]
        [StringLength(200, ErrorMessage = "Please enter the name more than 5 words", MinimumLength = 5)]
        public string CourseName { get; set; }
        
        public string CourseImgURl { get; set; }
        
        [Required(ErrorMessage = "Please enter the course description")]
        [StringLength(1200, ErrorMessage = "Please enter description for course more than 10 wordss", MinimumLength = 10)]
        public string CourseDescrption { get; set; }
        
        public CourseCategory CourseCategoryId { get; set; }
        
        [Display(Name = "Activation Date")]
        public DateTime ActivationTime { get; set; }
        
        public DateTime CreatedTimeStamp
        {
            get { return dateModified; }
            set { dateModified = value; }
        }
        private DateTime dateModified = DateTime.UtcNow;
    }
}
