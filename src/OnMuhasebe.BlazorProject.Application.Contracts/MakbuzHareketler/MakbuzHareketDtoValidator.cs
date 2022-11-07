using OnMuhasebe.BlazorProject.Makbuzlar;

namespace OnMuhasebe.BlazorProject.MakbuzHareketler;
public class MakbuzHareketDtoValidator : AbstractValidator<MakbuzHareketDto>
{
    public MakbuzHareketDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Id"]]);

        RuleFor(x => x.OdemeTuru)
            .IsInEnum()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["PaymentType"]])

            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["PaymentType"]]);

        RuleFor(x => x.TakipNo)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["TrackingNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxTakipNoLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["TrackingNo"], MakbuzHareketConsts.MaxTakipNoLength]);

        RuleFor(x => x.CekBankaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Bank"]]);

        RuleFor(x => x.CekBankaId)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["Bank"]]);

        RuleFor(x => x.CekBankaSubeId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["BankBranch"]]);

        RuleFor(x => x.CekBankaSubeId)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["BankBranch"]]);



        RuleFor(x => x.CekHesapNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CheckAccountNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxCekHesapNoLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["CheckAccountNo"], MakbuzHareketConsts.MaxCekHesapNoLength]);

        RuleFor(x => x.CekHesapNo)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["CheckAccountNo"]]);


        RuleFor(x => x.BelgeNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CheckNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxBelgeNoLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["CheckNo"], MakbuzHareketConsts.MaxBelgeNoLength]);

        RuleFor(x => x.Vade)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Date"]]);

        RuleFor(x => x.AsilBorclu)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek || x.OdemeTuru == OdemeTuru.Senet)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["PrincipalDebtor"]])

            .MaximumLength(MakbuzHareketConsts.MaxAsilBorcluLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["PrincipalDebtor"], MakbuzHareketConsts.MaxAsilBorcluLength]);

        RuleFor(x => x.AsilBorclu)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["PrincipalDebtor"]]);

        RuleFor(x => x.Ciranta)
           .MaximumLength(MakbuzHareketConsts.MaxCirantaLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Endorser"], MakbuzHareketConsts.MaxCirantaLength]);

        RuleFor(x => x.Ciranta)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["Endorser"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Nakit)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CashAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Banka || x.OdemeTuru == OdemeTuru.Pos)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["BankAccount"]]);

        RuleFor(x => x.Tutar)
           .NotNull()
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Amount"]])

           .GreaterThanOrEqualTo(0)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["Amount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BelgeDurumu)
            .IsInEnum()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["MeonsOfPaymentState"]])

            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["MeonsOfPaymentState"]]);

        RuleFor(x => x.Aciklama)
          .MaximumLength(EntityConsts.MaxAciklamaLength)
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
