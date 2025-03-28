
using api.Dtos.Databaset;
using api.Models;

namespace api.Services
{
    public interface IDatasetService
    {

        Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request);

        Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync();

        Task<Dataset?> GetByIdAsync(int id);

        Task<bool> IsDatasetNameAvailable(string datasetName);


    }
}
