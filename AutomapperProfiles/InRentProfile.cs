using AutoMapper;
using Solidex.Core.Data.Models.InRent;
using Solidex.Core.ViewModels.InRent;

namespace Microcervices.Core.AutomapperProfiles
{
    public class InRentProfile: Profile
    {
        public InRentProfile()
        {
            CreateMap<RentModuleEntity, RentModuleViewModel.Summary>();
            CreateMap<RentModuleEntity, RentModuleViewModel.Details>();

            CreateMap<RentEntity, RentEntityViewModel.Summary>();
            CreateMap<RentEntity, RentEntityViewModel.Details>();

            CreateMap<RentPriceRuleEntity, RentEntityViewModel.PriceRule>();

            CreateMap<RentCategoryEntity, RentCategoryViewModel.Summary>();
            CreateMap<RentCategoryEntity, RentCategoryViewModel.Details>();
            CreateMap<RentCategoryTypeEntity, RentCategoryTypeViewModel.Summary>();
        }
    }
}