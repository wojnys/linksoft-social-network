
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


                // await _unitOfWork.transaction.CommitAsync();

                datasetModel.UsersCount = await _unitOfWork.Datasests.GetUsersCountForDataset(datasetModel.Id);
                datasetModel.AverageFriendsPerUser = await _unitOfWork.Datasests.GetAvarageUserFriendsForDataset(datasetModel.Id);

                return datasetModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<IEnumerable<Dataset>> GetAllWithUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Dataset>> GetAllWithUserStatsAsync()
        {
            var datasets = await _unitOfWork.Datasests.GetAllAsync();
            foreach (var dataset in datasets)
            {
                dataset.UsersCount = await _unitOfWork.Datasests.GetUsersCountForDataset(dataset.Id);
                dataset.AverageFriendsPerUser = await _unitOfWork.Datasests.GetAvarageUserFriendsForDataset(dataset.Id);
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
