using Volo.Abp;

namespace OnMuhasebe.BlazorProject.Exceptions;
public class CannotBeDeletedException : BusinessException
{
    public CannotBeDeletedException() : base(BlazorProjectDomainErrorCodes.CannotBeDeleted)
    {
    }
}
