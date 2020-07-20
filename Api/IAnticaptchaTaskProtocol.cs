using Microcervices.Core.ApiResponse;
using Newtonsoft.Json.Linq;

namespace Microcervices.Core.Api
{
    public interface IAnticaptchaTaskProtocol
    {
        JObject GetPostData();
        TaskResultResponse.SolutionData GetTaskSolution();
    }
}