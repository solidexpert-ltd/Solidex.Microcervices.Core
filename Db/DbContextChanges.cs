using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solidex.Core.Base.Abstraction;

namespace Microcervices.Core.Db
{
    public class DbContextWithChanges:DbContext
    {
        
        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var added = ChangeTracker.Entries().Where(w => w.State == EntityState.Added).Select(s => s.Entity).ToList();

            foreach (var entry in added.OfType<IEntity>())
            {
                (entry).CreatedDate = DateTime.Now;
                (entry).ModificationDate = DateTime.Now;
            }


            var updated = ChangeTracker.Entries().Where(w => w.State == EntityState.Modified).Select(s => s.Entity)
                .ToList();

            foreach (var entry in updated.OfType<IEntity>())
            {
                (entry).ModificationDate = DateTime.Now;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(SaveChanges, cancellationToken);
        }
    }
}