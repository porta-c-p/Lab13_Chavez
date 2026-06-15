namespace Lab13_Chavez.Application.Interfaces;

public interface IGenericRepositoryMarker
{
}

public interface IGenericRepository<T> : IGenericRepositoryMarker where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
}