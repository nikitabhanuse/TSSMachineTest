using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSSMachineTest.Models;

namespace TSSMachineTest.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult DeptIndex()
        {
            return View();
        }
        public ActionResult SaveDept(DepartmentModel model)
        {
            try
            {

                return Json(new { Message = (new DepartmentModel().SaveDept(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DepartList(DepartmentModel model)
        {
            try
            {
                return Json(new { model = new DepartmentModel().DepartList() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteDepart(int Department_id)
        {
            try
            {
                return Json(new { Message = new DepartmentModel().DeleteDepart(Department_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditDepart(int Department_id)
        {
            try
            {
                return Json(new { model = new DepartmentModel().EditDepart(Department_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}