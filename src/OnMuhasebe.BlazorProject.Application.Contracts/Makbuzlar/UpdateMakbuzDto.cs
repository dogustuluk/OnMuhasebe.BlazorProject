using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.MakbuzHareketler;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public class UpdateMakbuzDto : IEntityDto
{
    public MakbuzTuru MakbuzTuru { get; set; } //bu property update edilemez fakat buradan hareketle bazı işlemler yapıyor olucaz. Mesela CreateMakbuzDto'da Kasa'da makbuz türü'nü kullanıyoruz. update aşamasında bu property'i map'leme aşamasında devre dışı bırakmış olucaz.
    public string MakbuzNo { get; set; }
    public DateTime Tarih { get; set; }
    public Guid? CariId { get; set; }
    public Guid? KasaId { get; set; }
    public Guid? BankaHesapId { get; set; }
    public int HareketSayisi { get; set; }
    public decimal CekToplam { get; set; }
    public decimal SenetToplam { get; set; }
    public decimal PosToplam { get; set; }
    public decimal NakitToplam { get; set; }
    public decimal BankaToplam { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    public IList<MakbuzHareketDto> MakbuzHareketler { get; set; }
}