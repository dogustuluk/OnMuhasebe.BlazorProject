using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Commons;
public class EfCoreCommonRepository<TEntity> : EfCoreRepository<BlazorProjectDbContext, TEntity, Guid>, ICommonRepository<TEntity> where TEntity : class, IEntity<Guid>
{
    public EfCoreCommonRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        //dataları getirirken includeProperties ile beraber navigation properties'lerini de dolu olarak gelmesini sağladık.
        var queryable = await WithDetailsAsync(includeProperties);

        TEntity entity;
        if(predicate != null)
        {//first kullanırsak null veri gelirse hata almış oluruz. Sadece veri döndüğünden eminsek kullanırız.
            entity = await queryable.FirstOrDefaultAsync(predicate);
            if(entity == null)
                throw new EntityNotFoundException(typeof(TEntity),id);
            return entity;
        }

        entity = await queryable.FirstOrDefaultAsync();
        if(entity == null)
            throw new EntityNotFoundException(typeof(TEntity),id);
        return entity;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {//hata mesajını vermek istemezsek. Proje ilk yüklendiği anda şube ve dönem seçilmediği için hata fırlamasını istemediğimiz için kullanıyor olucaz.
        var queryable = await WithDetailsAsync(includeProperties);

        if(predicate != null)
            return await queryable.FirstOrDefaultAsync(predicate);
        
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetAsync(object id, Expression<Func<TEntity, bool>> predicate = null)
    {
        var queryable = await WithDetailsAsync();//ICollection tipindeki navigation property'si olan entity'ler için bunu kullanıyoruz. buradaki navigation property'leri farklı bir class'ta veriyor olucaz.
        TEntity entity;

        if(predicate != null)
        {
            entity = await queryable.FirstOrDefaultAsync(predicate);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity),id);

            return entity;
        }

        entity = await queryable.FirstOrDefaultAsync();
        if(entity == null)
            throw new EntityNotFoundException(typeof(TEntity),id);
        return entity;
    }

    public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = await WithDetailsAsync(includeProperties);

        if(predicate !=null)//örn-> "select * from tabloAdi"
            queryable = queryable.Where(predicate);//burada sorguya ek yapıyoruz yani -> "select * from tabloAdi -> where Id>5 <-
        
        if(orderBy != null)
            queryable = queryable.OrderBy(orderBy);//burada da aynı şekilde sorgunun predicate uygulanmış haline OrderBy eklemiş oluyor.

        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();//ToListAsync ifadesi ile queryable ile yazmış olduğumuz string ifade veri tabanına gider. bundan öncesinde herhangi bir şekilde veri tabanına gönderme işlemi yapmaz, burada daha sorguyu yazıp tamamlayınca buradaki noktaya gelir.
    }

    public async Task<List<TEntity>> GetPagedListAsync<TKey>(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null)
    {
        var queryable = await WithDetailsAsync();

        if(predicate != null)
            queryable= queryable.Where(predicate);

        if (orderBy != null)
            queryable = queryable.OrderBy(orderBy);

        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    public async Task<List<TEntity>> GetPagedLastListAsync<TKey>(int skipCount, int maxResultCount, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = await WithDetailsAsync(includeProperties);

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (orderBy != null)
            queryable = queryable.OrderByDescending(orderBy);

        return await queryable
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    /// <summary>
    /// <para>Banka'nın Kod property'sinin otomatik olarak artması için kullanılan metot.</para>
    /// </summary>
    /// <param name="propertySelector"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<string> GetCodeAsync(Expression<Func<TEntity, string>> propertySelector, Expression<Func<TEntity, bool>> predicate = null)
    {
        static string CreateNewCode(string code)
        {
            var number = "";

            foreach (var character in code)
            {
                if (char.IsDigit(character))
                    number += character;
                else
                    number = "";//kod alanı string başlayıp sayısala geçip sonra tekrardan string devam ederse daha önce verilen sayıları kaldırmamız lazım.
            }
            var newNumber = number == "" ? "1" : (long.Parse(number) + 1).ToString();
            //code'un uzunluğu ile newNumber arasındaki farkı al
            var difference = code.Length - newNumber.Length;

            if (difference < 0)//Cari-9999 son kod olsun. eğer yeni bir cari eklersek son halimiz Cari10000 olacak.
                difference = 0;

            var newCode = code.Substring(0, difference);//Artık elimizde Cari- kısmı var.
            newCode += newNumber;//newCode'dan döneni aldı ve yeni oluşturulan sayıyı ekledi.
             
            return newCode;
        }
        //açıklama -> yeni bir banka oluşturulduğunda banka kodunun artmasını sağlıyor olucaz -> Banka001,Banka002,Banka003 gibi. burada string ifadeleri atıp kalan kısmı işliyor olucaz. propertySet ile ne yollarsak onun en büyük değerini alır ve işlem yaparız.
        //ilk önce dbset almak gerekiyor
        var dbSet = await GetDbSetAsync();
        var maxCode = predicate == null? 
            await dbSet.MaxAsync(propertySelector):
            await dbSet.Where(predicate).MaxAsync(propertySelector);
        
        return maxCode == null ? "0000000000000001" : CreateNewCode(maxCode);
    }
    public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
    {
        var context = await GetDbContextAsync(); //getDbSet ile de yapabiliriz.
        return await context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
    }
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        var dbSet = await GetDbSetAsync();
        return predicate == null? await dbSet.AnyAsync():await dbSet.AnyAsync(predicate);
    }
}
