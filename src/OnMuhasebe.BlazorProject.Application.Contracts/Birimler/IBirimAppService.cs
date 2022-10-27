using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Birimler;
public interface IBirimAppService : ICrudAppService<SelectBirimDto, ListBirimDto, BirimListParameterDto, CreateBirimDto, UpdateBirimDto, CodeParameterDto>
{
}
