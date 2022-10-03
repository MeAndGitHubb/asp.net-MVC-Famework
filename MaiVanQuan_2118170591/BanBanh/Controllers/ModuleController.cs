﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace BanBanh.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);
            return View("MainMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if (list.Count == 0)
            {
                return View("MainMenuSub1", menu);///khong co con
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);///co con
            }

        }
        //Slideshow
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slideshow");
            return View("Slideshow", list);
        }
        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("MenuFooter", list);
        }
        public ActionResult Search()
        {
            return View("Search");
        }
    }
}