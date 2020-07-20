using AutoMapper;
using Solidex.Core.Data.Models.Autoremove;
using Solidex.Core.ViewModels.Autoremove;

namespace Microcervices.Core.AutomapperProfiles
{
    public class AutoremoveProfile: Profile
    {
        public AutoremoveProfile()
        {
            CreateMap<AutoremoveModuleEntity, AutoremoveViewModel.Details>();
            CreateMap<AutoremoveRuleEntity, AutoremoveViewModel.RuleSummary>();
        }
    }
}