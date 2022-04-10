﻿using Blog.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Blog.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();


            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);

            #region AdminUser
            var adminUser = new User
            {
                Id = 1, // Identity yapısını string olarak kullanıyorsak guid ile yapmamız uygun olur
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "adminuser@gmail.com",
                NormalizedEmail = "ADMINUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultuser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString() // ToString("D") yaparsanız '-' ifadesi eklenir
            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");
            #endregion

            #region EditorUser
            var editorUser = new User
            {
                Id = 2, // Identity yapısını string olarak kullanıyorsak guid ile yapmamız uygun olur
                UserName = "editoruser",
                NormalizedUserName = "EDITORUSER",
                Email = "editoruser@gmail.com",
                NormalizedEmail = "EDITORUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultuser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editorUser.PasswordHash = CreatePasswordHash(editorUser, "editoruser");
            #endregion

            builder.HasData(adminUser, editorUser);


            #region Guid Kullanımı
            /*
                Guid.NewGuid().ToString() // Doğru Kullanım
                "c83b3047-1c4b-4087-9849-83750a1cab36"
                
                new Guid().ToString() // Yanlış Kullanım
                "00000000-0000-0000-0000-000000000000"
             */
            #endregion
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}