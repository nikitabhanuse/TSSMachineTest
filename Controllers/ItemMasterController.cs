using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSSMachineTest.Models;

namespace TSSMachineTest.Controllers
{
    public class ItemMasterController : Controller
    {
        // GET: ItemMaster
        public ActionResult itemIndex()
        {
            return View();
        }

        public ActionResult SaveItem(ItemMasterModel model)
        {
            try
            {

                return Json(new { Message = (new ItemMasterModel().SaveItem(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ItemList()
        {
            try
            {

                return Json(new { model = (new ItemMasterModel().ItemList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteItem(int Item_id)
        {
            try
            {

                return Json(new { Message = (new ItemMasterModel().DeleteItem(Item_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditItem(int Item_id)
        {
            try
            {

                return Json(new { model = (new ItemMasterModel().EditItem(Item_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}
    