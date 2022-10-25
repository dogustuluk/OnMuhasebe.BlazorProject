using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Commons;
/*Raporlama kısmında kullanacağımız repository
 * burada id ile ilgili bir alana ihtiyaç duymuyor olucaz, o yüzden TKey almıyoruz.
 * string sql -> sorgu ya da storeProcedure ismi olabilir.
 */
public interface ICommonNoKeyRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters);
    
    Task<IList<TEntity>> FromSqlRawListAsync(string sql, params object[] parameters);
}
