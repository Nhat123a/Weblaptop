using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.Models;

namespace Shopthoitrang.DataContext
{
    public class Datacontext:IdentityDbContext<AppUserModel>
    {
       public Datacontext(DbContextOptions<Datacontext> options):base(options) { 
        
        }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<CategoryModelcs> Categories { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<BannerModels> Banner { get; set; }
    }
}
