using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoureLearner.WebApi.Models;
using CoureLearner.WebApi;
using CourseLearner.Models;
using CourseLearner.ViewModels;

namespace CourseLearner.Controllers
{
    public class CourseController : Controller
    {
        private CourseLearnerContext db = new CourseLearnerContext();
        private UsersContext dbUser = new UsersContext();
        // GET: /Course/

        public ActionResult Index()
        {

            return View(db.Courses.ToList());
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // GET: /Course/Create
        [Authorize]
        public ActionResult Create()
        {
            var categoryList = db.Course_Category.ToList();
            ViewBag.categoryList = categoryList;
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(FormCollection objForm, Course course, HttpPostedFileBase file)
        {
            var courseName = string.Empty;
            var loggedUserID = string.Empty;
            var guidFileName = string.Empty;
            if (ModelState.IsValid)
            {
                loggedUserID = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();

                if (!string.IsNullOrEmpty(objForm["coursecategory"]))
                {
                    courseName = objForm["coursecategory"];
                    CourseCategory objcategory = new CourseCategory();
                    objcategory.CategoryName = courseName;
                    db.Course_Category.Add(objcategory);
                    db.SaveChanges();

                    course.CourseCategoryId = objcategory;
                    course.Userid = Convert.ToInt32(loggedUserID);
                }
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileInfoObj = new FileInfo(file.FileName);
                        var guid = Guid.NewGuid().ToString().ToUpper();
                        guidFileName = guid + fileInfoObj.Extension.ToLower();
                        var path = Path.Combine(Server.MapPath("~/UploadedImages/"), guidFileName);
                        if (!Directory.Exists(Server.MapPath("~/UploadedImages/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/UploadedImages/"));
                        }
                        file.SaveAs(path);
                    }
                }

                //temporary code
                course.CourseImgURl = guidFileName;
                db.Courses.Add(course);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(course);
        }

        //
        // GET: /Course/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult View(int courseId)
        {
            var detail = db.Courses.Include(e => e.CourseCategoryId).FirstOrDefault(e => e.CourseID == courseId);
            var VideoList = db.CourseAssets.Where(e => e.CourseID == courseId).ToList();
            ViewBag.UserName = "User";
            ViewBag.videoList = VideoList;

            return View(detail);


        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Preview(int courseID,int id)
        {
            var objViewModel = new previewVideoViewModel();
            var courseDetails = db.Courses.FirstOrDefault(e => e.CourseID==courseID);
            var courseAssetDetails = db.CourseAssets.FirstOrDefault(e=>e.AssetID==id);
            if(courseDetails!=null && courseAssetDetails !=null)
            {
                objViewModel.AssetGuid = courseAssetDetails.AssetGUid;
                objViewModel.FileName = courseAssetDetails.AssetFileName;
                objViewModel.topicName = courseAssetDetails.AssetHeaderName;
                objViewModel.CourseName = courseDetails.CourseName;
                objViewModel.CreatedTime = courseAssetDetails.CreatedTimeStamp;
                //objViewModel.AuthorName = db.CourseUser.First(e => e.UserID == courseDetails.Userid).FirstName;
                objViewModel.AuthorName = "User";
            }
            else
            {
                objViewModel.FileName = "file not found";
            }
            
            return PartialView("_previewVideo", objViewModel);
        }
    }
}