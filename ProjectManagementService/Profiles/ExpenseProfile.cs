using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using ExpenseEntity = PropertyManagementData.Model.Expense;

namespace PropertyManagementService.Profiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseEntity, Expense>();

            CreateMap<Expense, ExpenseEntity>()
                .ForMember(e => e.PropertyId, opt => opt.Ignore());
        }
    }
}
