namespace NGINX.Models.Configuration
{
    public class GameDeviceEntityTypeConfiguration : IEntityTypeConfiguration<GameDevice>
    {
        public void Configure(EntityTypeBuilder<GameDevice> builder)
        {
            builder.HasKey(k=> new {k.GameId , k.DeviceId});
        }
    }
}
