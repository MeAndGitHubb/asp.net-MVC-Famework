using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBContext db = new MyDBContext();

        //Tra ve 1 mau tin
        public List<Link> getList()
        {
            List<Link> list = db.Links.ToList();
            return list;
        }
        public Link getRow(int? id)
        {

            return db.Links.Find(id);
        }
        public Link getRow(int tableid,string type)
        {
            return db.Links.Where(m => m.TableId == tableid && m.Type == type).FirstOrDefault();
        }
        // Them mau tin
        public Link getRow(string slug)
        {
            return db.Links.Where(m => m.Slug == slug).FirstOrDefault();
        }
        public int Insert(Link row)
        {

            db.Links.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Link row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
