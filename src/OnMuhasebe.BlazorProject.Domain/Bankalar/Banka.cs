namespace OnMuhasebe.BlazorProject.Bankalar;
public class Banka:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav prop
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    //nav prop for BankaSube
    public ICollection<BankaSube> BankaSubeler { get; set; }
}