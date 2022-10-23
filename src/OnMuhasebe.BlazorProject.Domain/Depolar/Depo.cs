namespace OnMuhasebe.BlazorProject.Depolar;
public class Depo:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid SubeId { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav prop
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    public Sube Sube { get; set; }//bir deponun bir tane şubesi varken bir şubenin birden fazla deposu olabilir.
    //nav prop FirmaParametre
    public ICollection<FirmaParametre> FirmaParametreler { get; set; }
    //nav prop FaturaHareket
    public ICollection<FaturaHareket> FaturaHareketler { get; set; }
}
