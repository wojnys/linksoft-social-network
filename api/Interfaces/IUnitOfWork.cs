using api.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDatasetRepository Datasests { get; }
    IUserRepository Users { get; }
    int Complete();
}