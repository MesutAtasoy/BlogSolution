using Blog.Domain.Models;
using BlogSolution.Framework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Persistance
{
    //ToDo : Function will be completed. 
    public class BlogDbContextInitializer : IBlogDbContextInitializer
    {
        private readonly BlogDbContext _context;
        private readonly AppSettings appSettings;
        public BlogDbContextInitializer(BlogDbContext context, IOptions<AppSettings> options)
        {
            appSettings = options.Value;
            _context = context;
        }
        public async Task InitializeAsync()
        {
            if (!appSettings.UseCustomizationData) return;
            await SeedEverything(_context);
        }

        public async Task SeedEverything(BlogDbContext context)
        {

            if (appSettings.ApplyDbMigrations)
                context.Database.Migrate();
            else
                context.Database.EnsureCreated();

            //Seed Data Functions  
            await SeedCategory();
        }

        public async Task SeedCategory()
        {
            List<Category> categories = new List<Category>
            {
                new Category{Id = Guid.NewGuid(), Name = "Science", Description = "Science", CreatedBy = default(Guid), CreatedDate = DateTime.Now,IsActive = true,IsDeleted = false},
                new Category{Id = Guid.NewGuid(), Name = "Travel", Description = "Travel", CreatedBy = default(Guid), CreatedDate = DateTime.Now,IsActive = true,IsDeleted = false},
                new Category{Id = Guid.NewGuid(), Name = "Music", Description = "Music", CreatedBy = default(Guid) , CreatedDate = DateTime.Now,IsActive = true,IsDeleted = false},
                new Category{Id = Guid.NewGuid(), Name = "Movie", Description = "Movie", CreatedBy = default(Guid) , CreatedDate = DateTime.Now,IsActive = true,IsDeleted = false},
                new Category{Id = Guid.NewGuid(), Name = "Sport", Description = "Sport", CreatedBy = default(Guid) , CreatedDate = DateTime.Now,IsActive = true,IsDeleted = false}
            };

            await _context.Categories.AddRangeAsync(categories);
            _context.SaveChanges();
        }
    }
}
