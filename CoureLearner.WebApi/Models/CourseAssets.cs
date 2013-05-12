using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoureLearner.WebApi.Models
{

    public class CourseAssets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetID{ get; set; }

        public int CourseID { get; set; }
        public int userID { get; set; }
        
        [Required(ErrorMessage = "Please enter the Description for asset")]
        public string AssetHeaderName { get; set; }
        
        [Required(ErrorMessage = "Please enter the File name")]
        [Display(Name = "File Name")]
        [StringLength(200, ErrorMessage = "Please enter the name more than 5 words", MinimumLength = 5)]
        public string AssetFileName { get; set; }
        
        public string AssetImgUrl{ get; set; }
        public string AssetGUid { get; set; }
        public int AssetSize { get; set; }
        
        public DateTime CreatedTimeStamp
        {
            get { return dateModified; }
            set { dateModified = value; }
        }
        private DateTime dateModified = DateTime.UtcNow;
    }
}
