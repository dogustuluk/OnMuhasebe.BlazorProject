﻿using System.Data;
using Microsoft.EntityFrameworkCore;
using OnMuhasebe.BlazorProject.Bankalar;
using OnMuhasebe.BlazorProject.Consts;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OnMuhasebe.BlazorProject.Configurations;
public static class BlazorProjectDbContextModelBuilderExtensions
{
    public static void ConfigureBanka(this ModelBuilder builder)
    {
        builder.Entity<Banka>(b =>
        {
            b.ToTable(BlazorProjectConsts.DbTablePrefix + "Bankalar", BlazorProjectConsts.DbSchema);//blazorProjectConst ile oluşturulan tablonun önünde hangi string değerin geleceğini belirtir. burada App olarak verilmiş, isteğe bağlı olarak o sınıftan bu isim değiştirilebilir.
            b.ConfigureByConvention(); //auto configure for the base class props
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
}
