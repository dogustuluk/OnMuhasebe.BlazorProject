namespace OnMuhasebe.BlazorProject.OzelKodlar;
public class OzelKod:FullAuditedAggregateRoot<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public OzelKodTuru KodTuru { get; set; }
    public KartTuru KartTuru { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    //nav properties BankaHesap
    public ICollection<BankaHesap> OzelKod1BankaHesaplar { get; set; }
    public ICollection<BankaHesap> OzelKod2BankaHesaplar { get; set; }
    //nav prop2 Banka
    public ICollection<Banka> OzelKod1Bankalar { get; set; }
    public ICollection<Banka> OzelKod2Bankalar { get; set; }
    //nav prop3 BankaSube
    public ICollection<BankaSube> OzelKod1BankaSubeler { get; set; }
    public ICollection<BankaSube> OzelKod2BankaSubeler { get; set; }
    //nav prop4 Birim
    public ICollection<Birim> OzelKod1Birimler { get; set; }
    public ICollection<Birim> OzelKod2Birimler { get; set; }
    //nav prop5 Cari
    public ICollection<Birim> OzelKod1Cariler { get; set; }
    public ICollection<Birim> OzelKod2Cariler { get; set; }
    //nav prop6 depo
    public ICollection<Depo> OzelKod1Depolar { get; set; }
    public ICollection<Depo> OzelKod2Depolar { get; set; }
    //nav prop7 fatura
    public ICollection<Fatura> OzelKod1Faturalar { get; set; }
    public ICollection<Fatura> OzelKod2Faturalar { get; set; }
    //nav prop8 Birim
    public ICollection<Hizmet> OzelKod1Hizmetler { get; set; }
    public ICollection<Hizmet> OzelKod2Hizmetler { get; set; }
    //nav prop9 Kasa
    public ICollection<Kasa> OzelKod1Kasalar { get; set; }
    public ICollection<Kasa> OzelKod2Kasalar { get; set; }
    //nav prop10 Makbuz
    public ICollection<Makbuz> OzelKod1Makbuzlar { get; set; }
    public ICollection<Makbuz> OzelKod2Makbuzlar { get; set; }
    //nav prop11 Stok
    public ICollection<Stok> OzelKod1Stoklar { get; set; }
    public ICollection<Stok> OzelKod2Stoklar { get; set; }
}