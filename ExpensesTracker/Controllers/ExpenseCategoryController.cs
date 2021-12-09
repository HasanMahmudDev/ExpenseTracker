using ExpensesTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesTracker.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ExpenseDbContext db;
        public ExpenseCategoryController(ExpenseDbContext db) { this.db = db; }
        //View Section
        public IActionResult Index()
        {
            return View(db.ExpenseCategories.ToList());
        }
        //create Section
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ExpenseCategory expense)
        {
            if (ModelState.IsValid)
            {
                if (db.ExpenseCategories.Any(x => x.CategoryName.ToLower() == expense.CategoryName.ToLower()))
                {
                    ModelState.AddModelError("", "Category name already exits");
                    return View(expense);
                }
                db.ExpenseCategories.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }
        //Edit Section
        public ActionResult Edit(int id)
        {
            return View(db.ExpenseCategories.FirstOrDefault(x => x.ExpenseCategoryId == id));
        }
        [HttpPost]
        public ActionResult Edit(ExpenseCategory expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        //Delete Section
        public IActionResult Delete(int id)
        {
            return View(db.ExpenseCategories.First(x => x.ExpenseCategoryId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var expense = new ExpenseCategory { ExpenseCategoryId = id };
            if (!db.DailyExpenses.Any(x => x.ExpenseCategoryId == id))
            {
                db.Entry(expense).State = EntityState.Deleted;
                db.SaveChanges();
                return PartialView("_PDelete", true);
            }
            ModelState.AddModelError("", "Cannot delete. ExpenseCategory has related Daily Expense.");
            return PartialView("_PDelete", false);
        }

    }
}
