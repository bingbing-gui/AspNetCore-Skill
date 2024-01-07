using Microsoft.EntityFrameworkCore;


namespace AspNetCore.Swashbuckle.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}

