using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<User> getList1(string status = "All")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }

            }

            return list;

        }
        public List<User> getList(string page = "All",string roles=null)
        {
            List<User> list = null;
            switch (page)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0 && m.Roles == roles).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0 && m.Roles == roles).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Users.Where(m => m.Roles == roles).ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public User getRow(int? id)
        {
                return db.Users.Find(id);
            
        }
        public User getRow(string usename, string roles)
        {
          
                return db.Users.Where(m =>m.Status==1 && m.Roles== roles && (m.Username == usename || m.Email==usename)).FirstOrDefault();
            
        }
        // Them mau tin
        public int Insert(User row)
        {

            db.Users.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(User row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
