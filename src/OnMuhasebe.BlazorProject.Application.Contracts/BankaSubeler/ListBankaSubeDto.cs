using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class ListBankaSubeDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string BankaAdi { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public string Aciklama { get; set; }
}
