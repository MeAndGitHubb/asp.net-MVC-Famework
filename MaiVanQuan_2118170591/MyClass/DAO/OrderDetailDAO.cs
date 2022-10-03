using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private MyDBContext db = new MyDBContext();

        // tra ve danh sach cac mau tin
        public List<OrderDetail> getList(int? orderid)
        {

            return db.OrderDetails.Where(m=>m.OrderId== orderid).ToList();

        }
        public List<OrderDetail> getList(string status = "All")
        {
            List<OrderDetail> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.OrderDetails.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.OrderDetails.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.OrderDetails.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public OrderDetail getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.OrderDetails.Find(id);
            }
        }
        // Them mau tin
        public int Insert(OrderDetail row)
        {

            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(OrderDetail row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
