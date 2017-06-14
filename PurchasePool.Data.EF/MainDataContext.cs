using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PurchasePool.Data.EF.Entities;
using PurchasePool.Data.EF.Interfaces;
using PurchasePool.Data.EF.Initializers;

namespace PurchasePool.Data.EF
{
    public class MainDataContext : DbContext, IMainContainer
    {
        public MainDataContext(): base("MainData")
        {
           if(!Database.Exists())
            {
                Database.SetInitializer(new CreateDatabaseInitializer());
            }
            else
            {
                Database.SetInitializer(new NormalInitializer());
            }
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Database.Initialize(false);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<CategoryGoodReference> CategoryGoodReferences { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);
            modelBuilder.Entity<Category>().Property(e => e.Description)
                .IsUnicode()
                .IsMaxLength();

            modelBuilder.Entity<Good>().Property(e => e.Name)
                 .IsRequired()
                 .IsUnicode()
                 .HasMaxLength(255);
            modelBuilder.Entity<Good>().Property(e => e.Description)                 
                 .IsUnicode()
                 .HasMaxLength(1024);
            modelBuilder.Entity<Good>().Property(e => e.WebLink)
                 .IsRequired()
                 .IsUnicode()
                 .HasMaxLength(512);
        }

        public override int SaveChanges()
        {
            BeforeSaving();
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync()
        {
            BeforeSaving();
            return await base.SaveChangesAsync();
        }

        private void BeforeSaving()
        {
            if (ChangeTracker.HasChanges())
            {
                var now = DateTime.UtcNow;
                var entities = ChangeTracker.Entries<Entity>()
                    .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    .Select(entry => entry.Entity);
                entities.ToList().ForEach(entity => {
                    entity.DateCreate = now;
                    if (entity.Id == null || entity.Id == Guid.Empty)
                    {
                        entity.Id = Guid.NewGuid();
                    }
                });
            }
        }
    }
}
