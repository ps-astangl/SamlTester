using Microsoft.EntityFrameworkCore;

namespace SAMLTester.Context
{
    public class ApplicationDbContext : DbContext, IRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<PartnerServiceProviderConfigurations> PartnerServiceProviderConfigurations { get; set; }
    }


    public interface IRepository
    {
    }
}