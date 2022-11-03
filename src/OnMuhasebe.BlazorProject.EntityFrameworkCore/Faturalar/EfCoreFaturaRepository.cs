using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class EfCoreFaturaRepository : EfCoreCommonRepository<Fatura>, IFaturaRepository
{
    public EfCoreFaturaRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    #region description_IncludeProperties
    /*
     * Fatura sınıfındaki navigation property'ler include edilecek.
     * Buna ek olarak FatuaHareketler sınıfına da gidilip oradaki navigation property'ler de include edilmiş olmalı.
     * Fatura sınıfı içerisinde Sube ve Donem property'leri de var lakin onların içerisindeki herhangi bir property'e ihtiyacımız olmadığı için boşuna yüklenmesini istemediğimiz için include etmiyoruz.
     */
    #endregion
    public override async Task<IQueryable<Fatura>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Cari)
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Depo)

            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Stok)
                                             .ThenInclude(x => x.Birim)

            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Hizmet)
                                             .ThenInclude(x => x.Birim)

            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Masraf)
                                             .ThenInclude(x => x.Birim);
    }
}
