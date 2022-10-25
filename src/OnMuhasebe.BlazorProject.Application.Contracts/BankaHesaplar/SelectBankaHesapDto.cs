using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class SelectBankaHesapDto : EntityDto<Guid>,IOzelKod
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru HesapTuru { get; set; }
    public string HesapTuruAdi { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public string Aciklama { get; set; }
    public Guid BankaId { get; set; }//banka'ya BankaSubeId'nin relation'ından yapıyor olucaz.
    public string BankaAdi { get; set; }
    public Guid BankaSubeId { get; set; }
    public string BankaSubeAdi { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}