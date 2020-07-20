using System.Linq;
using AutoMapper;
using Solidex.Core.Data.Models.Comment;
using Solidex.Core.ViewModels.Comment;

namespace Microcervices.Core.AutomapperProfiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentEntity, CommentViewModel>()
                .ForMember(f => f.Properties,
                    opt => opt.MapFrom(m => m.Properties.ToDictionary(k => k.PropertyName, v => v.Value)));
            CreateMap<(CommentEntity, object, object), CommentViewModel>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<CommentViewModel>(src.Item1))
                .ForMember(f => f.UserInformation, opt => opt.MapFrom(m => m.Item2))
                .ForMember(f => f.AnsweredUserInformation, opt => opt.MapFrom(m => m.Item3));

        }
    }
}