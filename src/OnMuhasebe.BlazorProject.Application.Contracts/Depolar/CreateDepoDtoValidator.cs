﻿namespace OnMuhasebe.BlazorProject.Depolar;
public class CreateDepoDtoValidator : AbstractValidator<CreateDepoDto>
{
    public CreateDepoDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
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

        RuleFor(x => x.SubeId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Branch"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
