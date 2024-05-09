using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Common;
using TSSMachineTest.Data;
using System.Web.Services.Description;

namespace TSSMachineTest.Models
{
    public class ItemMasterModel
    {
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public string Category { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int Balance_quantity { get; set; }

        public virtual ICollection<tblTransaction> tblTransactions { get; set; }

        public string SaveItem(ItemMasterModel model)
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            string msg = "save successfully";

            if (model.Item_id == 0)
            {
                var save = new Item_master()
                {
                    Item_id = model.Item_id,
                    Item_name = model.Item_name,
                    Category = model.Category,
                    Rate = model.Rate,
                    Balance_quantity = model.Balance_quantity

                };
                db.Item_master.Add(save);
                db.SaveChanges();
                return msg;
            }
            else
            {
                var update = db.Item_master.Where(x => x.Item_id == model.Item_id).FirstOrDefault();

                if (update != null)
                {
                    update.Item_id = model.Item_id;
                    update.Item_name = model.Item_name;
                    update.Category = model.Category;
                    update.Rate = model.Rate;
                    update.Balance_quantity = model.Balance_quantity;

                }
                db.SaveChanges();
                msg = "update successfuly";
            }
            return msg;

        }
        //............List......
        public List<ItemMasterModel> ItemList()
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<ItemMasterModel> lstreg = new List<ItemMasterModel>();
            var listt = db.Item_master.ToList();

            foreach (var reg in listt)
            {
                lstreg.Add(new ItemMasterModel()
                {
                    Item_id = reg.Item_id,
                    Item_name = reg.Item_name,
                    Category = reg.Category,
                    Rate = reg.Rate,
                    Balance_quantity = reg.Balance_quantity

                });
            }
            return lstreg;
        }
    

        public string DeleteItem(int Item_id)
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            string Message = "Delete successfully";
            var del = db.Item_master.Where(p => p.Item_id == Item_id).FirstOrDefault();
            db.Item_master.Remove(del);
            db.SaveChanges();
            return Message;

        }

        public ItemMasterModel EditItem(int Item_id) 
        {
            ItemMasterModel model = new ItemMasterModel();
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            var edit = db.Item_master.Where(p => p.Item_id == Item_id).FirstOrDefault();

            if (edit != null)
            {
                model.Item_id = edit.Item_id;
                model.Item_name = edit.Item_name;
                model.Category = edit.Category;
                model.Rate = edit.Rate;
                model.Balance_quantity = Convert.ToInt32(edit.Balance_quantity);

            }
            return model;
        }

    }
}

        


    