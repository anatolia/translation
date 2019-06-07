using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Translation.Data.Entities.Base;

namespace Translation.Data.Repositories.Base
{
    public interface IBaseRepository<T, R> where T : BaseEntity
                                           where R : BaseRevision<T>
    {
        Task<int> Insert(T entity, int currentUserId);
        Task<int> InsertBulk(IEnumerable<T> entities, int currentUserId);

        Task<bool> Update(T entity, int currentUserId);

        Task<bool> Delete(int id, int currentUserId);
        Task<bool> UndoDelete(int id, int currentUserId);

        Task<T> SelectById(long id);
        Task<T> Select(Expression<Func<T, bool>> where);

        Task<List<T>> SelectMany(Expression<Func<T, bool>> where = null, int skip = 0, int take = 1000, bool isAscending = true);
        Task<List<T>> SelectAfter(int lastId, int take = 100, bool isAscending = true);
        
        Task<List<R>> SelectRevisions(int id);

        Task<bool> Any(Expression<Func<T, bool>> where = null);
        Task<long> Count(Expression<Func<T, bool>> where = null);
        Task<long> Max(Expression<Func<T, bool>> where = null, Expression<Func<T, int>> column = null);
        Task<long> Min(Expression<Func<T, bool>> where = null, Expression<Func<T, int>> column = null);
        Task<long> Sum(Expression<Func<T, bool>> where = null, Expression<Func<T, long>> column = null);
    }
}