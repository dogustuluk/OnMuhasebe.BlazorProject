using System;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class UpdateBankaHesapDto : IEntityDto//IEntityDto'yu generic olarak yapmıyoruz, ilgili id'yi dışardan yolluyor olucaz.
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru? HesapTuru { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public string Aciklama { get; set; }
    public Guid? BankaSubeId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public bool Durum { get; set; }
}
