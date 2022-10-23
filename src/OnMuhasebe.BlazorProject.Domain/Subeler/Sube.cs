namespace OnMuhasebe.BlazorProject.Subeler;
public class Sube:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav property
    public ICollection<BankaHesap> BankaHesaplar { get; set; }
    //nav prop Depo
    public ICollection<Depo> Depolar { get; set; }
    //nav prop Fatura
    public ICollection<Fatura> Faturalar { get; set; }
    //nav prop Kasa
    public ICollection<Kasa> Kasalar { get; set; }
    //nav prop Makbuz
    public ICollection<Makbuz> Makbuzlar { get; set; }
}