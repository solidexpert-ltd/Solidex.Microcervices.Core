using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.ViewModels.ActivityStream;
using Solidex.Core.ViewModels.Company;
using Solidex.Core.ViewModels.UserInformation;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Querying;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface ICompanyApi
    {
        
        #region Activity

        [Get("/{shortcut}/activity")]
        Task<PageView<ActivityViewModel.ActivityViewModelDetails>> GetActivitiesAsync(string shortcut, [Query(CollectionFormat.Multi)] FilterRequest<QueryRequest.ActivityRequest> request);

        [Post("/{shortcut}/activity")]
        Task<ActivityViewModel.ActivityViewModelDetails> CreateActivityAsync(string shortcut, [Body] ActivityViewModel.ActivityCreateModel model);

        [Get("/search/activity")]
        Task<ApiResponse<List<ActivityViewModel.ActivityViewModelDetails>>> GetLastActiviesGet([Query(CollectionFormat.Multi)] FilterRequest<QueryRequest.IdsSearchRequest> request);

        [Post("/search/activity")]
        Task<ApiResponse<List<ActivityViewModel.ActivityViewModelDetails>>> GetLastActiviesPost([Body] FilterRequest<QueryRequest.IdsSearchRequest> request);
        #endregion

        #region Participants

        [Get("/{shortcut}/participant")]
        Task<PageView<UserInformationViewModel.UserInformationViewModelParticipant>> GetParticipantsAsync(string shortcut,
            [Query(CollectionFormat.Multi)] FilterRequest request);

        [Get("/{shortcut}/participant/{id}")]
        Task<UserInformationViewModel.UserInformationViewModelParticipant> GetParticipantAsync(string shortcut, Guid id);

        #endregion

        #region Company

        [Get("/route")]
        Task<PageView<CompanyViewModel.Summary>> GetCompaniesAsync([Query(CollectionFormat.Multi)] FilterRequest<QueryRequest.CompanyRequest> request);

        [Get("/route/{shortcut}")]
        Task<ApiResponse<CompanyViewModel.Details>> GetCompanyToShortcutAsync(string shortcut);
        
        [Patch("/route/{shortcut}")]
        Task<ApiResponse<CompanyViewModel.Details>> UpdateCompanyField(string shortcut,[Body] Dictionary<string,string> company);

        #endregion
    }
}