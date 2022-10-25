using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Donemler;
public class UpdateDonemDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}
