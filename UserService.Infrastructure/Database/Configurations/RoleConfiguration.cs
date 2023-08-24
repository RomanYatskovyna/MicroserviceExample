﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UserService.Domain.Entities;
using UserService.Infrastructure.Database;

#nullable disable

namespace UserService.Infrastructure.Database.Configurations
{
    public partial class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(e => e.Name);

            entity.Property(e => e.Name).HasMaxLength(50);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Role> entity);
    }
}
