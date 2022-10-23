namespace OnMuhasebe.BlazorProject.Cariler;
public class Cari:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string VergiDairesi { get; set; }
    public string VergiNo { get; set; }
    public string Telefon { get; set; }
    public string Adres { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav properties
    public OzelKod OzelKod1 { get; set; }
    public OzelKod OzelKod2 { get; set; }
    //nav prop Fatura
    public ICollection<Fatura> Faturalar { get; set; }
}
