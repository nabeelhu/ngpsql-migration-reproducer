using Migration.Reproducer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using System;

namespace Migration.Reproducer.Tests
{
    public class MyEntityRepositoryTests: IDisposable
    {
        private readonly DbContextOptions<AppDbContext> options;
          

        public MyEntityRepositoryTests()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=test_db;User Id=postgres;Password=password;")
          .Options;
            using (var dbContext = new AppDbContext(options))
            {
                dbContext.Database.Migrate();
                if (dbContext.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
                {
                     npgsqlConnection.Open();
                    npgsqlConnection.ReloadTypes();
                    npgsqlConnection.Close();
                }
            }

        }
        public void Dispose()
        {
            // Dispose of resources, such as the database context
            using (var dbContext = new AppDbContext(options))
            {
                dbContext.Database.EnsureDeleted();
            }
        }
        [Fact]
        public void CrudOperation_Success()
        {
            // Arrange
      

            using (var dbContext = new AppDbContext(options))
            {
                var repository = new MyEntityRepository(dbContext);

                // Act
                repository.Add(new MyEntity { Id = 1, Name = "TestEntity 1", status = StatusType.Active });
                repository.Add(new MyEntity { Id = 2, Name = "TestEntity 2", status = StatusType.Inactive });
                repository.Add(new MyEntity { Id = 3, Name = "TestEntity 3", status = StatusType.Active });

                // Assert
                var entity = repository.GetById(1);
                Assert.NotNull(entity);
                Assert.Equal("TestEntity 1", entity.Name);
                Assert.Equal(3, repository.GetAll().Count());

            }
        }
    }
}
