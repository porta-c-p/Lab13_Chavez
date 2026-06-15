using Lab13_Chavez.Application.Interfaces;
using Lab13_Chavez.Infrastructure.Persistence.Context;
using Lab13_Chavez.Infrastructure.Repositories;

namespace Lab13_Chavez.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lab13ChavezDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(Lab13ChavezDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        Type entityType = typeof(T);

        if (!_repositories.ContainsKey(entityType))
        {
            _repositories[entityType] = new GenericRepository<T>(_context);
        }

        return (IGenericRepository<T>)_repositories[entityType];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}