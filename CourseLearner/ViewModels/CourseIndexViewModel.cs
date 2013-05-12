using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseLearner.ViewModels
{
    public class CourseIndexViewModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string AuthorName { get; set; }
        public string ImgUrl { get; set; }
        public string CategoryName{get;set;}
        public DateTime ActivationDate { get; set; }
    }
}