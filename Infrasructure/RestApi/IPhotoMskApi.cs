using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solidex.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.ViewModels.Company;
using Solidex.Core.ViewModels.Event;
using Solidex.Core.ViewModels.UserInformation;


namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface IPhotoMskApi
    {
        [Get("/api/v2/route/{shortcut}")]
        Task<dynamic> GetRouteToShortcutAsync(string shortcut);

        [Get("/api/v2/userinformation/{id}")]
        Task<UserInformationViewModel.UserInformationViewModelSummary> GetUserInformationAsync(Guid id);

        [Patch("/api/v2/userinformation")]
        Task<List<UserInformationViewModel.UserInformationViewModelSummary>> GetUserInformationsAsync([Body]Guid[] ids);

        [Get("/api/v2/event")]
        Task<List<UserEventViewModel>> GetEventsByIDAsync([Query(CollectionFormat.Multi)] string[] ids);

        [Get("/api/v2/payments/{shortcut}/bonuscard")]
        Task<ApiResponse<OkResult>> ConfirmPaymentBonusCardAsync(string shortcut, [Query] Guid checkLineId);

        [Get("/api/v2/route/{shortcut}")]
        Task<ApiResponse<CompanyViewModel.Details>> GetRouteAsync(string shortcut);

        [Get("/api/v2/route/{shortcut}/participant")]
        Task<ApiResponse<PageView<dynamic>>>
            GetRouteParticipantsAsync(string shortcut);

        [Get("/api/v2/route/{shortcut}/legalinformation")]
        Task<ApiResponse<dynamic>> GetLegalInformationAsync(string shortcut);
    }
}