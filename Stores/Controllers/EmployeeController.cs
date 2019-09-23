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
    public class EmployeeController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security _security = new Security();



        #region index

        // GET: Employee
        public ActionResult Index()
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                // table1.GroupBy(x => x.Text).Select(x => x.FirstOrDefault());
                ViewBag.Category = new SelectList(_db.Category.GroupBy(f => f.CategoryName).Select(f => f.FirstOrDefault()).Where(f => f.Status == false), "id", "CategoryName");
                var model = new DaysOffWithEX();
                model.EmpX = _db.Employee.OrderByDescending(f => f.empID).ToList();

                model.CategoryX = _db.Category.ToList();


                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }
        #endregion

        #region login

        public ActionResult login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login(User _users)
        {
            var model = _db.Users.Where(p => p.Number == _users.Number && p.Password == _users.Password && p.Status == true).FirstOrDefault();
            if (model != null)
            {
                Session["userName"] = model.name;
                Session["userID"] = model.Id;
                Session["flag"] = "true";

                Session.Timeout = 720;
                return RedirectToAction("Index", "Home");
            }
            Session["userName"] = null;
            ViewBag.ErroreLogin = "خطأ فى اسم المستخدم او كلمة المرور";
            return View();
        }

        #endregion


        #region add emp

        public JsonResult SaveEmpData(Employee model)
        {
            // bool result = true;

            try
            {
                _db.Employee.Add(model);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region edit


        [HttpGet]
        public ActionResult Edit(int? id)
        {

            bool result = _security.AsAdmin();
            if (result == true)
            {
                ViewBag.Category = new SelectList(_db.Category.GroupBy(f => f.CategoryName).Select(f => f.FirstOrDefault()).Where(f => f.Status == false), "id", "CategoryName");


                if (id == null)
                {
                    return HttpNotFound();
                }

                var model = _db.Employee.Find(id);
                if (model == null)
                {
                    return HttpNotFound();

                }
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }

        [HttpPost]
        public ActionResult Edit(Employee model , string Number)
        {
            int ss = int.Parse(Number);
            model.Number = ss.ToString();

            _db.Entry(model).State = EntityState.Modified;

            _db.SaveChanges();
            return RedirectToAction("index");

        }
        #endregion

        #region ReturnProName

        public JsonResult ReturnProName(int CategoryName)
        {
            var model = _db.Category.Where(id => id.id == CategoryName).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region logOff


        public ActionResult LogOff(User _users)
        {
            if (Session["userName"] != null)
            {
                Session.Clear();


            }
            return RedirectToAction("Login");
        }

        #endregion

        #region Havent access

        [HttpGet]
        public ActionResult HavntAccess()
        {

            return View();

        }
        #endregion


        #region CheckNumberIfExist
        public JsonResult CheckNumberIfExist(string Number)
        {
            bool IsExist = false;
            int ss = int.Parse(Number);
            string res = ss.ToString();
            var model = _db.Employee.Where(i => i.Number == res).FirstOrDefault();

            if (model != null)
            {
                IsExist = true;
            }
            return Json(IsExist, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region profile

        public ActionResult Profile()
        {
            int id = int.Parse(Session["userID"].ToString());
            var model = _db.Users.Find(id);
            return View(model);
        }


        #endregion



        #region   DeleteEmp


        public JsonResult DeleteEmp(int empID)
        {
            bool result = false;
            try
            {
                Employee model = _db.Employee.Find(empID);
                _db.Employee.Remove(model);
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