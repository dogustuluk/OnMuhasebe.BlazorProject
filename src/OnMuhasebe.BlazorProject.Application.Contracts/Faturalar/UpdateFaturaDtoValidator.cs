using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.FaturaHareketler;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class UpdateFaturaDtoValidator : AbstractValidator<UpdateFaturaDto>
{
    public UpdateFaturaDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
    {
        RuleFor(x => x.FaturaNo)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["InvoiceNumber"]])

            .MaximumLength(FaturaConsts.MaxFaturaNoLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["InvoiceNumber"], FaturaConsts.MaxFaturaNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Date"]]);

        RuleFor(x => x.BrutTutar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["GrossAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["GrossAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.IndirimTutar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["DiscountAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["DiscountAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.KdvHaricTutar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["ExcludesValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["ExcludesValueAddedTaxAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.KdvTutar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["ValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["ValueAddedTaxAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NetTutar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["TotalAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["TotalAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CariId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Customer"]]);

        RuleFor(x => x.Aciklama)
           .MaximumLength(EntityConsts.MaxAciklamaLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);

        
        RuleForEach(x => x.FaturaHareketler)
            .SetValidator(y => new FaturaHareketDtoValidator(localizer));
    }
}
