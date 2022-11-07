using System;
using System.Collections.Generic;
using System.Text;

namespace OnMuhasebe.BlazorProject.Cariler;
public class UpdateCariDtoValidator : AbstractValidator<UpdateCariDto>
{
    public UpdateCariDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
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

        RuleFor(x => x.VergiDairesi)
           .MaximumLength(CariConsts.MaxVergiDairesiLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["TaxAdministration"], CariConsts.MaxVergiDairesiLength]);

        RuleFor(x => x.VergiNo)
           .MaximumLength(CariConsts.MaxVergiNoLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["TaxNumber"], CariConsts.MaxVergiNoLength]);

        RuleFor(x => x.Telefon)
           .MaximumLength(EntityConsts.MaxTelefonLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["PhoneNumber"], EntityConsts.MaxTelefonLength]);

        RuleFor(x => x.Adres)
           .MaximumLength(EntityConsts.MaxAdresLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Address"], EntityConsts.MaxAdresLength]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
