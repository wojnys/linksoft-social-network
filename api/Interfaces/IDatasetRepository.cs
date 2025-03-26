using System.Data;
using api.Dtos.Databaset;
using api.Models;

namespace api.Interfaces
{
    public interface IDatasetRepository : IGenericRepository<Dataset>
    {

        Task<int> GetAvarageUserFriendsForDataset(int datasetId);
        Task<int> GetUsersCountForDataset(int datasetId);
        Task<bool> IsDatasetNameAvailable(string datasetName);


    }
}