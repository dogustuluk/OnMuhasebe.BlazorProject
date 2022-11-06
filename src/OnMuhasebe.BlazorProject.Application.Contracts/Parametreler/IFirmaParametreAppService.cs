using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Parametreler;
public interface IFirmaParametreAppService : ICrudAppService<SelectFirmaParametreDto, SelectFirmaParametreDto, FirmaParametreListParameterDto, CreateFirmaParametreDto, UpdateFirmaParametreDto>
{
    Task<bool> UserAnyAsync(Guid userId); 
}
