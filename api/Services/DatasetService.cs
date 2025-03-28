
using api.Dtos.Databaset;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class DatasetService : IDatasetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DatasetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Create task for CreateDataset

        public async Task<Dataset> CreateDatasetAsync(Dataset request)
        {
            var datasetModel = new Dataset
            {
                Name = request.Name
            };
            await _unitOfWork.Datasests.AddAsync(datasetModel);
            await _unitOfWork.Complete();
            return datasetModel;
        }


        public async Task<int> GetUsersCountForDataset(int datasetId)
        {
            var userIds = await _unitOfWork.Datasests.GetUserIdsForDataset(datasetId);
            var friendIds = await _unitOfWork.Datasests.GetFriendIdsForDataset(datasetId);

            return userIds.Union(friendIds).Distinct().Count();
        }

        public async Task<int> GetAverageUserFriendsForDataset(int datasetId)
        {
            var userFriendCounts = await _unitOfWork.Datasests.GetUserFriendCountsForDataset(datasetId);

            if (!userFriendCounts.Any()) return 0;

            return (int)Math.Round(userFriendCounts.Average(x => x.Count));
        }

        public async Task<Dataset> GetDatasetUserStats(int datasetId)
        {
            var dataset = await _unitOfWork.Datasests.GetByIdAsync(datasetId);

            dataset.UsersCount = await GetUsersCountForDataset(datasetId);
            dataset.AverageFriendsPerUser = await GetAverageUserFriendsForDataset(datasetId);
            return dataset;
        }

        public async Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync()
        {
            var datasets = await _unitOfWork.Datasests.GetAllAsync();
            foreach (var dataset in datasets)
            {
                var datasetModel = await GetDatasetUserStats(dataset.Id);
                dataset.UsersCount = datasetModel.UsersCount;
                dataset.AverageFriendsPerUser = datasetModel.AverageFriendsPerUser;

            }
            return datasets;
        }


        public async Task<Dataset?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Datasests.GetByIdAsync(id);
        }

        public async Task<bool> IsDatasetNameAvailable(string datasetName)
        {
            return await _unitOfWork.Datasests.IsDatasetNameAvailable(datasetName);
        }

    }
}
