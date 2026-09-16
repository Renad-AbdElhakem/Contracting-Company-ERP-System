using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ContractDtos
{
    public class CreateContractDto
    {
        public DateOnly SignDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PenaltyClauseNote { get; set; }
        public decimal? PenaltyPerDay { get; set; }
        public decimal? MaxPenaltyAmount { get; set; }
        public Guid ClientId { get; set; }
    }
}
