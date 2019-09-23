using Stores.Models;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class CategoryController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security _security = new Security();

        #region index

        // GET: Category
        public ActionResult Index()
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                var model = _db.Category.OrderByDescending(f=>f.id).ToList();
                ViewBag.Emp = new SelectList(_db.Employee.GroupBy(f => f.name).Select(f => f.FirstOrDefault()), "empID", "name");

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }

        #endregion

        #region add cat

        public JsonResult SaveCatData(Category model)
        {
            // bool result = true;

            try
            {
                _db.Category.Add(model);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Edit category

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                ViewBag.Emp = new SelectList(_db.Employee.GroupBy(f => f.name).Select(f => f.FirstOrDefault()), "empID", "name");
                //  ViewBag.Emp = new SelectList(_db.Employee.ToList(), "empID", "name");

                if (id == null)
                {
                    return HttpNotFound();
                }

                var model = _db.Category.Find(id);
                if (model == null)
                {
                    return HttpNotFound();

                }
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }

        [HttpPost]
        public ActionResult Edit(Category model, string name)
        {
            // ViewBag.Emp = new SelectList(_db.Employee.GroupBy(f => f.name).Select(f => f.FirstOrDefault()), "empID", "name");

            //model.ManagerName =_db.Employee.Where(f=>f.empID.ToString()==name).Select(f=>f.name).FirstOrDefault();
            _db.Entry(model).State = EntityState.Modified;

            _db.SaveChanges();
            return RedirectToAction("index", "Category");

        }

        #endregion


        #region CheckCateIfExist
        public JsonResult CheckCateIfExist(string CategoryName)
        {
            bool IsExist = false;
            var model = _db.Category.Where(f => f.CategoryName == CategoryName).FirstOrDefault();

            if (model != null)
            {
                IsExist = true;
            }
            return Json(IsExist, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region MyRegion

        public JsonResult DeleteCate(int id)
        {
            bool result = false;
            try
            {
                Category model = _db.Category.Find(id);
                _db.Category.Remove(model);
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