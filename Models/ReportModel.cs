using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSSMachineTest.Data;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;


namespace TSSMachineTest.Models
{
    public class ReportModel
    {
        public List<VMTransactionReport> GetTransactionReport(string Transaction_type, string ToDate,string FromDate)
        {
            List<VMTransactionReport> lstReport = new List<VMTransactionReport>();
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            DataSet ds = new DataSet();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = "USPGETTRANCTIONDETAILS";
                cmd.CommandType = CommandType.StoredProcedure;
                DbDataAdapter da= DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                da.SelectCommand= cmd;
                da.Fill(ds);
                db.Database.Connection.Close();
            };
            foreach(DataRow dr in ds.Tables[0].Rows)
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