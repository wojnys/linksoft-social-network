using System.Data;
using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IDatasetRepository
    {
        Task<List<Dataset>> GetAllAsync();
        Task<List<Dataset>> GetAllWithoutUsersAsync();

        Task<Dataset> GetUsersDatasetStat(int datasetId);

        Task<Dataset?> CreateDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request);
        Task<Boolean> CheckDatasetName(String datasetName);

        Task<Dataset?> GetByIdAsync(int id);  // first or null
        Task<Dataset> CreateAsync(Dataset datasetModel); // create
    }
}