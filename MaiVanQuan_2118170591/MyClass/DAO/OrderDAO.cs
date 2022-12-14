using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }

            }

            return list;

        }
        public List<OrderInfo> getListJoin(string status = "All")
        {
            List<OrderInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,

                                ReceiverName = o.ReceiverName,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                Note = o.Note,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Number = od.Number,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount,
                                Sale = od.Sale
                            }

                            )
                            .Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,

                                ReceiverName = o.ReceiverName,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                Note = o.Note,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Number = od.Number,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount,
                                Sale = od.Sale
                            }

                            )
                            .Where(m => m.Status != 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.OrderId,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                UserId = o.UserId,

                                ReceiverName = o.ReceiverName,
                                ReceiverAddress = o.ReceiverAddress,
                                ReceiverPhone = o.ReceiverPhone,
                                ReceiverEmail = o.ReceiverEmail,
                                Note = o.Note,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                OrderId = od.OrderId,
                                ProductId = od.ProductId,
                                Number = od.Number,
                                Price = od.Price,
                                Qty = od.Qty,
                                Amount = od.Amount,
                                Sale = od.Sale
                            }

                            )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Order row)
        {

            db.Orders.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Order row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}

