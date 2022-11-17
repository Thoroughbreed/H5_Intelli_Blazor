using IntelliHouse2000.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelliHouse2000.Services.Database;

public class DBContext : DbContext
{
    public DbSet<LogMessageDTO> Messages { get; set; }
    private IConfiguration _config;


    public DBContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_config["ConnectionStrings:Default"]);
    }
}