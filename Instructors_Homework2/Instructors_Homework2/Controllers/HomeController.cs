using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instructors_Homework2.Models;
using PagedList;

namespace Instructors_Homework2.Controllers
{
    public class HomeController : Controller
    {
        db_InstructorsEntities db = new db_InstructorsEntities();

        // GET: Home
        public ActionResult Index(string option, string search, int? pageNumber)
        {
            if (option == "Subject")
            {
                return View(db.instructorsTables.Where
                    (x => x.Subject == search || search == null).ToList().
                    ToPagedList(pageNumber ?? 1, 3));
            }

            else if (option == "Gender")
            {
                return View(db.instructorsTables.Where
                    (x => x.Gender == search || search == null).ToList().
                    ToPagedList(pageNumber ?? 1, 5));
            }

            else if (option == "InstructorName")
            {
                return View(db.instructorsTables.Where
                    (x => x.InstructorName == search || search == null).ToList().
                    ToPagedList(pageNumber ?? 1, 5));
            }

            else if (option =="Email")
            {
                return View(db.instructorsTables.Where
                    (x => x.Email == search || search == null).ToList().
                    ToPagedList(pageNumber ?? 1, 5));
            }

            else 
            {
                return View(db.instructorsTables.Where
                    (x => x.PhoneNum == search || search == null).ToList().
                    ToPagedList(pageNumber ?? 1, 5));
            }


            //List<instructorsTable> InstructorList = db.instructorsTables.ToList();

            //return View(InstructorList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpGet]
        public ActionResult EditUpdate(int id)
        {
            try
            {
                if (id != 0)
                {
                    instructorsTable edit_data = db.instructorsTables.SingleOrDefault(x => x.Id == id);
                    return PartialView("_EditUpdate", edit_data);
                }

                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditUpdate(instructorsTable edit_modified)
        {
            try
            {
                if (edit_modified != null)
                {
                    instructorsTable enr_update = db.instructorsTables.SingleOrDefault(x =>
                                                                        x.Id == edit_modified.Id);
                    enr_update.InstructorName = edit_modified.InstructorName;
                    enr_update.Gender = edit_modified.Gender;
                    enr_update.Subject = edit_modified.Subject;
                    enr_update.Email = edit_modified.Email;
                    enr_update.PhoneNum = edit_modified.PhoneNum;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("Index");
            }
            }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                instructorsTable prod_data = db.instructorsTables.SingleOrDefault(x => x.Id == id);
                if (prod_data != null)
                {
                    db.instructorsTables.Remove(prod_data);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.msg = "Some error ocurred.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Read(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            instructorsTable prd = db.instructorsTables.Find(id);
            if (prd == null)
            {
                return HttpNotFound();
            }
            return View(prd);
        }

    }
    }
