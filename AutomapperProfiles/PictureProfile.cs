using AutoMapper;
using Solidex.Core.Data.Models.Picture;
using Solidex.Core.ViewModels.Picture;

namespace Microcervices.Core.AutomapperProfiles
{
    public class PictureProfile: Profile
    {
        public PictureProfile()
        {
            CreateMap<FileEntryEntity, FileEntryViewModel.Summary>();
            CreateMap<FileEntryEntity, FileEntryViewModel.Details>();

            CreateMap<PhotoEntity, FileEntryViewModel.PhotoViewModel>();
        }
    }
}