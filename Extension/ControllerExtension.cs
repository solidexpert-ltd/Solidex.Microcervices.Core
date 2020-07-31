using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microcervices.Core.Infrasructure;
using Microsoft.AspNetCore.Mvc;
using Solidex.Core.Base.Infrastructure;

namespace Microcervices.Core.Extension
{
    public interface IControllerWithMapper
    {
        IMapper Mapper { get; }
    }

    public static class ControllerExtension
    {
        /// <summary>
        /// Generate bad request response
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="message">message of response</param>
        /// <returns></returns>
        public static IActionResult BadRequestResult(this ControllerBase controllerBase, string message)
        {
            return new ObjectResult(new ResponseViewModel(message,400,false)){StatusCode = 400};
        }
        
        /// <summary>
        /// Send 404 result
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="message">message </param>
        /// <returns></returns>
        public static IActionResult NotFoundResult(this ControllerBase controllerBase, string message)
        {
            return new NotFoundObjectResult(new ResponseViewModel(message, 404, false));
        }

        public static IActionResult AccessDeniedResult(this ControllerBase controllerBase, string message)
        {
            return new ObjectResult(new ResponseViewModel(message,403,false)){StatusCode = 403};
        }

        public static IActionResult CreatedResult<T>(this ControllerBase controllerBase, T model) where T : class
        {
            return new ResponseViewModel<T>(model, 201, true).AsActionResult();
        }

        public static IActionResult CreatedResult<T,TE>(this IControllerWithMapper controller, T model) where T : class
        {
            return new ResponseViewModel<T>(controller.Mapper.Map<T>(model), 201, true).AsActionResult();
        }
        public static IActionResult FoundResult<T, TE>(this IControllerWithMapper controller, TE model) where T : class
        {
            return new OkObjectResult(new ResponseViewModel<T>(controller.Mapper.Map<T>(model)).AsActionResult());;
        }
        
        public static IActionResult PageResult<T, TE>(this IControllerWithMapper controller, IEnumerable<TE> model,int page,int totoal) where T : class
        {
            return new OkObjectResult(new PageView<T>()
            {
                Elements = controller.Mapper.Map<IList<T>>(model),
                Count = model.Count(),
                Page = page,
                Total = totoal
            });
        }
    }
}