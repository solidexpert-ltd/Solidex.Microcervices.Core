using System.Linq;
using AutoMapper;
using Solidex.Core.Data.Models.Accounting;
using Solidex.Core.Data.Models.Company;
using Solidex.Core.Data.Models.CompanyApplication;
using Solidex.Core.ViewModels.Company;
using Solidex.Core.ViewModels.CompanyApp;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.AutomapperProfiles
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<UserInformationViewModel.UserInformationViewModelSummary, UserInformationViewModel.UserInformationViewModelParticipant>();

            CreateMap<CompanyApplicationEntity, CompanyAppViewModel.Summary>()
                .ForMember(f => f.Count, opt => opt.MapFrom(m => m.Items.Count));

            CreateMap<ApplicationItemEntity, ApplicationItemViewModel.Summary>()
                .ForMember(f => f.Properties, opt => opt.MapFrom(m => m.Properties.ToDictionary(k => k.PropertyName, v => v.Value)));

            CreateMap<LegalInformationEntity, LegalInformationViewModel.Summary>()
                .ForMember(f => f.ModifiedBy, opt => opt.Ignore());

            CreateMap<CompanyEntity, CompanyViewModel.Summary>();
            CreateMap<CompanyEntity, CompanyViewModel.Details>();

            CreateMap<TransactionEntity, AccountViewModel.TransactionSummary>();

            CreateMap<BonusCardTransactionEntity, BonusCardViewModel.TransactionViewModelSummary>()
                .ForMember(f => f.Date, opt => opt.MapFrom(m => m.ModificationDate));
            CreateMap<BonusCardTransactionEntity, BonusCardViewModel.TransactionViewModelDetails>()
                .ForMember(f => f.Date, opt => opt.MapFrom(m => m.ModificationDate));
            CreateMap<(BonusCardTransactionEntity, object), BonusCardViewModel.TransactionViewModelDetails>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<BonusCardViewModel.TransactionViewModelDetails>(src.Item1))
                .ForMember(f => f.Creator, opt => opt.MapFrom(m => m.Item2));
        }
        
    }
}