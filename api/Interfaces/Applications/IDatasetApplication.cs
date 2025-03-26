using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IDatasetApplication
    {
        Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request);
        Task<bool> IsDatasetNameAvailable(string datasetName);
        Task<IEnumerable<DatasetWithoutUsersDto>> GetAllWithUserStatsAsync();
        Task<Dataset?> GetByIdAsync(int id);
    }
}
