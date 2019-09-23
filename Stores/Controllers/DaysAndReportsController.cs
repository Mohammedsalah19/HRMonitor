using CrystalDecisions.CrystalReports.Engine;
using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class DaysAndReportsController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security _security = new Security();


        #region Create days off

        public ActionResult Create()
        {

            bool result = _security.AsAdmin();
            if (result == false)
            {
                var model = new DaysOffWithEX();
                List<Category> res = filldrop();
                model.CategoryX = res;
                return View(model);
            }

            return RedirectToAction("HavntAccess", "Employee");
        }
        [HttpPost]
        public ActionResult Create(DaysOFF model,  string NumberOfMahdar, string Cat, int PublisherID, string Number, TimeSpan DateFrom, TimeSpan DateTo, DateTime reportDate,DateTime DayofDate)
        {
            model.Number = NumberOfMahdar;
            model.CategoryName = Cat;
            model.empID = PublisherID;
            model.Number = Number;
            model.DateFrom = DateFrom;
            model.DateTo = DateTo;
            model.reportDate = reportDate;
            model.DayofDate = DayofDate;

            _db.DaysOFF.Add(model);
            _db.SaveChanges();

            var models = new DaysOffWithEX();
            List<Category> res = filldrop();
            models.CategoryX = res;
            return View(models);
        }
        #endregion
 
        #region Reports

        public ActionResult Reports()
        {
            bool result = _security.AsUser();
            if (result == true)
            {
                string userID = Session["userID"].ToString();

                List<Category> cat = new List<Category>();
                var model = new DaysOffWithEX();

                var categorysOfSuper = _db.UserAccess.Where(f => f.userID.ToString() == userID).Select(f => f.cateID).ToList();

                foreach (var item in categorysOfSuper)
                {
                    var res = _db.Category.Where(f => f.id == item && f.Status == false).FirstOrDefault();
                    cat.Add(res);

                }

                model.CategoryX = cat;
                return View(model);
            }

            return RedirectToAction("HavntAccess", "Employee");
        }

        #endregion

        #region GetEmployee

        public JsonResult GetEmployee(string id)
        {
            List<Employee> cat = new List<Employee>();

            var s = _db.Category.Where(ss => ss.CategoryName == id).FirstOrDefault();
            var obj = _db.Employee.Where(p => p.CategoryName == s.id).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    Employee model = new Employee();
                    model.empID = item.empID;
                    model.name = item.name;
                    cat.Add(model);
                }
            }

            return Json(cat, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MangeReport

        #region MangeReport index

        public ActionResult MangeReport()
        {
            bool result = _security.AsAdmin();
            if (result == true)
            {
                string userID = Session["userID"].ToString();

                List<Category> cat = new List<Category>();
                var model = new DaysOffWithEX();

                var categorysOfSuper = _db.UserAccess.Where(f => f.userID.ToString() == userID).Select(f => f.cateID).ToList();

                foreach (var item in categorysOfSuper)
                {
                    var res = _db.Category.Where(f => f.id == item).FirstOrDefault();
                    cat.Add(res);

                }

                model.CategoryX = cat;
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");
        }
        #endregion


        #region partial  

        public PartialViewResult SearchManage(int PublisherID, string Cat, DateTime DateFrom, DateTime DateTo)
        {


            var model = _db.DaysOFF.Where(f => f.reportDate >= DateFrom && f.reportDate <= DateTo && f.CategoryName == Cat && f.empID == PublisherID).ToList();
            var WorkHours = _db.Employee.Where(f => f.empID == PublisherID).FirstOrDefault();

            if (WorkHours.workHour == "دوام 7 ساعات")
            {

                ViewBag.WorkHours = "7";
            }
            else
            {
                ViewBag.WorkHours = "6";


            }
            ViewBag.SearchModel = model;
            WorkHoursFun();
            return PartialView("_SeachMange", model);
        }



        #endregion


        #region DeleteDayOff

        public JsonResult DeleteDayOff(int ID)
        {
            bool result = false;
            try
            {
                DaysOFF model = _db.DaysOFF.Find(ID);
                _db.DaysOFF.Remove(model);
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

        #endregion


        //return number a value when emp change
        #region ReturnNumberValue

        public JsonResult ReturnNumberValue(int empID)
        {
            List<Employee> result = new List<Employee>();

            var model = _db.Employee.Where(i => i.empID == empID);
            // decimal result = model.Quantity;
            foreach (var item in model)
            {
                Employee mo = new Employee();
                mo.empID = item.empID;
                mo.name = item.name;
                mo.Number = item.Number;

                result.Add(mo);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ReturnDaysOFnumber

        public JsonResult ReturnDaysOFnumber()
        {
            var model = _db.DaysOFF.OrderByDescending(f => f.ID).FirstOrDefault();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
 
        #region partial view search ReportSearch

        public PartialViewResult ReportSearch(int PublisherID, string Cat, DateTime DateFrom, DateTime DateTo)
        {


            var model = _db.DaysOFF.Where(f => f.reportDate >= DateFrom && f.reportDate <= DateTo && f.CategoryName == Cat && f.empID == PublisherID).ToList();

            var WorkHours = _db.Employee.Where(f => f.empID == PublisherID).FirstOrDefault();

            if (WorkHours.workHour == "دوام 7 ساعات")
            {

                ViewBag.WorkHours = "7";
            }
            else
            {
                ViewBag.WorkHours = "6";


            }
            ViewBag.SearchModel = model;
            WorkHoursFun();

            return PartialView("_SearchReport", model);
        }

        #endregion


        #region return category list on /DaysAndReports/create

        public List<Category> filldrop()
        {
            string userID = Session["userID"].ToString();

            List<Category> cat = new List<Category>();
            var model = new DaysOffWithEX();
            var categorysOfSuper = _db.UserAccess.Where(f => f.userID.ToString() == userID).Select(f => f.cateID).ToList();

            foreach (var item in categorysOfSuper)
            {
                var res = _db.Category.Where(f => f.id == item && f.Status == false).FirstOrDefault();
                cat.Add(res);

            }
            return cat;

        }
        #endregion

        #region print

        public ActionResult Print(string Cat, int PublisherID, DateTime DateFrom, DateTime DateTo)
        {

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/EmployeeReport.rpt")));

            rd.SetDataSource(_db.Employee.Where(d => d.empID == PublisherID).Select(p => new
            {
                CatCategoryName = _db.Category.Where(d => d.id == p.CategoryName).Select(f => f.CategoryName).FirstOrDefault(),

                empID = p.name,
                Number = p.Number,
                Exp1 = DateFrom,
                Exp2 = DateTo,
                //    CatID = _db.DaysOFF.Where(f=>f.empID == PublisherID &&  f.reportDate >= DateFrom && f.reportDate <= DateTo).Select(f=>f.reportDate).ToList(),
                //  reportDate = _db.DaysOFF.Where(f=>f.empID == PublisherID &&  f.reportDate >= DateFrom && f.reportDate <= DateTo).Select(f=>f.reportDate).ToList(),
                DateFrom = _db.DaysOFF.Where(f => f.empID == PublisherID && f.reportDate >= DateFrom && f.reportDate <= DateTo).Select(f => f.DateFrom).ToList(),
                DateTo = _db.DaysOFF.Where(f => f.empID == PublisherID && f.reportDate >= DateFrom && f.reportDate <= DateTo).Select(f => f.DateTo).ToList(),

            }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", " تفاصيل القضيه.pdf");
        }
        #endregion

        
        #region ReturnDaysOFnumber
 

        void WorkHoursFun()
        {

            TimeSpan res = new TimeSpan();


            List<long> ss = new List<long>();
            foreach (var items in ViewBag.SearchModel)
            {
                res = items.DateTo.Subtract(items.DateFrom);
                ss.Add(res.Ticks);
            }


            TimeSpan ts = TimeSpan.FromTicks(ss.Sum());
        

            double RetuntoSecond = ss.Sum() / 100000000;
            int baseHour =int.Parse(ViewBag.WorkHours);

            double ReturnToHours = RetuntoSecond / 360;
            var Days = Math.Floor(ReturnToHours / baseHour);
            var Remainder = ReturnToHours % baseHour;
            var Hours = Math.Floor(Remainder);
            var Minutes = Math.Floor(60 * (Remainder - Hours));

            ViewBag.Days = Days;
            ViewBag.Hours = Hours;
            ViewBag.Min = Minutes;
        }
        #endregion
    }
}