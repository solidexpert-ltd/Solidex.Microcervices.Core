using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Solidex.Core.Data.Models.Schedule;
using Solidex.Core.ViewModels.Schedule;

namespace Microcervices.Core.AutomapperProfiles
{
    public class ScheduleModuleProfile: Profile
    {
        public ScheduleModuleProfile()
        {
            CreateMap<ScheduleModule, ScheduleModuleViewModel.ScheduleModuleViewModelSummary>();
            CreateMap<ScheduleModule, ScheduleModuleViewModel.ScheduleModuleViewModelDetails>();
            CreateMap<ParticipantEvent, ParticipantEventViewModel.Summary>();
            CreateMap<ParticipantEvent, ParticipantEventViewModel.Details>();
        }
    }
}
