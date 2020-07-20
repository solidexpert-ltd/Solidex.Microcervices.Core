using System.Threading.Tasks;
using Refit;
using Solidex.Core.ViewModels;
using Solidex.Core.ViewModels.Vstroyke.Order;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface IOrderApi
    {
        [Post("/{shortcut}/order")]
        Task<ApiResponse<ResponseViewModel<OrderViewModel.Details>>> CreateOrderAsync(string shortcut, [Body] OrderViewModel.Details model);
    }
}