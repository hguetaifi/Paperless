using Microsoft.EntityFrameworkCore;
using Paperless.DataAccessLayer.Entities;

namespace Paperless.DataAccessLayer.Sql;

public class DBContext : DbContext
{
    public DbSet<DocumentDTO> Documents { get; set; }
    public DBContext() { }
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    
    //Override Configuration of DbContext
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var isConfigured = optionsBuilder.IsConfigured;
        if (!isConfigured)
        {
            optionsBuilder.UseNpgsql("Host=paperless-db;Username=root;Password=123;Database=paperless");
        }
    }
}