using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TSSMachineTest.Models;

namespace TSSMachineTest.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult VendorIndex()
        {
            return View();
        }

        public ActionResult SaveVendor(VendorModel model)
        {
            try
            {

                return Json(new { Message = (new VendorModel().SaveVendor(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VendorList()
        {
            try
            {

                return Json(new { model = (new VendorModel().VendorList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteVendor(int Vendor_id)
        {
            try
            {

                return Json(new { Message = (new VendorModel().DeleteVendor(Vendor_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message="Data is used you can not delete!" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditVendor(int Vendor_id)
        {
            try
            {

                return Json(new { Message = (new VendorModel().EditVendor(Vendor_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

    
