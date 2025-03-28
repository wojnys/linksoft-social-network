
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

        public async Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto request)
        {

            // Start a transaction
            using var transaction = await _unitOfWork.Datasests.BeginTransactionAsync();
            try
            {
                if (await _unitOfWork.Datasests.IsDatasetNameAvailable(request.DatasetName) == false)
                {
                    await transaction.RollbackAsync();
                    return null;
                }

                var datasetModel = new Dataset
                {
                    Name = request.DatasetName
                };
                await _unitOfWork.Datasests.AddAsync(datasetModel);
                await _unitOfWork.Complete();


                var users = request.Users.Select(u => new User
                {
                    UserId = (int)u.UserId,
                    FrientId = (int)u.FriendId,
                    DatasetId = datasetModel.Id
                }).ToList();

                await _unitOfWork.Users.AddRangeAsync(users);
                await _unitOfWork.Complete();

                await transaction.CommitAsync();

                datasetModel.UsersCount = await GetUsersCountForDataset(datasetModel.Id);
                datasetModel.AverageFriendsPerUser = await GetAverageUserFriendsForDataset(datasetModel.Id);

                return datasetModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetUsersCountForDataset(int datasetId)
        {
            var userIds = await _unitOfWork.Datasests.GetUserIdsForDataset(datasetId);
            var friendIds = await _unitOfWork.Datasests.GetFriendIdsForDataset(datasetId);

            return userIds.Union(friendIds).Distinct().Count();
        }

        public async Task<double> GetAverageUserFriendsForDataset(int datasetId)
        {
            var userFriendCounts = await _unitOfWork.Datasests.GetUserFriendCountsForDataset(datasetId);

            if (!userFriendCounts.Any()) return 0;

            return userFriendCounts.Average(x => x.Count);
        }

        public async Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync()
        {
            var datasets = await _unitOfWork.Datasests.GetAllAsync();
            foreach (var dataset in datasets)
            {
                dataset.UsersCount = await GetUsersCountForDataset(dataset.Id);
                dataset.AverageFriendsPerUser = await GetAverageUserFriendsForDataset(dataset.Id);
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
