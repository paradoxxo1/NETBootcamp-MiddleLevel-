using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Domain.Entities;

namespace TodoCleanArchitecture.Application.Service;
public interface IApplicationDbContext
{
    public DbSet<Todo> Todos { get; set; }
}
