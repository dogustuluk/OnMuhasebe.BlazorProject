using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Makbuzlar;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.MakbuzHareketler;
public class EfCoreMakbuzHareketRepository : EfCoreCommonRepository<MakbuzHareket>,IMakbuzHareketRepository
{
    public EfCoreMakbuzHareketRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
