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
using OnMuhasebe.BlazorProject.Hizmetler;
using OnMuhasebe.BlazorProject.Kasalar;
using OnMuhasebe.BlazorProject.Makbuzlar;
using OnMuhasebe.BlazorProject.Masraflar;
using OnMuhasebe.BlazorProject.OzelKodlar;
using OnMuhasebe.BlazorProject.Parametreler;
using OnMuhasebe.BlazorProject.Stoklar;
using OnMuhasebe.BlazorProject.Subeler;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace OnMuhasebe.BlazorProject.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BlazorProjectDbContext :
    AbpDbContext<BlazorProjectDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    //dbset ekleme. FaturaHareketler ve MakbuzHareketler AggregateRoot olmadığı için eklemiyoruz. Buraya sadece AggreateRoot olanlar eklenir.
    public DbSet<Banka> Bankalar { get; set; }
    public DbSet<BankaSube> BankaSubeler { get; set; }
    public DbSet<BankaHesap> BankaHesaplar { get; set; }
    public DbSet<Birim> Birimler { get; set; }
    public DbSet<Cari> Cariler { get; set; }
    public DbSet<Depo> Depolar { get; set; }
    public DbSet<Donem> Donemler { get; set; }
    public DbSet<FirmaParametre> FirmaParametreler { get; set; }
    public DbSet<Fatura> Faturalar { get; set; }
    public DbSet<Hizmet> Hizmetler { get; set; }
    public DbSet<Kasa> KasaHareketler { get; set; }
    public DbSet<Makbuz> Makbuzlar { get; set; }
    public DbSet<Masraf> Masraflar { get; set; }
    public DbSet<OzelKod> OzelKodlar { get; set; }
    public DbSet<Stok> Stoklar { get; set; }
    public DbSet<Sube> Subeler { get; set; }

    public BlazorProjectDbContext(DbContextOptions<BlazorProjectDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

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
