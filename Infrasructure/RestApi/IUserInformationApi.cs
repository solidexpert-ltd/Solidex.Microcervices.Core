using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Querying;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.ViewModels.UserInformation;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface IUserInformationApi
    {
        [Get("/userinformation/{id}")]
        Task<UserInformationViewModel.UserInformationViewModelSummary> GetUserInformationAsync(Guid id);

        [Get("/userinformation")]
        Task<PageView<UserInformationViewModel.UserInformationViewModelSummary>> GetUserInformationsAsnc(
            [Query(CollectionFormat.Multi)] FilterRequest<QueryRequest.FilterIdsSearchRequest> request);
    }
}