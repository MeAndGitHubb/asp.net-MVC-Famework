using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using PagedList.Mvc;
using PagedList;
namespace BanBanh.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        private LinkDAO linkDAO = new LinkDAO();
        private PostDAO postDAO = new PostDAO();
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private TopicDAO topicDAO = new TopicDAO();
        public ActionResult Index(string slug = null, int? page = null)
        {
            //MyDBContext db = new MyDBContext();
            //int count = db.Products.Count();
            //ViewBag.somautin = count.ToString();

            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.Type;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                                //break;
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                                //break;
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                                //break;
                            }
                        default:
                            {
                                return this.Error404(slug);
                                //break;
                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug, "Post");
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }
            }
        }
        public ActionResult Home()
        {
            List<ProductInfo> productNew = productDAO.getList(4);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //danh muc loai theo 3 cap
            List<int> listcatid = new List<int>();
            listcatid.Add(id);//cap 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//cap2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);//cap 3 
                        }
                    }
                }
            }
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct", list);
        }
        //nhom action product
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1;  //Trang hien tai
            int pageSize = 9; //So mau tin hien thi tren 1 trang
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        private ActionResult ProductCategory(string slug, int? page)
        {
            int pageNumber = (page ?? 1);  //Trang hien tai
            int pageSize = 9; //So mau tin hien thi tren 1 trang
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //danh muc loai theo 3 cap
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);//cap 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);//cap2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category.Id);//cap 3 
                        }
                    }
                }
            }
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        private ActionResult ProductDetail(Product product)
        {

            return View("ProductDetail", product);
        }

        //nhom action post
        public ActionResult Post(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IPagedList<PostInfo> list = postDAO.getList(pageSize, pageNumber, "Post");
            return View("Post", list);
        }
        private ActionResult PostTopic(String slug, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            IPagedList<PostInfo> list = postDAO.getListByTopicId(topic.Id, pageSize, pageNumber, "Post", null);
            return View("PostTopic", list);
        }
        private ActionResult PostPage(String slug)
        {
            Post post = postDAO.getRow(slug, "page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.Topid, "Post", post.Id);
            return View("PostDetail", post);
        }

        //ham loi
        private ActionResult Error404(String slug)
        {
            return View("Error404");
        }

    }
}