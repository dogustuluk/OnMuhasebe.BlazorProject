using System;
using System.Collections.Generic;
using System.Text;

namespace OnMuhasebe.BlazorProject.Stoklar;
public class UpdateStokDtoValidator : AbstractValidator<UpdateStokDto>
{
    public UpdateStokDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
    {
        RuleFor(x => x.Kod)
          .NotEmpty()
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Code"]])

          .MaximumLength(EntityConsts.MaxKodLength)
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Code"], EntityConsts.MaxKodLength]);

        RuleFor(x => x.Ad)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Name"]])

            .MaximumLength(EntityConsts.MaxAdLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Name"], EntityConsts.MaxAdLength]);

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["ValueAddedTaxRate"], localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["UnitPrice"], localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Barkod)
            .MaximumLength(EntityConsts.MaxBarkodLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["BarCode"], EntityConsts.MaxBarkodLength]);

        RuleFor(x => x.BirimId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Unit"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
