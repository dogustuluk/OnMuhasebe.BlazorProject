using System.Data;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.BankaHesaplar;
using OnMuhasebe.BlazorProject.Bankalar;
using OnMuhasebe.BlazorProject.BankaSubeler;
using OnMuhasebe.BlazorProject.Birimler;
using OnMuhasebe.BlazorProject.Cariler;
using OnMuhasebe.BlazorProject.Consts;
using OnMuhasebe.BlazorProject.Depolar;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OnMuhasebe.BlazorProject.Configurations;
public static class BlazorProjectDbContextModelBuilderExtensions
{
    public static void Configure(this ModelBuilder builder)
    {
        builder.Entity<Banka>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Bankalar", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->

            //indexes->

            //relations->
        });
        }
    public static void ConfigureBanka(this ModelBuilder builder)
    {
        builder.Entity<Banka>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Bankalar", BlazorProjectConsts.DbSchema);//blazorProjectConst ile oluşturulan tablonun önünde hangi string değerin geleceğini belirtir. burada App olarak verilmiş, isteğe bağlı olarak o sınıftan bu isim değiştirilebilir.
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);

            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());

            //indexes->
            b.HasIndex(x => x.Kod);//kod alanına göre sıralama hem de arama yaptırabiliriz. Dolayısıyla herhangi bir tipe göre benzeri işlemler yapıyorsak index'lememiz gerekmektedir performans açısından.

            //relations->
            b.HasOne(x => x.OzelKod1)
            .WithMany(x => x.OzelKod1Bankalar)
            .OnDelete(DeleteBehavior.NoAction);//noAction ->eğer banka entity'si silinirse ozelKod1'i silmesin. daha sonra aynı banka tekrar tanımlandığı zaman orada tanımlanmış olan(ozelKodlar'da tanımlanmış olan) ozelKodu kullanabilsin diye.
            b.HasOne(x => x.OzelKod2)
            .WithMany(x => x.OzelKod2Bankalar)
            .OnDelete(DeleteBehavior.NoAction);

        });
    }
    public static void ConfigureBankaSube(this ModelBuilder builder)
    {
        builder.Entity<BankaSube>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "BankaSubeler", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);
            
            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);

            b.Property(x => x.BankaId)
                .IsRequired()//yapmaya gerek yoktur.entity sınıfında zaten zorunlu olduğunu belirttik.
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);

            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
            //indexes->
            b.HasIndex(x => x.Kod);
            //relations->
            b.HasOne(x => x.Banka)
                .WithMany(x => x.BankaSubeler)
                .OnDelete(DeleteBehavior.Cascade);//cascade olması ->bankaya bağlı şubelerin de silinmesi lazım. hard delete durumda geçerlidir. bu projede hard delete kullanmıyoruz, yani bu tanımlama şuanki projede geçerli olmayacaktır çünkü soft delete işlemi uyguluyor olucaz.
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1BankaSubeler)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2BankaSubeler)
                .OnDelete(DeleteBehavior.NoAction);
            //b.HasOne<Banka>().WithMany().HasForeignKey(x => x.BankaId);//generic yapıyı eğer navigation property tanımlamazsak kullanırız.
        });
    }
    public static void ConfigureBankaHesap(this ModelBuilder builder)
    {
        builder.Entity<BankaHesap>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "BankaHesaplar", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);
            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);
            b.Property(x => x.HesapTuru)
                .IsRequired()
                .HasColumnType(SqlDbType.TinyInt.ToString());
            b.Property(x => x.HesapNo)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(BankaHesapConsts.MaxHesapNoLength);
            b.Property(x => x.IbanNo)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(BankaHesapConsts.MaxIbanNoLength);
            b.Property(x => x.BankaSubeId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.SubeId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
            //indexes->
            b.HasIndex(x => x.Kod);
            //relations->
            b.HasOne(x => x.BankaSube)
                .WithMany(x => x.BankaHesaplar)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1BankaHesaplar)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2BankaHesaplar)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Sube)
                .WithMany(x => x.BankaHesaplar)
                .OnDelete(DeleteBehavior.NoAction);

        });
    }
    public static void ConfigureBirim(this ModelBuilder builder)
    {
        builder.Entity<Birim>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Birimler", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);
            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);
            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
            //indexes->
            b.HasIndex(x => x.Kod);
            //relations->
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Birimler)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Birimler)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureCari(this ModelBuilder builder)
    {
        builder.Entity<Cari>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Cariler", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);
            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);
            b.Property(x => x.VergiDairesi)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(CariConsts.MaxVergiDairesiLength);
            b.Property(x => x.VergiNo)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(CariConsts.MaxVergiNoLength);
            b.Property(x => x.Telefon)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxTelefonLength);
            b.Property(x => x.Adres)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdresLength);
            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
            //indexes->
            b.HasIndex(x => x.Kod);
            //relations->
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Cariler)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Cariler)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureDepo(this ModelBuilder builder)
    {
        builder.Entity<Depo>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Depolar", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.Kod)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxKodLength);
            b.Property(x => x.Ad)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAdLength);
            b.Property(x => x.OzelKod1)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.OzelKod2)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.Sube)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
            //indexes->
            b.HasIndex(x => x.Kod);
            //relations->
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Depolar)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Depolar)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Sube)
                .WithMany(x => x.Depolar)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }


}