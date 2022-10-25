using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public class EfCoreMakbuzRepository : EfCoreCommonRepository<Makbuz>,IMakbuzRepository
{
    public EfCoreMakbuzRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
