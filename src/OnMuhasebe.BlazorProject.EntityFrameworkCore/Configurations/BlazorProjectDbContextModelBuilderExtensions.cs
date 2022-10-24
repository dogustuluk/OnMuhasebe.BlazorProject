﻿using System.Data;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.BankaHesaplar;
using OnMuhasebe.BlazorProject.Bankalar;
using OnMuhasebe.BlazorProject.BankaSubeler;
using OnMuhasebe.BlazorProject.Birimler;
using OnMuhasebe.BlazorProject.Cariler;
using OnMuhasebe.BlazorProject.Consts;
using OnMuhasebe.BlazorProject.Depolar;
using OnMuhasebe.BlazorProject.Donemler;
using OnMuhasebe.BlazorProject.Faturalar;
using OnMuhasebe.BlazorProject.Parametreler;
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
    public static void ConfigureDonem(this ModelBuilder builder)
    {
        builder.Entity<Donem>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Donemler", BlazorProjectConsts.DbSchema);
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
         
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
         
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
         
            //indexes->
            b.HasIndex(x => x.Kod);
           
            //relations->
            //burada bire-çok olan ilişkileri tanımlıyoruz. bu sınıfta çoka-bir olan ilişkiler olduğu için herhangi bir tanımlama yapmıyoruz çünkü bire-çok olan ilişkilerin tersini otamatik olarak algılıyor sistem.
        });
    }
    public static void ConfigureFatura(this ModelBuilder builder)
    {
        builder.Entity<Fatura>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Faturalar", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.FaturaTuru)
                .IsRequired()
                .HasColumnType(SqlDbType.TinyInt.ToString());
         
            b.Property(x => x.FaturaNo)
                .IsRequired()
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(FaturaConsts.MaxFaturaNoLength);
         
            b.Property(x => x.Tarih)
                .IsRequired()
                .HasColumnType(SqlDbType.Date.ToString());
         
            b.Property(x => x.BrutTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
         
            b.Property(x => x.IndirimTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
         
            b.Property(x => x.KdvHaricTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
         
            b.Property(x => x.KdvTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
       
            b.Property(x => x.NetTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
      
            b.Property(x => x.HareketSayisi)
                .IsRequired()
                .HasColumnType(SqlDbType.Int.ToString());
     
            b.Property(x => x.CariId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
     
            b.Property(x => x.OzelKod1Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
    
            b.Property(x => x.OzelKod2Id)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
     
            b.Property(x => x.SubeId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
       
            b.Property(x => x.DonemId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
        
            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
        
            b.Property(x => x.Durum)
                .HasColumnType(SqlDbType.Bit.ToString());
         
            //indexes->
            b.HasIndex(x => x.FaturaNo);
         
            //relations->
            b.HasOne(x => x.Cari)
                .WithMany(x => x.Faturalar)
                .OnDelete(DeleteBehavior.NoAction);
        
            b.HasOne(x => x.OzelKod1)
                .WithMany(x => x.OzelKod1Faturalar)
                .OnDelete(DeleteBehavior.NoAction);
        
            b.HasOne(x => x.OzelKod2)
                .WithMany(x => x.OzelKod2Faturalar)
                .OnDelete(DeleteBehavior.NoAction);
        
            b.HasOne(x => x.Sube)
                .WithMany(x => x.Faturalar)
                .OnDelete(DeleteBehavior.NoAction);
         
            b.HasOne(x => x.Donem)
                .WithMany(x => x.Faturalar)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureFaturaHareket(this ModelBuilder builder)
    {
        builder.Entity<FaturaHareket>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "FaturaHareketler", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.FaturaId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            
            b.Property(x => x.HareketTuru)
                .IsRequired()
                .HasColumnType(SqlDbType.TinyInt.ToString());
            
            b.Property(x => x.StokId)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            
            b.Property(x => x.HizmetId)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            
            b.Property(x => x.MasrafId)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            
            b.Property(x => x.DepoId)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            
            b.Property(x => x.Miktar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.BirimFiyat)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.BrutTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.IndirimTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.KdvOrani)
                .IsRequired()
                .HasColumnType(SqlDbType.Int.ToString());
            
            b.Property(x => x.KdvHaricTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.KdvTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());
            
            b.Property(x => x.NetTutar)
                .IsRequired()
                .HasColumnType(SqlDbType.Money.ToString());

            b.Property(x => x.Aciklama)
                .HasColumnType(SqlDbType.VarChar.ToString())
                .HasMaxLength(EntityConsts.MaxAciklamaLength);
            
            //indexes->

            //relations->
            b.HasOne(x => x.Fatura)
                .WithMany(x => x.FaturaHareketler)
                .OnDelete(DeleteBehavior.Cascade);
        
            b.HasOne(x => x.Stok)
                .WithMany(x => x.FaturaHareketler)
                .OnDelete(DeleteBehavior.NoAction);
           
            b.HasOne(x => x.Hizmet)
                .WithMany(x => x.FaturaHareketler)
                .OnDelete(DeleteBehavior.NoAction);
           
            b.HasOne(x => x.Masraf)
                .WithMany(x => x.FaturaHareketler)
                .OnDelete(DeleteBehavior.NoAction);
         
            b.HasOne(x => x.Depo)
                .WithMany(x => x.FaturaHareketler)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public static void ConfigureFirmaParametre(this ModelBuilder builder)
    {
        builder.Entity<FirmaParametre>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "FirmaParametreler", BlazorProjectConsts.DbSchema);
            b.ConfigureByConvention();

            //properties->
            b.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.SubeId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.DonemId)
                .IsRequired()
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());

            b.Property(x => x.DepoId)
                .HasColumnType(SqlDbType.UniqueIdentifier.ToString());
            //indexes->

            //relations->
            //user ile firmaparametre arasında bire-bir ilişki vardır. Burada generic yapı kullanmamızın sebebi -> abp framework tarafından sağlanan IdentityUser ile buradaki FirmaParametre arasında bir navigation property olmamasından dolayı. Yani burada FirmaParametre ile identityUser arasında bir navigation property'si var ancak IdentityUser'dan bizim FirmaParametre'ye navigation property'si yok. bundan dolayı generic yapı kullanırız. 
            b.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<FirmaParametre>(x => x.UserId);

            b.HasOne(x => x.Sube)
                .WithMany(x => x.FirmaParametreler)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Donem)
                .WithMany(x => x.FirmaParametreler)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Depo)
                .WithMany(x => x.FirmaParametreler)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}