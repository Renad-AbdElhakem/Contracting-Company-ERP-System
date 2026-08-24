using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Expense
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public ExpenseType Type { get; set; } 
        public string? Note { get; set; }
    }
}
