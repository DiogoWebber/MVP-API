using Microsoft.EntityFrameworkCore;
using mvpAPI.Dtos;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<HistoricoModel> Historicos { get; set; }
}