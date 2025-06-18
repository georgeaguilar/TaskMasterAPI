using Microsoft.EntityFrameworkCore;
using TaskMasterAPI.Models;

namespace TaskMasterAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Task> Tasks { get; set; }

}
