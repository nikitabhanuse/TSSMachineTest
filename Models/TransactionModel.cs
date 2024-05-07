using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSSMachineTest.Data;

namespace TSSMachineTest.Models
{

    public class TransactionModel
    {
        public int Transaction_id { get; set; }
        public int Item_id { get; set; }
        public string Item_name { get; private set; }
        public System.DateTime Transaction_date { get; set; }
        public Nullable<int> Department_id { get; set; }
        public Nullable<int> Vendor_id { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; }

        public virtual Department_mast Department_mast { get; set; }
        public virtual Item_master Item_master { get; set; }
        public virtual Vendor_mast Vendor_mast { get; set; }
        public string Vendor_name { get; private set; }
        public string Department_name { get; private set; }

        public string SaveTransaction(TransactionModel model)
        {
            string Message = "Data Save Successfully.";
            TSSMachineTestEntities db = new TSSMachineTestEntities();

            if (model.Transaction_id == 0)
            {
                var editData = db.Item_master.Where(p => p.Item_id == model.Item_id).FirstOrDefault();
                if (editData != null && model.TransactionType != null)
                {
                    TransactionType = model.TransactionType;
                    editData.Balance_quantity = editData.Balance_quantity;
                    if (TransactionType == "I")
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
                    Quantity = Convert.ToInt32(editTran.Quantity);
                    TransactionType = model.TransactionType;
                    editData.Balance_quantity = editData.Balance_quantity;
                    editData.Balance_quantity = editData.Balance_quantity - editTran.Quantity;
                }
                if (editData != null && model.TransactionType != null)
                {
                    {
                        TransactionType = model.TransactionType;
                        editData.Balance_quantity = editData.Balance_quantity;
                        if (TransactionType == "I")
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
                    update.Transaction_date = model.Transaction_date;
                }
                db.SaveChanges();
                Message = "Update  Data Successfully.";

            }
            return Message;
        }
        public List<TransactionModel> GetTransactionList()
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<TransactionModel> lstTransaction = new List<TransactionModel>();

            var TransactionList = (from m in db.Item_master
                                   join t in db.tblTransactions on m.Item_id equals t.Item_id
                                   select new
                                   {
                                       m.Item_id,
                                       m.Item_name,
                                       t.Transaction_date,
                                       t.Transaction_id,
                                       t.Quantity,
                                       t.TransactionType
                                   }).ToList();
            if (TransactionList != null)
            {
                foreach (var obj in TransactionList)
                {
                    lstTransaction.Add(new TransactionModel()
                    {
                        Transaction_id = obj.Transaction_id,
                        Item_id = obj.Item_id,
                        Item_name = obj.Item_name,
                        TransactionType = obj.TransactionType,
                        Quantity = Convert.ToInt32(obj.Quantity),
                        Transaction_date = obj.Transaction_date,
                    });
                }
            }
            return lstTransaction;
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
        public TransactionModel EditTransaction(int Transaction_id)
        {
            TransactionModel model = new TransactionModel();
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
                model.Vendor_name = editData.Vendor_name;
                model.Department_name = editData.Department_name;
                model.TransactionType = editData.TransactionType;
                model.Quantity = Convert.ToInt32(editData.Quantity);
                model.Transaction_date = editData.Transaction_date;
            }

            return model;
        }
    }
}