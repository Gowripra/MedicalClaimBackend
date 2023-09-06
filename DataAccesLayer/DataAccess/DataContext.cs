using DataAccesLayer.Models;
using Medical_Claim.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Claim.DataAccess
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<PolicyHolder> policyHolders { get; set; }
        public DbSet<ClaimProcessRegistration> claimProcessRegistrations { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Claims> claims { get; set; }
        
    }
}
