using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSSMachineTest.Models
{
    public class VMtransactions
    {
       public List<department> dept {  get; set; }
        public List<Vendor> vendors {  get; set; }
        public List<ItemMaster> items {  get; set; }

    }
    public class department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

    }
    public class Vendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set;}
    }
    public class ItemMaster
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Balance_qty {  get; set; }

    }
    public class VMSaveTransation
    {
        public int Transaction_id { get; set; }
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public string Vendor_name { get; set; }
        public string Department_name { get; set; }
        public string Transaction_date { get; set; }
        public Nullable<int> Department_id { get; set; }
        public Nullable<int> Vendor_id { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }

    }
    public class VMTransactionDetails
    {
        public int Transaction_id { get; set; }
        public int Item_id { get; set; }
        public string Item_name { get;  set; }
        public string Department_name { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; }
        public string Transaction_date { get; set; }
        public Nullable<int> Department_id { get; set; }
        public Nullable<int> Vendor_id { get; set; }        
        public string Vendor_name { get;  set; }
        
    }
    public class VMTransactionReport
    {
        public string Item_name { get; set; }
        public string Category { get; set; }
        public string Rate { get; set; }
        public string Transaction_date { get; set;}
        public string Department_name { get; set; }
        public string Vendor_name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }


    }
}