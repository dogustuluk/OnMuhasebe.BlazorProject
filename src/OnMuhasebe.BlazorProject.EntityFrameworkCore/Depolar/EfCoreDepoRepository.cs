using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Depolar;
public class EfCoreDepoRepository : EfCoreCommonRepository<Depo>, IDepoRepository
{
    public EfCoreDepoRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Depo>> WithDetailsAsync()
    {
        #region ICollectionNavigationPropertyDescription
        /* Kendi include ve thenInclude ICollection tipinde property'leri olan entity'lerin içindeki yani ICollection property'lerinin içerisindeki navigation property'lerine ulaşabilmek için include ve thenInclude'ları burada yapıcaz.
         * Bu işlemi yapabilmek için öncelikle IQueryable lazım çünkü bunları sadece oraya ekleyebiliriz. ABP Framework hazır bir fonksiyon sunmaktadır.
         */
        #endregion
        return (await GetQueryableAsync())
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
    }
}
