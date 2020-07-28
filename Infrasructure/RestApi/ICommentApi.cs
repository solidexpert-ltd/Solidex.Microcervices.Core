using System;
using System.Threading.Tasks;
using Solidex.Core.ViewModels.Querying;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Comment;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface ICommentApi
    {
        [Get("/{parentId}/comment")]
        Task<PageView<CommentViewModel>> GetCommentsAsync(Guid parentId, [Query(CollectionFormat.Multi)] FilterRequest request);

        [Post("/{parentId}/comment")]
        Task<CommentViewModel> CreateCommentAsync(Guid parentId, [Body] CommentViewModel model);

        [Put("/{parentId}/comment/{id}")]
        Task<CommentViewModel> UpdateCommentAsync(Guid parentId, Guid id, [Body] CommentViewModel model);

        [Delete("/{parentId}/comment/{id}")]
        Task<ResponseViewModel> DeleteCommentAsync(Guid parentId, Guid id);
    }
}