namespace OnMuhasebe.BlazorProject.OzelKodlar;
public class CreateOzelKodDtoValidator : AbstractValidator<CreateOzelKodDto>
{
    public CreateOzelKodDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
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

        RuleFor(x => x.KodTuru)
            .IsInEnum()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CodeType"]])

            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CodeType"]]);

        RuleFor(x => x.KartTuru)
            .IsInEnum()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CardType"]])

            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CardType"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
