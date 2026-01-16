using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BookClub.IntegrationTests.Data
{
    public class ApplicationDbContextIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ApplicationDbContextIntegrationTests(
            CustomWebApplicationFactory factory)
        {
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [Fact]
        public void Can_Insert_And_Read_Discussion_From_Real_Database()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var discussion = new Discussion
            {
                Title = "EF Core integration test",
                Description = "Stored in real SQL Server"
            };

            db.Discussions.Add(discussion);
            db.SaveChanges();

            var fromDb = db.Discussions.Single(d => d.Id == discussion.Id);

            Assert.Equal(discussion.Title, fromDb.Title);
            Assert.Equal(discussion.Description, fromDb.Description);
            Assert.True(fromDb.CreatedAt > DateTime.MinValue);
        }

        [Fact]
        public void CreatedAt_Is_Set_By_Database_Default()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var discussion = new Discussion
            {
                Title = "Default date test",
                Description = "CreatedAt from DB"
            };

            db.Discussions.Add(discussion);
            db.SaveChanges();

            Assert.NotEqual(default, discussion.CreatedAt);
        }

        [Fact]
        public void Title_MaxLength_Constraint_Is_Enforced()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var discussion = new Discussion
            {
                Title = new string('A', 201),
                Description = "Too long title"
            };

            db.Discussions.Add(discussion);

            Assert.Throws<DbUpdateException>(() => db.SaveChanges());
        }

        [Fact]
        public void Description_Is_Required()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var discussion = new Discussion
            {
                Title = "Missing description"
            };

            db.Discussions.Add(discussion);

            Assert.Throws<DbUpdateException>(() => db.SaveChanges());
        }
    }
}