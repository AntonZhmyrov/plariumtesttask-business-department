﻿using ApacheLogParserProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApacheLogParserProject.Data.Configuration
{
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasIndex(l => l.PublicId).IsUnique();
            builder.Property(l => l.RequestRoute).IsRequired();
            builder.Property(l => l.ClientIpAddress).IsRequired();
        }
    }
}