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
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<PostInfo> getListByTopicId(int? topid, string type = "Post", int? notid = null)
        {
            List<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid).ToList();
            }
            else
            {
                list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid && m.Id != notid).ToList();
            }
            return list;
        }

        public IPagedList<PostInfo> getListByTopicId(int? topid, int pageSize, int pageNumber, string type = "Post", int? notid = null)
        {
            IPagedList<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);
            }
            else
            {
                list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid && m.Id != notid)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);
            }
            return list;
        }
        public List<PostInfo> getList(string type = "Post")
        {
            List<PostInfo> list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type).ToList();
            return list;
        }
        public IPagedList<PostInfo> getList(int pageSize, int pageNumber, string type = "Post")
        {
            IPagedList<PostInfo> list = db.Posts
                            .Join(db.Topics,
                            p => p.Topid,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                Topid = p.Topid,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaDesc = p.MetaDesc,
                                MetaKey = p.MetaKey,
                                Created_By = p.Created_By,
                                Updated_By = p.Updated_By,
                                Created_At = p.Created_At,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            ).Where(m => m.Status == 1 && m.PostType == type)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public List<Post> getList(string page = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (page)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostType == type).ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        // Them mau tin

        public Post getRow(string slug, string posttype)
        {
            return db.Posts
                .Where(m => m.Slug == slug && m.PostType == posttype && m.Status == 1)
                .FirstOrDefault();
        }
        public int Insert(Post row)
        {

            db.Posts.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Post row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
