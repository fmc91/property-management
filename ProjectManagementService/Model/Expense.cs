using System;

namespace PropertyManagementService.Model
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }
    }
}