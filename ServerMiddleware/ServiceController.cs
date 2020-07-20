using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microcervices.Core.ServerMiddleware
{

    [Produces("application/json")]
    [ApiController]
    public abstract class ServiceController: ControllerBase
    {
        protected ServiceController(IMapper mapper, HttpManager.HttpManager httpManager)
        {
            Mapper = mapper;
            HttpManager = httpManager;
        }

        public string Shortcut
        {
            get
            {
                RouteData.Values.TryGetValue("shortcut", out var shortcut);
                return shortcut != null ? (string) shortcut : string.Empty;
            }
        }

        protected IMapper Mapper { get; }
        protected HttpManager.HttpManager HttpManager { get; } 
    }
}
