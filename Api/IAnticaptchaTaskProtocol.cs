using Newtonsoft.Json.Linq;
using Solidex.Microservices.Core.ApiResponse;

namespace Solidex.Microservices.Core.Api
{
    public interface IAnticaptchaTaskProtocol
    {
        JObject GetPostData();
        TaskResultResponse.SolutionData GetTaskSolution();
    }
}