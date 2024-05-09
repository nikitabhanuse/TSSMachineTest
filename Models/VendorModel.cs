using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSSMachineTest.Data;

namespace TSSMachineTest.Models
{
    public class VendorModel
    {

        public int Vendor_id { get; set; }
        public string Vendor_name { get; set; }

        public virtual ICollection<tblTransaction> tblTransactions { get; set; }

        public string SaveVendor(VendorModel model)
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            string msg = "save successfully";
            var save = new Vendor_mast()
            {
                Vendor_id = model.Vendor_id,
                Vendor_name = model.Vendor_name
            };
            db.Vendor_mast.Add(save);
            db.SaveChanges();

            return msg;

        }
        public List<VendorModel> VendorList()
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<VendorModel> vlist = new List<VendorModel>();
            var vendorlist = db.Vendor_mast.ToList();

            foreach (var alist in vendorlist)
            {
                vlist.Add(new VendorModel()
                {
                    Vendor_id = alist.Vendor_id,
                    Vendor_name = alist.Vendor_name
                });
            }
            return vlist;
        }

        public string DeleteVendor(int Vendor_id)
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            string msg = "Delete successfully";
            var del = db.Vendor_mast.Where(p => p.Vendor_id == Vendor_id).FirstOrDefault();
            db.Vendor_mast.Remove(del);
            db.SaveChanges();
            return msg;

        }

        public VendorModel EditVendor(int vendor_id) //.....public RegModel.....
        {
            VendorModel model = new VendorModel();
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            var edit = db.Vendor_mast.Where(p => p.Vendor_id == vendor_id).FirstOrDefault();

            if (edit != null)
            {

                model.Vendor_id = edit.Vendor_id;
                model.Vendor_name = edit.Vendor_name;

            }
            return model;
        }
    }
}

    