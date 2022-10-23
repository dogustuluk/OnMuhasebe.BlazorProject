namespace OnMuhasebe.BlazorProject.Subeler;
public class Sube:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav property
    public ICollection<BankaHesap> BankaHesaplar { get; set; }
}