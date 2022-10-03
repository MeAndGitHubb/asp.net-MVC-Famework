using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
namespace MyClass.DAO
{
    public class BranchDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Branch> getList(string status = "All")
        {
            List<Branch> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Branchs.Where(m => m.NameBranch != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Branchs.Where(m => m.NameBranch == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Branchs.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Branch getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Branchs.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Branch row)
        {

            db.Branchs.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Branch row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Branch row)
        {
            db.Branchs.Remove(row);
            return db.SaveChanges();
        }
    }
}
