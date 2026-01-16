using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;


namespace BookClub.Tests.Data
{
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Can_Create_DbContext()
        {
            // Arrange & Act
            using var context = GetDbContext();

            // Assert
            Assert.NotNull(context);
            Assert.NotNull(context.Discussions);
        }

        [Fact]
        public void Can_Add_Discussion_To_Database()
        {
            // Arrange
            using var context = GetDbContext();

            var discussion = new Discussion
            {
                Title = "Test Discussion",
                Description = "Test Description"
            };

            // Act
            context.Discussions.Add(discussion);
            context.SaveChanges();

            // Assert
            var savedDiscussion = context.Discussions.FirstOrDefault();

            Assert.NotNull(savedDiscussion);
            Assert.Equal("Test Discussion", savedDiscussion.Title);
            Assert.Equal("Test Description", savedDiscussion.Description);
        }

        [Fact]
        public void Title_Is_Required_And_Has_MaxLength_200()
        {
            using var context = GetDbContext();

            var entityType = context.Model.FindEntityType(typeof(Discussion));
            var titleProperty = entityType.FindProperty(nameof(Discussion.Title));

            Assert.False(titleProperty.IsNullable);
            Assert.Equal(200, titleProperty.GetMaxLength());
        }

        [Fact]
        public void Description_Is_Required()
        {
            using var context = GetDbContext();

            var entityType = context.Model.FindEntityType(typeof(Discussion));
            var descriptionProperty = entityType.FindProperty(nameof(Discussion.Description));

            Assert.False(descriptionProperty.IsNullable);
        }

        [Fact]
        public void CreatedAt_Has_Default_Value()
        {
            using var context = GetDbContext();

            var entityType = context.Model.FindEntityType(typeof(Discussion));
            var createdAtProperty = entityType.FindProperty(nameof(Discussion.CreatedAt));

            Assert.NotNull(createdAtProperty.GetDefaultValueSql());
            Assert.Equal("SYSUTCDATETIME()", createdAtProperty.GetDefaultValueSql());
        }
    }
}
