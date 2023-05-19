using TestDBLib;
using Microsoft.EntityFrameworkCore;
using TestDBLib.Entities;
using System.Linq.Expressions;
using EFCore.BulkExtensions;

namespace BusinessLayerLib.Implementations
{
    public class EFRepository<T> : IAsyncDisposable
        where T: Entity
    {
        private readonly TestDBContext testDBContext;

        public EFRepository(TestDBContext testDBContext)
        {
            this.testDBContext = testDBContext;
        }

        private static void CheckEntry(T entry) { if (entry is null) throw new ArgumentNullException(nameof(entry), $"{entry} must be non null"); }


        // CRUD для таблиц из базы данных как синхронные, так и асинхронные

        public void Delete(T entry)
        {
            CheckEntry(entry);
            testDBContext.Remove(entry);
            testDBContext.SaveChanges();
        }
        public async Task DeleteAsync(T entry)
        {
            CheckEntry(entry);
            testDBContext.Remove(entry);
            await testDBContext.SaveChangesAsync();
        }

        public void Update(T entry)
        {
            CheckEntry(entry);
            testDBContext.Update(entry);
            testDBContext.SaveChanges();
        }
        public async Task UpdateAsync(T entry)
        {
            CheckEntry(entry);
            testDBContext.Update(entry);
            await testDBContext.SaveChangesAsync();
        }

        public void Insert(T entry)
        {
            CheckEntry(entry);
            testDBContext.Add(entry);
            testDBContext.SaveChanges();
        }
        public async Task InsertAsync(T entry)
        {
            CheckEntry(entry);
            await testDBContext.AddAsync(entry);
            await testDBContext.SaveChangesAsync();
        }
        public void InsertRange(params T[] entries) //EF Core + BULK SQL
        {
            using (var transaction = testDBContext.Database.BeginTransaction())
            {
                testDBContext.BulkInsert(entries);
                transaction.Commit();
            }
        }
        public async Task InsertRangeAsync(params T[] entries) //EF Core + BULK SQL
        {
            using (var transaction = testDBContext.Database.BeginTransaction())
            {
                testDBContext.BulkInsert(entries);
                await transaction.CommitAsync();
            }
        }

        public IQueryable<T>? Get(int takeNumber = 0, Expression<Func<T, bool>>? predicate = null)
        {
            if (takeNumber == 0) takeNumber = testDBContext.Set<T>().Count();

            return predicate is null ? testDBContext.Set<T>().Take(takeNumber).ToList().AsQueryable() :
                                testDBContext.Set<T>().Take(takeNumber).ToList().AsQueryable().Where(predicate);
        }


        public async Task<IQueryable<T>?> GetAsync(int takeNumber = 0, Expression<Func<T, bool>>? predicate = null)
        {
            if (takeNumber == 0)
            {
                return predicate is null ? (await testDBContext.Set<T>().ToListAsync()).AsQueryable() :
                   (await testDBContext.Set<T>().Where(predicate).ToListAsync()).AsQueryable();
            }

            return predicate is null ? (await testDBContext.Set<T>().Take(takeNumber).ToListAsync()).AsQueryable() :
                    (await testDBContext.Set<T>().Where(predicate).Take(takeNumber).ToListAsync()).AsQueryable();
        }



        public async ValueTask DisposeAsync()
        {
            await testDBContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
