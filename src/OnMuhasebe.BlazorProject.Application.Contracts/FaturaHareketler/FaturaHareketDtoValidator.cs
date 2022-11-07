using OnMuhasebe.BlazorProject.Faturalar;

namespace OnMuhasebe.BlazorProject.FaturaHareketler;
public class FaturaHareketDtoValidator : AbstractValidator<FaturaHareketDto>
{
    public FaturaHareketDtoValidator(IStringLocalizer localizer)
    {
        #region nonInject
        //IStringLocalizer localizer -> şeklinde bir tanımlama yapıyoruz çünkü Fatura sınıfındaki validator'larda halihazırda bir 'localizer' yolluyoruz, dolayısıyla burada gereksiz bir şekilde enjekte etmemize gerek yok.
        #endregion

        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Id"]]);

        RuleFor(x => x.HareketTuru)
           .IsInEnum()
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["RowType"]])

           .NotEmpty()
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["RowType"]]);


        #region Description
        /*Özel Durumlar
         * Stok, masraf, hizmet ve depo alanlarından biri dolu olabilir.
         * Eğer stok var ise depo da olmak zorundadır.
         * Bu durum için aşağıdaki kodlar yardımcı olur.
         */
        #endregion
        RuleFor(x => x.StokId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Stock"]]);
        RuleFor(x => x.HizmetId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Hizmet)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Service"]]);
        RuleFor(x => x.MasrafId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Masraf)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Expense"]]);
        RuleFor(x => x.DepoId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Warehouse"]]);
        //--

        RuleFor(x => x.Miktar)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Quantity"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["Quantity"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

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

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

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

        RuleFor(x => x.Aciklama)
          .MaximumLength(EntityConsts.MaxAciklamaLength)
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
