namespace NGINX.Models.Configuration
{
    public class DeviceiesEntityTypeConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasData(new Device[]
            {
                new Device { Id = 1, Name = "PlayStation" , Icon = "bi bi-playstation"},
                new Device { Id = 2, Name = "XBox" , Icon = "bi bi-xbox"},
                new Device { Id = 3, Name = "Nintendo" , Icon = "bi bi-nintendo"},
                new Device { Id = 4, Name = "PC" , Icon = "bi bi-pc-display"},
            });
        }
    }
}
