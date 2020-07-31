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
        /// Send 404 result
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="message">message </param>
        /// <returns></returns>
        public static IActionResult NotFound(this ControllerBase controllerBase, string message)
        {
            return new ResponseViewModel(message, 404, false).AsActionResult();
        }

        public static IActionResult Created<T>(this ControllerBase controllerBase, T model) where T : class
        {
            return new ResponseViewModel<T>(model, 201, true).AsActionResult();
        }

        public static IActionResult Created<T,TE>(this IControllerWithMapper controller, T model) where T : class
        {
            return new ResponseViewModel<T>(controller.Mapper.Map<T>(model), 201, true).AsActionResult();
        }
        public static IActionResult Found<T, TE>(this IControllerWithMapper controller, TE model) where T : class
        {
            return new ResponseViewModel<T>(controller.Mapper.Map<T>(model)).AsActionResult();
        }
        
        public static IActionResult Page<T, TE>(this IControllerWithMapper controller, IEnumerable<TE> model,int page,int totoal) where T : class
        {
            return new ObjectResult(new PageView<T>()
            {
                Elements = controller.Mapper.Map<IList<T>>(model),
                Count = model.Count(),
                Page = page,
                Total = totoal
            });
        }
    }
}