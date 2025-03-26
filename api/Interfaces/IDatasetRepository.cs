using System.Data;
using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IDatasetRepository : IGenericRepository<Dataset>
    {
        Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request);
        Task<IEnumerable<Dataset>> GetAllWithUsersAsync();
        Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync();

    }
}