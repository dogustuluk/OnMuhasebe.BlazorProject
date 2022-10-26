using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.FaturaHareketler;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class FaturaReportDto : IEntityDto
{//fatura dökümü yapılacağı zaman.
    public string FaturaNo { get; set; }
    public DateTime Tarih { get; set; }
    public string CariAdi { get; set; }
    public string VergiDairesi { get; set; }
    public string VergiNo { get; set; }
    public string Adres { get; set; }
    public string Telefon { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public string Aciklama { get; set; }
    public List<FaturaHareketReportDto> FaturaHareketler { get; set; }
}
