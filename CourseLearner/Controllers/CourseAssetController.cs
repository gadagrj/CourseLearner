using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using CoureLearner.WebApi.Models;
using CoureLearner.WebApi;
using CourseLearner.Infrastructure;
using CourseLearner.ViewModels;

namespace CourseLearner.Controllers
{
    public class CourseAssetController : Controller
    {
        private CourseLearnerContext db = new CourseLearnerContext();

        //
        // GET: /CourseAsset/

        public ActionResult Index()
        {
            
            var courseList = db.Course_Category.AsEnumerable().Select(m => new SelectListItem { Text = m.CategoryName, Value = m.CategoryID.ToString() });
            
            ViewBag.coursList = courseList;
            return View(db.CourseAssets.ToList());
        }

        //
        // GET: /CourseAsset/Details/5

        public ActionResult Details(int id = 0)
        {
            CourseAssets courseassets = db.CourseAssets.Find(id);
            if (courseassets == null)
            {
                return HttpNotFound();
            }
            return View(courseassets);
        }

        //
        // GET: /CourseAsset/Create

        [System.Web.Mvc.Authorize]
        public ActionResult Create(int id=0)
        {
            
            return View();
        }

        //
        // POST: /CourseAsset/Create

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Create(CourseAssets courseassets, HttpPostedFileBase file,int Courseid)
        {
            var guidFileName = string.Empty;
            var videoSize = string.Empty;
            var loggedUserID = string.Empty;
            var tmpGuid = string.Empty;
            VideoEncoder objEncoder;
            if (ModelState.IsValid)
            {
                courseassets.CourseID = Courseid;
                try
                {
                    loggedUserID = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                    courseassets.userID = Convert.ToInt32(loggedUserID);
                }
                catch (Exception ex)
                {
                    courseassets.userID = 0;

                }
                
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        objEncoder=new VideoEncoder();
                        var fileInfoObj = new FileInfo(file.FileName);
                        tmpGuid = Guid.NewGuid().ToString().ToUpper();
                        guidFileName = tmpGuid + fileInfoObj.Extension.ToLower();
                        videoSize = file.ContentLength.ToString();
                        var path = Path.Combine(Server.MapPath("~/tempVideoFiles/"), guidFileName);
                        if (!Directory.Exists(Server.MapPath("~/tempVideoFiles/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/tempVideoFiles/"));
                        }
                        file.SaveAs(path);
                        if (!Directory.Exists(Server.MapPath("~/VideoFiles/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/VideoFiles/"));
                        }
                        objEncoder.EncodeVideo(guidFileName,file.FileName);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    courseassets.AssetGUid = tmpGuid;
                    courseassets.AssetSize = Convert.ToInt32(videoSize);
                }

                db.CourseAssets.Add(courseassets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseassets);
        }

        //
        // GET: /CourseAsset/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourseAssets courseassets = db.CourseAssets.Find(id);
            if (courseassets == null)
            {
                return HttpNotFound();
            }
            return View(courseassets);
        }

        //
        // POST: /CourseAsset/Edit/5

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CourseAssets courseassets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseassets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseassets);
        }

        //
        // GET: /CourseAsset/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourseAssets courseassets = db.CourseAssets.Find(id);
            if (courseassets == null)
            {
                return HttpNotFound();
            }
            return View(courseassets);
        }

        //
        // POST: /CourseAsset/Delete/5

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssets courseassets = db.CourseAssets.Find(id);
            db.CourseAssets.Remove(courseassets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Preview(string id, string courseID)
        {
            var courseDetails = db.Courses.Find(courseID);
            var courseAssetDetails = db.CourseAssets.Find(id);
            var objViewModel = new previewVideoViewModel();
            objViewModel.AssetGuid = courseAssetDetails.AssetGUid;
            objViewModel.FileName = courseAssetDetails.AssetFileName;
            objViewModel.topicName = courseAssetDetails.AssetHeaderName;
            objViewModel.CourseName = courseDetails.CourseName;
            objViewModel.CreatedTime = courseAssetDetails.CreatedTimeStamp;
            objViewModel.AuthorName = db.CourseUser.First(e => e.UserID == courseDetails.Userid).FirstName;

            return PartialView("_previewVideo", objViewModel);
        }
    }
}