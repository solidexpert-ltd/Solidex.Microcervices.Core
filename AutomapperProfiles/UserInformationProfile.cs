using System.Linq;
using AutoMapper;
using Solidex.Core.Data.Models.UserInformation;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.AutomapperProfiles
{
    public class UserInformationProfile: Profile
    {
        public UserInformationProfile()
        {
            CreateMap<UserInformationEntity, UserInformationViewModel.UserInformationViewModelSummary>()
                .ForMember(f => f.UserPhone, opt => opt.MapFrom(m => m.Phones.First().Phone.Number));

            CreateMap<GroupEntity, PermissionViewModel.GroupViewModelSummary>()
                .ForMember(f => f.Count, opt => opt.MapFrom(m => m.UserGroups.Count));

            CreateMap<GroupEntity, PermissionViewModel.GroupViewModelDetails>()
                .ForMember(f => f.Permissions, opt => opt.MapFrom(m => m.GroupPermissions));

            CreateMap<GroupPermissionEntity, PermissionViewModel.GroupPermissionViewModelSummary>();
            CreateMap<GroupPermissionEntity, PermissionViewModel.GroupPermissionViewModelDetails>();

        }
    }
}