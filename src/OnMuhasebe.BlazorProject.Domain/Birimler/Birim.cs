namespace OnMuhasebe.BlazorProject.Birimler;
public class Birim:FullAuditedAggregateRoot<Guid>
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
    //nav prop Hizmet
    public ICollection<Hizmet> Hizmetler { get; set; }
    //nav prop Stok
    public ICollection<Stok> Stoklar { get; set; }
    //nav prop Masraf
    public ICollection<Masraf> Masraflar { get; set; }
}
