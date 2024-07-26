﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoCleanArchitecture.Domain.Abstractions;
using TodoCleanArchitecture.Domain.Repositories;
using TodoCleanArchitecture.Infrastructure.Context;

namespace TodoCleanArchitecture.Infrastructure.Repositories;
internal class Repository<T> : IRepository<T>
     where T : Entity
{
    private DbSet<T> entity;
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        entity = context.Set<T>();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await entity.AnyAsync(predicate, cancellationToken);
    }

    public async Task CreateAsync(T data, CancellationToken cancellationToken = default)
    {
        await entity.AddAsync(data, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T data, CancellationToken cancellationToken = default)
    {
        entity.Remove(data);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> GetAll()
    {
        return entity.AsNoTracking().AsQueryable();
    }

    public IQueryable<T> GetAllWithTracking()
    {
        return entity.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await entity.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Update(T data)
    {
        entity.Update(data);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return entity.AsNoTracking().Where(predicate).AsQueryable();
    }
}