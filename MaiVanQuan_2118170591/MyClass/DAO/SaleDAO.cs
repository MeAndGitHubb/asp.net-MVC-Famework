using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SaleDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Sale> getList(string status = "All")
        {
            List<Sale> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Sales.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Sales.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Sales.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Sale getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sales.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Sale row)
        {

            db.Sales.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Sale row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Sale row)
        {
            db.Sales.Remove(row);
            return db.SaveChanges();
        }
    }
}
