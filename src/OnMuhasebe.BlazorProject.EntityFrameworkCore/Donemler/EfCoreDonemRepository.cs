using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.Donemler;
public class EfCoreDonemRepository : EfCoreCommonRepository<Donem>, IDonemRepository
{
    public EfCoreDonemRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
