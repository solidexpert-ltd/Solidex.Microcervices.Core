using AutoMapper;
using Solidex.Core.Data.Models.SMS;
using Solidex.Core.ViewModels.Sms;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.AutomapperProfiles
{
    public class SmsServiceProfile: Profile
    {
        public SmsServiceProfile()
        {
            CreateMap<(CompanySmsMessageEntity, UserInformationViewModel.UserInformationViewModelSummary),
                    RouteSmsMessageViewModel>()
                .ConstructUsing((src, ctx) =>
                    ctx.Mapper.Map<CompanySmsMessageEntity, RouteSmsMessageViewModel>(src.Item1))
                .ForMember(m => m.Client, opt => opt.MapFrom(m => m.Item2));
        }
    }
}