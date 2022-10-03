using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class RecruitmentDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Recruitment> getList(string status = "All")
        {
            List<Recruitment> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Recruitments.Where(m => m.Id != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Recruitments.Where(m => m.Id == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Recruitments.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Recruitment getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Recruitments.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Recruitment row)
        {

            db.Recruitments.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Recruitment row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Recruitment row)
        {
            db.Recruitments.Remove(row);
            return db.SaveChanges();
        }
    }
}
