
using BlogSolution.Authentication.Password;
using BlogSolution.Types.Settings;
using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Persistance
{
    public class IdentityDbContextInitilizer : IIdentityDbContextInitilizer
    {
        private readonly IdentityDbContext dbContext;
        private readonly AppSettings appSettings;
        public IdentityDbContextInitilizer(IdentityDbContext context, IOptions<AppSettings> options)
        {
            appSettings = options.Value;
            dbContext = context;
        }
        public async Task InitializeAsync()
        {
            if (!appSettings.UseCustomizationData) return;

            await SeedEverything(dbContext);
        }

        public async Task SeedEverything(IdentityDbContext context)
        {

            if (appSettings.ApplyDbMigrations)
                context.Database.Migrate();
            else
                context.Database.EnsureCreated();

            //Seed Data Functions  
            await SeedUser(context);
        }

        public async Task SeedUser(IdentityDbContext context)
        {
            var password = new PasswordHasher("123456");

            var userList = new List<User>
            {
                new User {Id = Guid.NewGuid(),CreatedBy = default(Guid), CreatedDate = DateTime.Now, Email = "admin@mail.com",IsActive = true,IsDeleted = false ,Name = "admin",PasswordHash = password.Hash,PasswordSalt = password.Salt,Username = "admin"},
                new User {Id = Guid.NewGuid(),CreatedBy = default(Guid), CreatedDate = DateTime.Now, Email = "user@mail.com",IsActive = true,IsDeleted = false ,Name = "user",PasswordHash = password.Hash,PasswordSalt = password.Salt,Username = "user"}
            };
            await context.Users.AddRangeAsync(userList);
            context.SaveChanges();
        }
    }
}
