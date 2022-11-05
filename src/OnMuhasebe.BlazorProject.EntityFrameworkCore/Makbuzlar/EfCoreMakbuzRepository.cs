using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public class EfCoreMakbuzRepository : EfCoreCommonRepository<Makbuz>,IMakbuzRepository
{
    public EfCoreMakbuzRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    public override async Task<IQueryable<Makbuz>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Cari)
            .Include(x => x.Kasa)
            .Include(x => x.BankaHesap)
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.CekBanka)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.CekBankaSube)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.Kasa)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.BankaHesap);
    }
}
