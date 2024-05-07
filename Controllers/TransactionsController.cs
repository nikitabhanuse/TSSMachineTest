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
        public ActionResult SaveTransaction(TransactionModel model)
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

        public ActionResult GetTransactionList()
        {
            try
            {
                return Json(new { model = new TransactionModel().GetTransactionList() }, JsonRequestBehavior.AllowGet);
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
    }
}
