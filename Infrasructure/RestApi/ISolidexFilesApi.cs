using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Picture;
using Solidex.Core.ViewModels.Querying;

namespace Microservices.RestClient.Infrastructure
{
    public interface ISolidexFilesApi
    {
        [Get("/files/{shortcut}")]
        Task<ApiResponse<PageView<FileEntryViewModel.Details>>> GetFilesToShorcutAsync(string shortcut, [Query(CollectionFormat.Multi)] FilterRequest<QueryRequest.FilesSearchRequest> request);

        [Post("/files/{shortcut}")]
        Task<ApiResponse<PageView<FileEntryViewModel.Details>>> UploadFilesToShortcut(string shortcut, [Body] List<FileUploadModel> model);

        [Delete("/files/{shortcut}/{id}")]
        Task<ApiResponse<ResponseViewModel>> DeleteFileAsync(string shortcut, Guid id);
    }
}