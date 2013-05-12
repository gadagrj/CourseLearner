using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoureLearner.WebApi.Models
{
    public class CourseEnorlled
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollID { get; set; }

        public int CourseID { get; set; }
        public int userID { get; set; }

    }
}
