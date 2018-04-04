using System.Data.Entity;
using Witcherius.Entities;
using Witcherius.Initializers;

namespace Witcherius
{
    public class WitcheriusDbContext : DbContext
    {
        public WitcheriusDbContext() : base("WitcheriusDb")
        {
            Database.SetInitializer(new WitcheriusDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<MiniLocation> MiniLocations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<InventoryItems> InventoryItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Relationships
            modelBuilder.Entity<InventoryItems>()
                .HasRequired(t => t.Inventory)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.InventoryId);

            modelBuilder.Entity<InventoryItems>()
                .HasRequired(t => t.Item)
                .WithMany(t => t.Inventories)
                .HasForeignKey(t => t.ItemId);
        }
    }
}
