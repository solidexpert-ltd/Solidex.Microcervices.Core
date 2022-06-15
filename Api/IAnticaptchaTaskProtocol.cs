using Newtonsoft.Json.Linq;
using Solidex.Microcervices.Core.ApiResponse;

namespace Solidex.Microcervices.Core.Api
{
    public interface IAnticaptchaTaskProtocol
    {
        JObject GetPostData();
        TaskResultResponse.SolutionData GetTaskSolution();
    }
}