using ApacheLogParserProject.Data.Entities.KeylessEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApacheLogParserProject.Data.Configuration
{
    public class HostInfoConfiguration : IEntityTypeConfiguration<HostInfo>
    {
        public void Configure(EntityTypeBuilder<HostInfo> builder)
        {
            builder.HasNoKey().ToView(null);
        }
    }
}