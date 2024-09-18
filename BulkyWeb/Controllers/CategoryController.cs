﻿using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
               return View(objCategoryList);
        }


        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category has been created successfully";

                return RedirectToAction("Index");
            }
            return View();
           
        }

        public IActionResult Edit(int? id)
        {
            if (id ==null || id==0)
            {
                return NotFound();
            }
            Category categoryFromDb = _categoryRepository.Get(u=>u.Id == id);
            if( categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _categoryRepository.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        } 

        [HttpPost,  ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepository.Get(u => u.Id == id);
            if (obj == null)
            { 
                return NotFound();
            }

            _categoryRepository.Remove(obj);
            _categoryRepository.Save();
                TempData["success"] = "Category has been deleted successfully";
            return RedirectToAction("Index");


        }
    }
}