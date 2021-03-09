using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SAMLTester.Context
{
    public class ApplicationDbContext : DbContext, IRepository
    {
        public const string NewGuidSql = "NEWID()";
        public const string GetDateSql = "GETDATE()";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity) entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity) entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public virtual DbSet<PartnerServiceProviderConfiguration> PartnerServiceProviderConfigurations { get; set; }
        public virtual DbSet<PartnerCertificate> PartnerCertificates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PartnerServiceProviderConfigurationMap());
            builder.ApplyConfiguration(new PartnerCertificateMap());
        }

        public Task<int> CreateConfiguration(PartnerServiceProviderConfiguration configuration)
        {
            PartnerServiceProviderConfigurations.Add(configuration);
            return SaveChangesAsync();
        }

        public Task<List<PartnerServiceProviderConfiguration>> ListAllConfigurations()
        {
            return PartnerServiceProviderConfigurations.Select(x => x).ToListAsync();
        }
    }

    public class PartnerCertificateMap : IEntityTypeConfiguration<PartnerCertificate>
    {
        public void Configure(EntityTypeBuilder<PartnerCertificate> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasDefaultValueSql(ApplicationDbContext.NewGuidSql);

            builder
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql(ApplicationDbContext.GetDateSql);

            builder
                .Property(x => x.UpdatedDate)
                .HasDefaultValueSql(ApplicationDbContext.GetDateSql);
        }
    }

    public class PartnerServiceProviderConfigurationMap : IEntityTypeConfiguration<PartnerServiceProviderConfiguration>
    {
        public void Configure(EntityTypeBuilder<PartnerServiceProviderConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasDefaultValueSql(ApplicationDbContext.NewGuidSql);

            builder
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql(ApplicationDbContext.GetDateSql);

            builder
                .Property(x => x.UpdatedDate)
                .HasDefaultValueSql(ApplicationDbContext.GetDateSql);

            builder
                .HasMany(x => (IEnumerable<PartnerCertificate>) x.PartnerCertificates)
                .WithOne(x => x.PartnerServiceProviderConfiguration)
                .HasForeignKey(x => x.PartnerServiceProviderConfigurationId);
        }
    }


    public interface IRepository
    {
        public Task<int> CreateConfiguration(PartnerServiceProviderConfiguration configuration);
        public Task<List<PartnerServiceProviderConfiguration>> ListAllConfigurations();
    }
}