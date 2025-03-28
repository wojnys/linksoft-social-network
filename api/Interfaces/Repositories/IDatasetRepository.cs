using System.Data;
using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IDatasetRepository : IGenericRepository<Dataset>
    {

        Task<IEnumerable<int>> GetUserIdsForDataset(int datasetId);
        Task<IEnumerable<int>> GetFriendIdsForDataset(int datasetId);
        Task<List<(int UserId, int Count)>> GetUserFriendCountsForDataset(int datasetId);
        Task<bool> IsDatasetNameAvailable(string datasetName);


    }
}