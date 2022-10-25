using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Hizmetler;
public class EfCoreHizmetRepository : EfCoreCommonRepository<Hizmet>,IHizmetRepository
{
    public EfCoreHizmetRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
