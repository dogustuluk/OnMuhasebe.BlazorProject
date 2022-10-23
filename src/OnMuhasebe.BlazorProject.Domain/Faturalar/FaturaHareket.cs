namespace OnMuhasebe.BlazorProject.Faturalar;
/*FullAuditedEntity vermemizin nedeni
 * faturadan bağımsız hareket etmesi gerekmektedir. çünkü 6 fatura hareketinden 2 tanesi silindi ise bunu raporlarken görmemiz gerekir.
 */
public class FaturaHareket:FullAuditedEntity<Guid>
{
    public Guid FaturaId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public Guid? StokId { get; set; }
    public Guid? HizmetId { get; set; }
    public Guid? MasrafId { get; set; }
    public Guid? DepoId { get; set; }
    public decimal Miktar { get; set; }
    public decimal BirimFiyat { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public int KdvOrani { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public string Aciklama { get; set; }
}
