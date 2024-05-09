using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using TSSMachineTest.Data;
using TSSMachineTest.Models;

namespace TSSMachineTest.Models
{

    public class TransactionModel
    {
        //public int Transaction_id { get; set; }
        //public int Item_id { get; set; }
        //public string Item_name { get; private set; }
        //public string Transaction_date { get; set; }
        //public Nullable<int> Department_id { get; set; }
        //public Nullable<int> Vendor_id { get; set; }
        //public int Quantity { get; set; }
        //public string TransactionType { get; set; }
        //public string Vendor_name { get; private set; }
        //public string Department_name { get; private set; }


        public string SaveTransaction(VMSaveTransation model)
        {
            string Message = "Data Save Successfully.";
            TSSMachineTestEntities db = new TSSMachineTestEntities();

            if (model.Transaction_id == 0)
            {
                var editData = db.Item_master.Where(p => p.Item_id == model.Item_id).FirstOrDefault();
                if (model.Quantity > editData.Balance_quantity)
                {
                    Message = "Quantity is greater Balance Quantity";
                    return Message;
                }
                if (editData != null && model.TransactionType != null)
                {

                    editData.Balance_quantity = editData.Balance_quantity;
                    if (model.TransactionType == "I")
                    {
                        editData.Balance_quantity = editData.Balance_quantity - model.Quantity;
                    }
                    else
                    {
                        editData.Balance_quantity = editData.Balance_quantity + model.Quantity;

                    }
                    db.SaveChanges();
                };
                var save = new tblTransaction()
                {
                    Item_id = model.Item_id,
                    TransactionType = model.TransactionType,
                    Quantity = model.Quantity,
                    Transaction_date = Convert.ToDateTime(model.Transaction_date),
                    Transaction_id = model.Transaction_id,
                    Vendor_id = model.Vendor_id,
                    Department_id = model.Department_id
                };
                db.tblTransactions.Add(save);
                db.SaveChanges();
            }
            else
            {
                var editData = db.Item_master.Where(p => p.Item_id == model.Item_id).FirstOrDefault();
                var editTran = db.tblTransactions.Where(p => p.Item_id == model.Item_id).FirstOrDefault();
                if (editTran.Quantity != model.Quantity)
                {
                    editTran.TransactionType = model.TransactionType;
                    editData.Balance_quantity = editData.Balance_quantity;
                    editData.Balance_quantity = editData.Balance_quantity - editTran.Quantity;
                }
                if (editData != null && model.TransactionType != null)
                {
                    {

                        editData.Balance_quantity = editData.Balance_quantity;
                        if (model.TransactionType == "I")
                        {
                            editData.Balance_quantity = editData.Balance_quantity - model.Quantity;
                        }
                        else
                        {
                            editData.Balance_quantity = editData.Balance_quantity + model.Quantity;

                        }
                        db.SaveChanges();
                    }
                };
                var update = db.tblTransactions.Where(p => p.Transaction_id == model.Transaction_id).FirstOrDefault();
                if (update != null)
                {
                    update.Transaction_id = model.Transaction_id;
                    update.Item_id = model.Item_id;
                    update.TransactionType = model.TransactionType;
                    update.Quantity = model.Quantity;
                    update.Transaction_date = Convert.ToDateTime(model.Transaction_date);
                    update.Transaction_id = model.Transaction_id;
                    update.Vendor_id = model.Vendor_id;
                    update.Department_id = model.Department_id;
                }
                db.SaveChanges();
                Message = "Update  Data Successfully.";

            }
            return Message;
        }
        public VMtransactions GetAllDropDowns()
        {

            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<ItemMaster> lsitem = new List<ItemMaster>();
            List<department> lstdept = new List<department>();
            List<Vendor> lstvendor = new List<Vendor>();
            VMtransactions vMtransaction = new VMtransactions();
            lsitem = (from m in db.Item_master
                      select new ItemMaster
                      {
                          ItemID = m.Item_id,
                          ItemName = m.Item_name,
                          Balance_qty = m.Balance_quantity

                      }).ToList();

            lstdept = (from d in db.Department_mast
                       select new department
                       {
                           DepartmentID = d.Department_id,
                           DepartmentName = d.Department_name,
                       }).ToList();
            lstvendor = (from v in db.Vendor_mast
                         select new Vendor
                         {
                             VendorID = v.Vendor_id,
                             VendorName = v.Vendor_name,
                         }).ToList();

            vMtransaction.dept = lstdept;
            vMtransaction.vendors = lstvendor;
            vMtransaction.items = lsitem;

            return vMtransaction;
        }

        public List<VMTransactionDetails> GetTransactionLst()
        {

            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<VMTransactionDetails> lsttransation = new List<VMTransactionDetails>();
            lsttransation = (from T in db.tblTransactions
                             join I in db.Item_master on T.Item_id equals I.Item_id
                             join D in db.Department_mast on T.Department_id equals D.Department_id
                             join V in db.Vendor_mast on T.Vendor_id equals V.Vendor_id
                             select new VMTransactionDetails
                             {
                                 Transaction_id=T.Transaction_id,
                                 Vendor_id=V.Vendor_id,
                                 Department_id=D.Department_id,
                                 Item_name = I.Item_name,
                                 Department_name = D.Department_name,
                                 Quantity = T.Quantity,
                                 TransactionType= T.TransactionType,
                                 Transaction_date = T.Transaction_date.ToString(),
                                 Vendor_name = V.Vendor_name,
                             }).ToList();
            return lsttransation;
        }

        public string DeleteTransaction(int Transaction_id)
        {
            string Message = " Data delete successfully.";
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            var Data = db.tblTransactions.Where(p => p.Transaction_id == Transaction_id).FirstOrDefault();
            if (Data != null)
            {
                db.tblTransactions.Remove(Data);
                db.SaveChanges();
            }
            return Message;
        }
        public VMSaveTransation EditTransaction(int Transaction_id)
        {
            VMSaveTransation model = new VMSaveTransation();
            TSSMachineTestEntities db = new TSSMachineTestEntities();

            var editData = (from m in db.Item_master
                            join v in db.Vendor_mast on m.Item_id equals v.Vendor_id
                            join d in db.Department_mast on m.Item_id equals d.Department_id
                            join t in db.tblTransactions on m.Item_id equals t.Item_id

                            where t.Transaction_id == Transaction_id
                            select new
                            {
                                m.Item_id,
                                m.Item_name,
                                v.Vendor_name,
                                v.Vendor_id,
                                d.Department_id,
                                d.Department_name,
                                t.Transaction_date,
                                t.Transaction_id,
                                t.Quantity,
                                t.TransactionType
                            }).FirstOrDefault();

            if (editData != null)
            {
                model.Transaction_id = editData.Transaction_id;
                model.Item_id = editData.Item_id;
                model.Item_name = editData.Item_name;
                model.Vendor_id = editData.Vendor_id;
                model.Vendor_name = editData.Vendor_name;
                model.Department_name = editData.Department_name;
                model.Department_id = editData.Department_id;
                model.TransactionType = editData.TransactionType;
                model.Quantity = Convert.ToInt32(editData.Quantity);
                model.Transaction_date = Convert.ToDateTime(editData.Transaction_date).ToString("dd/MM/yyyy");
            }

            return model;
        }
        public List<VMTransactionReport> GetTransactionReport(string Transaction_type, string ToDate, string FromDate)
        {
            List<VMTransactionReport> lstReport = new List<VMTransactionReport>();
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            DataSet ds = new DataSet();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = "USPGETTRANCTIONDETAILS";             
                cmd.CommandType = CommandType.StoredProcedure;
                var paramTransactionType = cmd.CreateParameter();
                paramTransactionType.ParameterName = "@TransactionType";
                paramTransactionType.Value = Transaction_type;
                cmd.Parameters.Add(paramTransactionType);

                var paramToDate = cmd.CreateParameter();
                paramToDate.ParameterName = "@ToDate";
                paramToDate.Value = ToDate;
                cmd.Parameters.Add(paramToDate);

                var paramFromDate = cmd.CreateParameter();
                paramFromDate.ParameterName = "@FromDate";
                paramFromDate.Value = FromDate;
                cmd.Parameters.Add(paramFromDate);
                DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                db.Database.Connection.Close();
            };
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                VMTransactionReport vmReport = new VMTransactionReport();
                vmReport.Department_name = dr["Department_name"].ToString();
                vmReport.Item_name = dr["Item_name"].ToString();
                vmReport.Category = dr["Category"].ToString();
                vmReport.Rate = dr["Rate"].ToString();
                vmReport.Transaction_date = dr["Transaction_date"].ToString();
                vmReport.Vendor_name = dr["Vendor_name"].ToString();
                vmReport.Quantity = dr["Quantity"].ToString();
                vmReport.Price = dr["Price"].ToString();

                lstReport.Add(vmReport);




            }

            return lstReport;



        }

    }
}