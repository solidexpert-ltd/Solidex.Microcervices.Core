using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Solidex.Core.Data.Models.Payment;
using Solidex.Core.ViewModels.Payment;

namespace Microcervices.Core.AutomapperProfiles
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile()
        {
            CreateMap<AlfabankProviderEntity, PaymentProviderViewModel.Alfabank>();
            CreateMap<Privat24ProviderEntity, PaymentProviderViewModel.Private24>();
            CreateMap<WalletOneProviderEntity, PaymentProviderViewModel.WalletOne>();
            CreateMap<WebPayProviderEntity, PaymentProviderViewModel.WebPay>();
            CreateMap<YandexCashProviderEntity, PaymentProviderViewModel.YandexCash>();

            CreateMap<(PaymentModuleEntity, PaymentProvider), PaymentViewModel.Summary>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<PaymentViewModel.Summary>(src.Item1))
                .ForMember(f => f.Provider, opt => opt.MapFrom(m => m.Item2));
        }
    }
}