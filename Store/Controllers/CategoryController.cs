using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {
        private StoreContext _db;

        public CategoryController(StoreContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Index()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }
        
        [HttpPost]
        [ActionName("Index")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        public IActionResult Remove(int id)
        {
            Category category = _db.Categories.FirstOrDefault(b => b.Id == id);
            if(category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Edit(int id)
        {
            Category category = _db.Categories.FirstOrDefault(b => b.Id == id);
            if (category?.Name != null)
            {
                ViewBag.CategoryEvent = "Edit";
                ViewBag.category = category;
                ViewBag.Categories = _db.Categories.ToList();
                return View(nameof(Index));
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameof(Index));
        }
    }
}