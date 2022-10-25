using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Faturalar;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.FaturaHareketler;
public class EfCoreFaturaHareketlerRepository : EfCoreCommonRepository<FaturaHareket>,IFaturaHareketRepository
{
    public EfCoreFaturaHareketlerRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
