using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CoureLearner.WebApi;
using CoureLearner.WebApi.Models;
using CourseLearner.ViewModels;
using System.Data.Entity;

namespace CourseLearner.Controllers
{
    public class HomeController : Controller
    {
        private CourseLearnerContext objContext;
        public HomeController()
        {
            objContext = new CourseLearnerContext();

        }

        public ActionResult Index()
        {
            var courseList = objContext.Courses.Include(e => e.CourseCategoryId).ToArray().Select(e => new CourseIndexViewModel
                                                                                            {
                                                                                                CourseID = e.CourseID,
                                                                                                CourseName = e.CourseName,
                                                                                                CourseDescription = e.CourseDescrption,
                                                                                               // CategoryName = e.CourseCategoryId.CategoryName,
                                                                                                AuthorName = "User",
                                                                                                ImgUrl = e.CourseImgURl,
                                                                                                ActivationDate = e.ActivationTime

                                                                                            }).ToList();
            return View(courseList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult FilterResult([FromUri]string searchTerm)
        {
            var courseList = objContext.Courses.Include(e => e.CourseCategoryId).ToArray().Where(e => e.CourseName.Contains(searchTerm)).Select(e => new CourseIndexViewModel
            {
                CourseID = e.CourseID,
                CourseName = e.CourseName,
                CourseDescription = e.CourseDescrption,
                CategoryName = e.CourseCategoryId.CategoryName,
                AuthorName = "asdasd",
                ImgUrl = e.CourseImgURl,
                ActivationDate = e.ActivationTime

            }).ToList();

            return PartialView("_CourseView", courseList);


        }

        public JsonResult GetCourseList(string term)
        {
            var query = objContext.Courses.Where(e => e.CourseName.Contains(term))
                                          .OrderBy(e => e.CourseName)
                                          .Select(e => new { label = e.CourseName, value = e.CourseID });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
