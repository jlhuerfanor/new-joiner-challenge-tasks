using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tasks.Service.Persistence
{
    public class EFTransactionalService : ITransactionalService
    {
        private TaskDbContext context;

        public EFTransactionalService(TaskDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }

}