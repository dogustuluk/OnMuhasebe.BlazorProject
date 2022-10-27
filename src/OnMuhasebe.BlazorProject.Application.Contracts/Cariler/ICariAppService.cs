using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Cariler;
public interface ICariAppService : ICrudAppService<SelectCariDto, ListCariDto, CariListParameterDto, CreateCariDto, UpdateCariDto, CodeParameterDto>
{
}