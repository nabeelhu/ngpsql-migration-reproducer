// See https://aka.ms/new-console-template for more information
using Migration.Reproducer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Xunit;
Console.WriteLine("Hello, World!");







public class AppDbContext : DbContext
{
    public DbSet<MyEntity> MyEntities { get; set; }
    public DbSet<ActiveEntity> ActiveEntities { get; set; }
    public DbSet<InactiveEntity> InactiveEntities { get; set; }

    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<StatusType>();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=test_db;User Id=postgres;Password=password;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("test_schema");
        modelBuilder.HasPostgresEnum<StatusType>();

        modelBuilder.Entity<MyEntity>()
            .HasDiscriminator<StatusType>("status_type")
        .HasValue<ActiveEntity>(StatusType.Active)
        .HasValue<InactiveEntity>(StatusType.Inactive);
    }
}


public class MyEntityRepository
{
    private readonly AppDbContext _dbContext;

    public MyEntityRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(MyEntity entity)
    {
        _dbContext.MyEntities.Add(entity);
        _dbContext.SaveChanges();
    }

    public MyEntity GetById(int id)
    {
        return _dbContext.MyEntities.Find(id);
    }

    public IQueryable<MyEntity> GetAll()
    {
        return _dbContext.MyEntities.AsQueryable();
    }
    public IQueryable<MyEntity> GetAllActive()
    {
        return _dbContext.ActiveEntities.AsQueryable();
    }
    public IQueryable<MyEntity> GetAllInactive()
    {
        return _dbContext.InactiveEntities.AsQueryable();
    }

    public void Update(MyEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _dbContext.MyEntities.Find(id);
        if (entity != null)
        {
            _dbContext.MyEntities.Remove(entity);
            _dbContext.SaveChanges();
        }
    }

}