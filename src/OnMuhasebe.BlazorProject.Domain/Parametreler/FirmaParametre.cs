namespace OnMuhasebe.BlazorProject.Parametreler;
public class FirmaParametre:Entity<Guid> //kullanıcıya has değişimler olduğu için en base sınıfı miras aldık.
{
    public Guid UserId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public Guid? DepoId { get; set; }
}
