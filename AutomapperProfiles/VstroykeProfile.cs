using AutoMapper;
using Solidex.Core.Data.Models.Vstroyke.Buildings;
using Solidex.Core.ViewModels.Company;
using Solidex.Core.ViewModels.Vstroyke.Buildings;

namespace Microcervices.Core.AutomapperProfiles
{
    public class VstroykeProfile: Profile
    {
        public VstroykeProfile()
        {
            CreateMap<TechnicRentPositionEntity, TechnicRentPositionViewModel.Summary>()
                .ForMember(f => f.Company, opt => opt.Ignore());
            CreateMap<(TechnicRentPositionEntity, CompanyViewModel.Details), TechnicRentPositionViewModel.Summary>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<TechnicRentPositionViewModel.Summary>(src.Item1))
                .ForMember(f => f.Company, opt => opt.MapFrom(m => m.Item2));

            CreateMap<CompanyViewModel.Summary, RentCompanyViewModel>();


            CreateMap<(CompanyViewModel.Summary, RentalEntity), RentCompanyViewModel>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<CompanyViewModel.Summary, RentCompanyViewModel>(src.Item1))
                .ForMember(f => f.Positions, opt => opt.MapFrom(m => m.Item2.Positions));

            CreateMap<KopalkaEntity, KopalkaViewModel>();
            CreateMap<SchedulerEntity, SchedulerViewModel>();

        }
        
    }
}