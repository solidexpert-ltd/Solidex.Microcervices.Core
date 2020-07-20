using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Solidex.Core.ViewModels;

namespace Microcervices.Core.Infrasructure
{
    public static class ResponseHelper
    {
        public static IActionResult AsActionResult(this ResponseViewModel model)
        {
            var result = new ObjectResult(model)
            {
                StatusCode = model.StatusCode
            };

            return result;
        }

        public static IActionResult AsActionResult<TModel>(this ResponseViewModel<TModel> model) where TModel : class
        {
            if (model.StatusCode >= 200 | model.StatusCode < 205)
            {
                return new ObjectResult(model.ViewModel)
                {
                    StatusCode = model.StatusCode
                };
            }
            else
            {
                return new ObjectResult(model)
                {
                    StatusCode = model.StatusCode
                };
            }
        }

        public static PageView<TModel> ToPageView<TModel>(this List<TModel> list, int page = 1, int total = 0) where TModel: class
        {
            return new PageView<TModel>()
            {
                Count = list.Count,
                Total = total,
                Page = page,
                Elements = list
            };
        }
    }
}
