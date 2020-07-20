using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Solidex.Core.Data.Models.ActivityStream;
using Solidex.Core.ViewModels.ActivityStream;
using Solidex.Core.ViewModels.Event;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.AutomapperProfiles
{
    public class ActivityProfile: Profile
    {
        public ActivityProfile()
        {
            CreateMap<ActivityEntity, ActivityViewModel.ActivityViewModelDetails>()
                .ForMember(f => f.Properties, opt => opt.MapFrom(m => m.Properties.ToDictionary(k => k.PropertyName, v => v.Value)));

            CreateMap<UserEventViewModel, AdminEventViewModel>()
                .ForMember(m => m.User, opt => opt.MapFrom(f => new UserInformationViewModel.UserInformationViewModelSummary()));
        }
    }
}
