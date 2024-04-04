using Microsoft.EntityFrameworkCore;
using Farm.Models;

namespace Farm.Models.Data
{

    public class FarmDbContext : DbContext
    {
        public FarmDbContext(DbContextOptions<FarmDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.Entity<OpCoClientInfo>().HasKey(u => new 
            // { 
            //     u.Id, 
            //     u.OfficialCompanyName 
            // });
        }
        public DbSet<Authorisation> Authorisation { get; set; }

        public DbSet<Broadcast_Message> Broadcast_Message { get; set; }
        public DbSet<Campaign_Types> Campaign_Types { get; set; }
        public DbSet<Campaigns> Campaigns { get; set; }
        public DbSet<Crop_Variety> Crop_Variety { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Document_Type> Document_Type { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Employee_Roles> Employee_Roles { get; set; }
        public DbSet<Employee_Types> Employee_Types { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Farm_Diseases> Farm_Diseases { get; set; }
        public DbSet<Farmer_Group> Farmer_Group { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Farmer_login_visit_logs> Farmer_login_visit_logs { get; set; }
        public DbSet<Farmer_trip_sheets> Farmer_trip_sheets { get; set; }
        public DbSet<Farmers> Farmers { get; set; }
        public DbSet<Farmers_Login> Farmers_Login { get; set; }
        public DbSet<FarmField> FarmField { get; set; }
        public DbSet<Field_Visit> Field_Visit { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Logins_Log> Logins_Log { get; set; }
        public DbSet<Mandal_Blocks> Mandal_Blocks { get; set; }
        public DbSet<Nursary> Nursary { get; set; }
        public DbSet<Nursary_Batches> Nursary_Batches { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<PlantationIdentification> PlantationIdentification { get; set; }
        public DbSet<Poaching_FFB> Poaching_FFB { get; set; }
        public DbSet<Privileges> Privileges { get; set; }
        public DbSet<Referral_Source> Referral_Source { get; set; }
        public DbSet<Role_Privileges> Role_Privileges { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<States> States { get; set; }
        public DbSet<Training_Videos> Training_Videos { get; set; }
        public DbSet<Villages> Villages { get; set; }
        public DbSet<Workflow> Workflow { get; set; }
        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                // E.Property("CreatedDate").CurrentValue = DateTime.Now;
                // E.Property("CreatedBy").CurrentValue = 1;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                // E.Property("UpdatedDate").CurrentValue = DateTime.Now;
                // E.Property("UpdatedBy").CurrentValue = 1;
            });

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                // E.Property("CreatedDate").CurrentValue = DateTime.Now;
                // E.Property("CreatedBy").CurrentValue = 1;
                // E.Property("Guid").CurrentValue = Guid.NewGuid().ToString();
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                // E.Property("UpdatedDate").CurrentValue = DateTime.Now;
                // E.Property("UpdatedBy").CurrentValue = 1;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
