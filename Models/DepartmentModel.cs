using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSSMachineTest.Data;

namespace TSSMachineTest.Models
{
    public class DepartmentModel
    {
        public int Department_id { get; set; }
        public string Department_name { get; set; }
        public string SaveDept(DepartmentModel model)
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            string msg = "save successfully";

            if (model.Department_id == 0)
            {
                var save = new Department_mast()
                {
                    Department_id = model.Department_id,
                    Department_name = model.Department_name
                };
                db.Department_mast.Add(save);
                db.SaveChanges();
                return msg;
            }
            else
            {
                var update = db.Department_mast.Where(x => x.Department_id == model.Department_id).FirstOrDefault();

                if (update != null)
                {
                    update.Department_id = model.Department_id;
                    update.Department_name = model.Department_name;
                }
                db.SaveChanges();
                msg = "update successfuly";
            }
            return msg;

        }

        public List<DepartmentModel> DepartList()
        {
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            List<DepartmentModel> lstdepart = new List<DepartmentModel>();
            var data = db.Department_mast.ToList();
            if (data != null)
            {
                foreach (var Dep in data)
                {
                    lstdepart.Add(new DepartmentModel()
                    {
                        Department_id = Dep.Department_id,
                        Department_name = Dep.Department_name
                    });
                }
            }
            return lstdepart;
        }
        public string DeleteDepart(int Department_id)
        {
            var msg = "delete successfully";
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            var data = db.Department_mast.Where(p => p.Department_id == Department_id).FirstOrDefault();
            if (data != null)
            {
                db.Department_mast.Remove(data);
                db.SaveChanges();
            }
            return msg;

        }
        public DepartmentModel EditDepart(int Department_id)
        {
            DepartmentModel model = new DepartmentModel();
            TSSMachineTestEntities db = new TSSMachineTestEntities();
            var data = db.Department_mast.Where(p => p.Department_id == Department_id).FirstOrDefault();
            if (data != null)
            {
                model.Department_id = data.Department_id;
                model.Department_name = data.Department_name;

            }
            return model;
        }
    }
}

    