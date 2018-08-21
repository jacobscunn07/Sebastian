using Microsoft.EntityFrameworkCore;

namespace Sebastian.Api.Domain
{
    public static class SebastianDbContextExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}