using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementData.Model
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        public int PropertyId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }
    }
}
