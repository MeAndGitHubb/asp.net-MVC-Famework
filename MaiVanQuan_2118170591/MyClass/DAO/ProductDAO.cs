using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Detail = p.Detail,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    Number = p.Number,
                    Created_By = p.Created_By,
                    Updated_By = p.Updated_By,
                    Created_At = p.Created_At,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            return list;
        }
        
        public List<ProductInfo> getList(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products
                        .Join(
                        db.Categorys,
                        p => p.CatId,
                        c => c.Id,
                        (p, c) => new ProductInfo
                        {
                            Id = p.Id,
                            CatId = p.CatId,
                            Name = p.Name,
                            CatName = c.Name,
                            Slug = p.Slug,
                            Img = p.Img,
                            Price = p.Price,
                            PriceSale = p.PriceSale,
                            Detail = p.Detail,
                            MetaDesc = p.MetaDesc,
                            MetaKey = p.MetaKey,
                            Number = p.Number,
                            Created_By = p.Created_By,
                            Updated_By = p.Updated_By,
                            Created_At = p.Created_At,
                            Updated_At = p.Updated_At,
                            Status = p.Status,
                        }
                        )
                                    .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Detail = p.Detail,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    Number = p.Number,
                    Created_By = p.Created_By,
                    Updated_By = p.Updated_By,
                    Created_At = p.Created_At,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                }
                )
                            .Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Detail = p.Detail,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    Number = p.Number,
                    Created_By = p.Created_By,
                    Updated_By = p.Updated_By,
                    Created_At = p.Created_At,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                }
                )
                            .ToList();
                        break;
                    }

            }

            return list;

        }
        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products
                .Join(db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Detail = p.Detail,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Number = p.Number,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status,
                            }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            return list;
        }
        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Detail = p.Detail,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Number = p.Number,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status,
                            }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Detail = p.Detail,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Number = p.Number,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status,
                            }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        //Tra ve 1 mau tin
        public List<Product> SearchByKey(string key)
        {
            return db.Products.SqlQuery("Select * from Product where Name like '%"+key+"%'").ToList();
        }
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        // Them mau tin
        public int Insert(Product row)
        {

            db.Products.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Product row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }

    }
}
