namespace OnMuhasebe.BlazorProject.Donemler;
public class Donem:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav prop Fatura
    public ICollection<Fatura> Faturalar { get; set; }
    //nav prop Makbuz
    public ICollection<Makbuz> Makbuzlar { get; set; }
    //nav prop FirmaParametre
    public ICollection<FirmaParametre> FirmaParametreler { get; set; }
}
