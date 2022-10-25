using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Commons;
public interface ICommonRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
{
    /*ICollection tipinde navigation properties olan entitylerde
     * include properties olmayan metotları kullanıyor olucaz.
     * ICollection tipinde navigation properties'i olmayan entity'lerde ise
     * include properties olan metotları kullanmamız gerekmektedir.
     */
    //predicate'i null veriyoruz çünkü herhangi bir sorgu yapılmadan sadece id ile işlem yapabiliriz.
    //includeProperties ile navigation property'lerden hangilerinin dolu olarak gelmesini sağlıyoruz.
    Task<TEntity> GetAsync(object id, 
        Expression<Func<TEntity, bool>> predicate = null, 
        params Expression<Func<TEntity, object>>[] includeProperties);
    
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, 
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetAsync(object id, 
        Expression<Func<TEntity, bool>> predicate = null);//bazı entity'lerin ICollection tipinde bir navigation property'leri olurken kendi içinde de navigation property'leri bulunacaktır. Yani iç içe geçmiş navigation property'leri bulunacak. Dolayısıyla buna ihtiyacımız olacak.

    Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount, 
        Expression<Func<TEntity, bool>> predicate = null, 
        Expression<Func<TEntity,TKey>> orderBy = null, 
        params Expression<Func<TEntity,object>>[] includeProperties);

    Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount, 
        Expression<Func<TEntity, bool>> predicate = null, 
        Expression<Func<TEntity, TKey>> orderBy = null);

    Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount,
        Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, TKey>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector,
        Expression<Func<TEntity, bool>> predicate = null);

    //raporlama için kullanıyor olucaz. StoreProcedure ile yapıyor olucaz. StoreProcedure olduğu için parametre olabilir de olmayabilir de.
    Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
}