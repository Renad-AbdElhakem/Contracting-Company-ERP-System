using AutoMapper;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_1_Project_Site_Management
{
    public class InternalExpensesProfile : Profile
    {
        public InternalExpensesProfile()
        {
            CreateMap<CreateExpenseDto, Expense>();
            CreateMap<Expense, ExpenseDto>();

            CreateMap<CreateProjectExpenseDto, ProjectExpense>();
               

            CreateMap<ProjectExpense, ProjectExpenseDto>();
        }
    }
}
