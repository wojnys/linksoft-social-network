using api.Data;
using api.Interfaces;
using api.Models;
using api.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        Datasests = new DatasetRepository(_context);
        Users = new UserRepository(_context);
    }

    public IDatasetRepository Datasests { get; private set; }
    public IUserRepository Users { get; private set; }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}