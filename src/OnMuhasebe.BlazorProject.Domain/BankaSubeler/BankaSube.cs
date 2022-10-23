namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class BankaSube:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public Guid BankaId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //navigation property
    public ICollection<BankaHesap> BankaHesaplar { get; set; } //bire-çok ilişkideki "çok" kısmı.
    //nav prop2 BankaSube 
    public Banka Banka { get; set; }
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
}
