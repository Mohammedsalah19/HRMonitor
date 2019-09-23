using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class SupervisorController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security _security = new Security();

        #region Index

        // GET: Supervisor
        public ActionResult Index()
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {

                var model = new UserWithCate();
                model.userAccessX = _db.UserAccess.ToList();
                model.UserX = _db.Users.OrderByDescending(f => f.Id).ToList();
                model.CategoryX = _db.Category.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }
        #endregion


        #region NewSuperVisor

        [HttpGet]
        public ActionResult NewSuperVisor()
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                var model = new UserWithCate();
                model.userY = new User();
                model.CategoryX = _db.Category.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }

        [HttpPost]
        public ActionResult NewSuperVisor(User _user, UserAccess access)
        {
            _user.Password = "1616";
            _db.Users.Add(_user);

            _db.SaveChanges();

            return RedirectToAction("index");
        }

        #endregion


        #region Checkbok ajaxs


        public JsonResult savecheck(UserAccess access, string id)
        {
            var s = _db.Category.Where(ss => ss.CategoryName == id).FirstOrDefault();
            var lastid = _db.Users.OrderByDescending(d => d.Id).Select(f => f.Id).FirstOrDefault();

            access.userID = lastid + 1;
            access.cateID = s.id;
            _db.UserAccess.Add(access);
            _db.SaveChanges();



            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Removecheck(UserAccess access, string id)
        {
            var s = _db.Category.Where(ss => ss.CategoryName == id).FirstOrDefault();

            var Beforelastid = _db.Users.OrderByDescending(d => d.Id).Select(f => f.Id).FirstOrDefault();
            int last = Beforelastid + 1;
            var AccessRemove = _db.UserAccess.Where(f => f.userID == last && f.cateID == s.id).FirstOrDefault();

            _db.UserAccess.Remove(AccessRemove);
            _db.SaveChanges();



            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int? id)
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                Session["editEmp"] = id;


                if (id == null)
                {
                    return HttpNotFound();
                }

                var model = new UserWithCate();
                model.userY = _db.Users.Find(id);
                model.userAccessX = _db.UserAccess.Where(f => f.userID == id).ToList();
                model.CategoryX = _db.Category.ToList();

                if (model == null)
                {
                    return HttpNotFound();

                }
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }
        [HttpPost]
        public ActionResult Edit(User model, string name, string Number, string Password, bool? AsAdmin)
        {
            model.name = name;
            model.Number = Number;
            model.Password = Password;
            model.AsAdmin = AsAdmin.HasValue ? AsAdmin.Value : false;
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("index", "Supervisor");
        }

        #endregion

        #region edit checkbox ajaxs


        public JsonResult Editecheck(UserAccess access, string id)
        {
            int Editit = int.Parse(Session["editEmp"].ToString());

            var s = _db.Category.Where(ss => ss.CategoryName == id).FirstOrDefault();


            var AccessRecord = _db.UserAccess.Where(f => f.userID == Editit && f.cateID == s.id).FirstOrDefault();

            if (AccessRecord == null)
            {
                access.userID = Editit;
                access.cateID = s.id;
                _db.UserAccess.Add(access);
                _db.SaveChanges();

            }
            else
            {
                _db.UserAccess.Remove(AccessRecord);
                _db.SaveChanges();

            }



            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  Status of Supervisor 

        public JsonResult SupervisorStatus(int id)
        {
            var user = _db.Users.Where(ss => ss.Id == id).FirstOrDefault();
            if (user.Status == true)
            {
                user.Status = false;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                user.Status = true;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckNumberIfExist
        public JsonResult CheckNumberIfExist(string Number)
        {
            bool IsExist = false;

            var model = _db.Users.Where(i => i.Number == Number).FirstOrDefault();

            if (model != null)
            {
                IsExist = true;
            }
            return Json(IsExist, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult EditProfile()
        {
            int id = int.Parse(Session["userID"].ToString());
            var model = _db.Users.Find(id);
            return View(model);

        }



        [HttpPost]
        public ActionResult EditProfileSave(User model, string name, string Number, string Password, bool? AsAdmin)

         {
    
            try
            {
                //  model.Id = id;
                model.name = name;
                model.Number = Number;
                model.Password = Password;
                model.AsAdmin = AsAdmin.HasValue ? AsAdmin.Value : false;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("index", "home");
        }





        #region   DeleteSupervisor


        public JsonResult DeleteSupervisor(int Id)
        {
            bool result = false;
            try
            {
                User model = _db.Users.Find(Id);
                _db.Users.Remove(model);
                _db.SaveChanges();
                result = true;
            }

            catch (Exception ex)
            {
                throw ex;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}