using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_4___Finance
{
    public class ProjectExpense
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string? Note { get; set; }
        public int ProjectPhaseId { get; set; }  
        public ProjectPhase ProjectPhase { get; set; }
    }
}
