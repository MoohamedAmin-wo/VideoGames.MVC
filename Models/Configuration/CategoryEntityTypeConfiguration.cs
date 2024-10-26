namespace NGINX.Models.Configuration
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
            {
                new Category {Id =15 , Name = "Sprorts"},
                new Category {Id =16 , Name = "Action"},
                new Category {Id =17 , Name = "Adventure"},
                new Category {Id =18 , Name = "Racing"},
                new Category {Id =19 , Name = "Zombies"},
                new Category {Id =20 , Name = "Film"},
                new Category {Id =21 , Name = "War"},
                new Category {Id =22 , Name = "Shooting"},
            });
        }
    }
}
