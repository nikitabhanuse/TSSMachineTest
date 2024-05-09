using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSSMachineTest.Models;

namespace TSSMachineTest.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult TransactionIndex()
        {
            return View();
        }
        public ActionResult IndexDetails(int Transaction_id)  
        {
            ViewBag.Transaction_id = Transaction_id;
            return View();
        }

        public ActionResult ReportIndex()
        {
            return View();
        }
        public ActionResult SaveTransaction(VMSaveTransation model)
        {
            try
            {
                return Json(new { Message = (new TransactionModel().SaveTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetAllDropDowns()
        {
            try
            {
                return Json(new { model = new TransactionModel().GetAllDropDowns() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransactionLst()
        {
            try
            {
                return Json(new { model = new TransactionModel().GetTransactionLst() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteTransaction(int Transaction_id)
        {
            try
            {
                var message = new TransactionModel().DeleteTransaction(Transaction_id);

                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditTransaction(int Transaction_id)
        {
            try
            {
                return Json(new { model = (new TransactionModel().EditTransaction(Transaction_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransactionReport(string Transaction_type, string ToDate, string FromDate)
        {
            try
            {
                return Json(new { model = (new TransactionModel().GetTransactionReport(Transaction_type, ToDate, FromDate)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
