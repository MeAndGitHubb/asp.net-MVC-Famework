using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ReportDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Report> getList(string status = "All")
        {
            List<Report> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Reports.Where(m => m.Id != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Reports.Where(m => m.Id == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Reports.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Report getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Reports.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Report row)
        {

            db.Reports.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Report row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Report row)
        {
            db.Reports.Remove(row);
            return db.SaveChanges();
        }
    }
}
