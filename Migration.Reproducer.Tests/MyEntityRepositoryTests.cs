using Migration.Reproducer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using System;

namespace Migration.Reproducer.Tests
{
    public class MyEntityRepositoryTests: IDisposable
    {
          

        public MyEntityRepositoryTests()
        {
           
            using (var dbContext = new AppDbContext())
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
            using (var dbContext = new AppDbContext())
            {
                dbContext.MyEntities.ExecuteDelete();
            }
        }
        [Fact]
        public void CrudOperation_Success()
        {
            // Arrange
      

            using (var dbContext = new AppDbContext())
            {
                var repository = new MyEntityRepository(dbContext);

                // Act
                repository.Add(new ActiveEntity { Id = 1, Name = "TestEntity 1" });
                repository.Add(new InactiveEntity { Id = 2, Name = "TestEntity 2", status = StatusType.Inactive });
                repository.Add(new ActiveEntity { Id = 3, Name = "TestEntity 3", status = StatusType.Active });

                // Assert
                var entity = repository.GetById(1);
                
                Assert.NotNull(repository.GetAllActive());
                Assert.Equal("TestEntity 1", entity.Name);
                repository.GetAllActive().Count();
                dbContext.MyEntities.RemoveRange(dbContext.MyEntities.ToList());
                //Assert.Equal(3, repository.GetAll().Count());
                //Assert.Equal(2, repository.GetAllActive().Count());
                //Assert.Equal(1, repository.GetAllInactive().Count());

            }
        }

        [Fact]
        public void CrudOperation_Success2()
        {
            // Arrange


            using (var dbContext = new AppDbContext())
            {
                var repository = new MyEntityRepository(dbContext);

                // Act
                repository.Add(new ActiveEntity { Id = 5, Name = "TestEntity 5" });
                repository.Add(new InactiveEntity { Id = 6, Name = "TestEntity 9", status = StatusType.Inactive });
                repository.Add(new ActiveEntity { Id = 9, Name = "TestEntity 22", status = StatusType.Active });

                // Assert
                var entity = repository.GetById(5);

                Assert.NotNull(repository.GetAllActive());
                Assert.Equal("TestEntity 5", entity.Name);
                //Assert.Equal(, repository.GetAll().Count());
                //Assert.Equal(2, repository.GetAllActive().Count());
                //Assert.Equal(1, repository.GetAllInactive().Count());

            }
        }
    }
}
