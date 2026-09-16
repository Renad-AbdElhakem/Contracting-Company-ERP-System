using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos
{
    public class ProjectExpenseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string? Note { get; set; }
        public int ProjectPhaseId { get; set; }
    }
}
