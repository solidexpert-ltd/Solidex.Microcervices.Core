using AutoMapper;
using Solidex.Core.Data.Models.Vstroyke.Order;
using Solidex.Core.ViewModels.UserInformation;
using Solidex.Core.ViewModels.Vstroyke.Order;

namespace Microcervices.Core.AutomapperProfiles
{
    public class VstroykeOrdersProfile: Profile
    {
        public VstroykeOrdersProfile()
        {
            CreateMap<(VstroykeOrderEntity, UserInformationViewModel.UserInformationViewModelSummary),
                    OrderViewModel.Summary>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<OrderViewModel.Summary>(src.Item1))
                .ForMember(f => f.UserInformation, opt => opt.MapFrom(m => m.Item2));

            CreateMap<(VstroykeOrderEntity, UserInformationViewModel.UserInformationViewModelSummary),
                    OrderViewModel.Details>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<OrderViewModel.Details>(src.Item1))
                .ForMember(f => f.UserInformation, opt => opt.MapFrom(m => m.Item2));

            CreateMap<StatusEntity, StatusViewModel>();
            CreateMap<ShippingAddressEntity, ShippingAddressViewModel>();
            CreateMap<VstroykeOrderLineEntity, OrderViewModel.OrderLineViewModel>();
        }
    }
}