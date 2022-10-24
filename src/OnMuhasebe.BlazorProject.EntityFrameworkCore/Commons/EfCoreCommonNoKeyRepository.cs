using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Commons;
public class EfCoreCommonNoKeyRepository<TEntity> : EfCoreRepository<BlazorProjectDbContext, TEntity>, ICommonNoKeyRepository<TEntity> where TEntity : class, IEntity
{
    public EfCoreCommonNoKeyRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FromSqlRaw(sql,parameters).FirstOrDefaultAsync();
    }
    public async Task<IList<TEntity>> FromSqlRawListAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FromSqlRaw(sql,parameters).ToListAsync();
    }

}
