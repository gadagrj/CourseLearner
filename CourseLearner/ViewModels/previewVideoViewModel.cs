using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseLearner.ViewModels
{
    public class previewVideoViewModel
    {
        public string AuthorName { get; set; }
        public string CourseName { get; set; }
        public DateTime CreatedTime { get; set; }
        public string AssetGuid { get; set; }
        public string thumbimgUrl{ get; set; }
        public string topicName { get; set; }
        public string FileName { get; set; }
    }
}