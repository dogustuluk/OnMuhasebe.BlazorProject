﻿using System;
using System.Collections.Generic;
using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.MakbuzHareketler;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class ListBankaHesapDto : EntityDto<Guid>
{
    public string Ad { get; set; }
    public string Kod { get; set; }
    public BankaHesapTuru HesapTuru { get; set; }
    public string HesapTuruAdi { get; set; }
    public string HesapNo { get; set; }
    public string IbanNo { get; set; }
    public string BankaAdi { get; set; }
    public string BankaSubeAdi { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public decimal Borc { get; set; }
    public decimal Alacak { get; set; }
    public decimal BorcBakiye => Borc - Alacak > 0 ? Borc - Alacak : 0;
    public decimal AlacakBakiye => Alacak - Borc > 0 ? Alacak - Borc : 0;
    public string Aciklama { get; set; }
    public ICollection<SelectMakbuzHareketDto> MakbuzHareketler { get; set; }
}