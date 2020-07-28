using System.Threading.Tasks;
using Refit;
using Solidex.Core.Base.Infrastructure;
using Solidex.Core.Data.Models.SMS;
using Solidex.Core.ViewModels;

namespace Microcervices.Core.Infrasructure.RestApi
{
    public interface ISmsServiceApi
    {

        #region System SMS

        [Post("/system")]
        Task<ResponseViewModel> SendSystemSmsAsync([Body] SmsMessageEntity message);

        #endregion

       
        #region Company SMS

        [Post("/company")]
        Task<ResponseViewModel> SendCompanySmsAsync([Body] CompanySmsMessageEntity message);

        #endregion
    }
}