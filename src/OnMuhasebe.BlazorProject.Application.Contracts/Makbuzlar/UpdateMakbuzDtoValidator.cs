using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.MakbuzHareketler;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public class UpdateMakbuzDtoValidator : AbstractValidator<UpdateMakbuzDto>
{
    public UpdateMakbuzDtoValidator(IStringLocalizer<BlazorProjectResource> localizer)
    {
        RuleFor(x => x.MakbuzNo)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["ReceiptNo"]])

            .MaximumLength(MakbuzConsts.MaxMakbuzNoLength)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["ReceiptNo"], MakbuzConsts.MaxMakbuzNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Date"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.KasaIslem)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Cash"]]);

        RuleFor(x => x.KasaId) //makbuztürü kasa işlem değilse kasaId'nin mutlaka null olması gerekir.
           .Empty()
           .When(x => x.MakbuzTuru != MakbuzTuru.KasaIslem)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["Cash"]]);

        RuleFor(x => x.CariId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.Tahsilat || x.MakbuzTuru == MakbuzTuru.Odeme)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["Customer"]]);

        RuleFor(x => x.CariId)
           .Empty()
           .When(x => x.MakbuzTuru != MakbuzTuru.Tahsilat && x.MakbuzTuru != MakbuzTuru.Odeme)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["Customer"]]);


        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.BankaIslem)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["BankAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.BankaIslem)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.IsNull, localizer["BankAccount"]]);

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CekToplam)
           .NotNull()
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CheckTotal"]])

           .GreaterThanOrEqualTo(0)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["CheckTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.SenetToplam)
           .NotNull()
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["BillOfExchangeTotal"]])

           .GreaterThanOrEqualTo(0)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["BillOfExchangeTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.PosToplam)
          .NotNull()
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["PosTotal"]])

          .GreaterThanOrEqualTo(0)
          .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["PosTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NakitToplam)
         .NotNull()
         .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["CashTotal"]])

         .GreaterThanOrEqualTo(0)
         .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["CashTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BankaToplam)
         .NotNull()
         .WithMessage(localizer[BlazorProjectDomainErrorCodes.Required, localizer["BankTotal"]])

         .GreaterThanOrEqualTo(0)
         .WithMessage(localizer[BlazorProjectDomainErrorCodes.GreaterThanOrEqual, localizer["BankTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Aciklama)
           .MaximumLength(EntityConsts.MaxAciklamaLength)
           .WithMessage(localizer[BlazorProjectDomainErrorCodes.MaxLenght, localizer["Description"], EntityConsts.MaxAciklamaLength]);

        ////----MakbuzHareket'i validate etmek için.
        RuleForEach(x => x.MakbuzHareketler)
            .SetValidator(y => new MakbuzHareketDtoValidator(localizer));//MakbuzHareketDtoValidator sınıfına gidip localizer'ı çalıştır denir.
    }

}
