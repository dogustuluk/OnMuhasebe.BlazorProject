using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.OzelKodlar;
public class EfCoreOzelKodRepository : EfCoreCommonRepository<OzelKod>,IOzelKodRepository
{
    public EfCoreOzelKodRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
