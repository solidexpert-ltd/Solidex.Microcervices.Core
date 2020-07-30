using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Solidex.Core.Base.Interfaces;
using Solidex.Core.Data.Infrastructure;

namespace Microcervices.Core.Infrasructure
{
    public interface IServiceManager<TContext, TModel> where TContext: class where TModel: class, IEntity
    {
        TContext Context { get; }
        DbSet<TModel> Entities { get; }
    }
}
