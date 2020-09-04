using ApacheLogParserProject.Data.Entities.KeylessEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApacheLogParserProject.Data.Configuration
{
    public class RouteInfoConfiguration : IEntityTypeConfiguration<RouteInfo>
    {
        public void Configure(EntityTypeBuilder<RouteInfo> builder)
        {
            builder.HasNoKey().ToView(null);
        }
    }
}