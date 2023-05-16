using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext: DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions):base(dbContextOptions) 
        {
            
        }

        public DbSet<Difficulty> Difficulties  { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for difficulties

            var difficulties  =new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("772da223-096e-4dfd-b900-95e22c3de191"),
                    Name="Easy"
                },
                 new Difficulty()
                {
                    Id=Guid.Parse("10e405c3-a390-4094-be7c-91a565fd1829"),
                    Name="Hard"
                },
                  new Difficulty()
                {
                    Id=Guid.Parse("a8c77684-72f2-4e75-9d36-7c96451f03bb"),
                    Name="Medium"
                }
            };

            //seed difficualties to database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // seed data for regions

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id=Guid.Parse("ae8f0b88-3e84-4dde-a30e-e2a8e4634d87") ,
                    Name= "Gharaneej",
                    Code = "GHR",
                    RegionImageUrl= "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE",
                },

                   new Region()
                {
                    Id=Guid.Parse("696e4c25-7c3d-49c9-be38-38fb74c063c9") ,
                    Name= "Albahra",
                    Code = "BHR",
                    RegionImageUrl= "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE",
                },

                      new Region()
                {
                    Id=Guid.Parse("9188e603-975f-4aba-ba34-ff0bdd353d3e") ,
                    Name= "Abo Hammam",
                    Code = "AHMM",
                    RegionImageUrl= "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.seiu1000.org%2Fpost%2Fimage-dimensions&psig=AOvVaw0INXBPxzDdt40oc8apK8LH&ust=1683634630836000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCIim5vPZ5f4CFQAAAAAdAAAAABAE",
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
