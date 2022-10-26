using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class CreateBankaHesapDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public BankaHesapTuru? HesapTuru { get; set; } = BankaHesapTuru.VadesizMevduatHesabi;
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public string Aciklama { get; set; }
    public Guid? BankaSubeId { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public Guid? SubeId { get; set; }
    public bool Durum { get; set; }
}