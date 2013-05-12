using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoureLearner.WebApi.Models;

namespace CoureLearner.WebApi
{
    public class CourseListController : ApiController
    {
        private CourseLearnerGenericRepo<Course> objCourseList;

        public CourseListController()
        {
            objCourseList = new CourseLearnerGenericRepo<Course>();
        }

        public IEnumerable<Course> Get()
        {
            var CourseList = objCourseList.GetAll();
            return CourseList;

        }

    }
}