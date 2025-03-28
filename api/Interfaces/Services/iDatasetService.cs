
using api.Dtos.Databaset;
using api.Models;

namespace api.Services
{
    public interface IDatasetService
    {

        Task<Dataset> GetDatasetUserStats(int datasetId);

        Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync();
        Task<Dataset> CreateDatasetAsync(Dataset request);

        Task<Dataset> GetByIdAsync(int id);

        Task<bool> IsDatasetNameAvailable(string datasetName);


    }
}
