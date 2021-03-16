using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tasks.Service.Persistence
{
    public class EFTransactionalService : ITransactionalService, IDisposable
    {
        private TaskDbContext context;
        private IDbContextTransaction transaction;

        public EFTransactionalService(TaskDbContext context)
        {
            this.context = context;
        }

        public void BeginTransaction()
        {
            if(this.transaction == null) {
                this.transaction = context.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            context.SaveChanges();
            transaction.Commit();
        }

        public void Dispose()
        {
            if(transaction != null) 
            {
                transaction.Dispose();
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }

}