using OnMuhasebe.BlazorProject.Commons;
using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class EfCoreBankaSubeRepository : EfCoreCommonRepository<BankaSube>, IBankaSubeRepository
{ 
    public EfCoreBankaSubeRepository(IDbContextProvider<BlazorProjectDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
