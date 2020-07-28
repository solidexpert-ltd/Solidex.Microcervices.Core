using System.Threading.Tasks;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Payment;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface IPaymentApi
    {
        [Get("/{shortcut}/module")]
        Task<PaymentViewModel.Summary> GetModuleAsync(string shortcut);

        [Delete("/{shortcut}/module")]
        Task<ResponseViewModel> DeleteModuleAsync(string shortcut);

        [Post("/{shortcut}/data")]
        Task<PaymentDataViewModel.Response> SetPaymentData([Body] PaymentDataViewModel.Request request);
    }
}