using ExpensesTracker.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesTracker.Models
{
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            this.DailyExpenses = new List<DailyExpense>();
        }
        public int ExpenseCategoryId { get; set; }
        [Required, StringLength(50), Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        //Navigation
        public virtual ICollection<DailyExpense> DailyExpenses { get; set; }
    }
    public class DailyExpense
    {
        public int DailyExpenseId { get; set; }
        [Required, Column(TypeName = "date"), FutureDateValidation(ErrorMessage = "Value cannot be future date"),
            Display(Name = "Expenses Date")
            ]
        public DateTime ExpenseDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Required, ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }
        //Navigation
        public virtual ExpenseCategory ExpenseCategory { get; set; }

    }
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) {
        

        
        }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<DailyExpense> DailyExpenses { get; set; }
    }
}
