using System.Linq.Expressions;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Exceptions;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Extensions;
public static class EntityAsyncExtensions
{
    /// <summary>
    /// <para>Metot kullanıldığı zaman; UI'dan gelen kodu buraya yolluyoruz ve check edilip edilmediği de sorguluyor olucaz. Eğer UI'dan gelen kod ile o andaki kodumuz aynı ise check edilmesine gerek yoktur.</para>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <param name="kod"></param>
    /// <param name="check"></param>
    /// <returns></returns>
    /// <exception cref="DuplicateCodeException"></exception>
    //AnyAsync'i kullanıyor olacağımız için, IReadOnlyRepository'i almalıyız.
    public static async Task KodAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, string kod, Expression<Func<TEntity, bool>> predicate, bool check = true) where TEntity : class, IEntity
    {
        if (check && await repository.AnyAsync(predicate))
        {
            throw new DuplicateCodeException(kod);
        }
    }
    public static async Task EntityAnyAsync(this IReadOnlyRepository<OzelKod> repository, Guid? id, OzelKodTuru kodTuru, KartTuru kartTuru, bool check = true)
    {
        //veri tabanında var ise;
        if (check && id != null)
        {
            var anyAsync = await repository.AnyAsync
                (x =>
                    x.Id == id
                &&
                    x.KodTuru == kodTuru
                &&
                    x.KartTuru == kartTuru
                );
            //veri tabanında yok ise
            if (!anyAsync)
            {
                throw new EntityNotFoundException(typeof(OzelKod), id);//OzelKod tipinde  ve buradaki id'ye sahip data veri tabanında yoktur.
            }
        }
    }
    /// <summary>
    /// Bu metoda predicate olarak ilişki halinde olmuş olduğu entity'nin kullanıp kullanmadığı ile alakalı olarak bir predicate gelecek ve buna göre işlem yapılacak. Gönderilen predicate true olarak geri dönerse kullanılmıştır, false ise kullanılmamıştır.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static async Task RelationalEntityAnyAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
    {
        var anyAsync = await repository.AnyAsync(predicate);

        if (anyAsync)
        {
            throw new CannotBeDeletedException();
        }
    }
}