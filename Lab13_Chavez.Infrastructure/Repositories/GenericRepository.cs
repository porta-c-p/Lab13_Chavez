using Lab13_Chavez.Application.Interfaces;
using Lab13_Chavez.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab13_Chavez.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly Lab13ChavezDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(Lab13ChavezDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}