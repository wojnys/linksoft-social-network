
using api.Dtos.Databaset;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Services;

namespace api.Applications
{
    public class DatasetApplication : IDatasetApplication
    {
        private readonly IDatasetService _service;

        public DatasetApplication(IDatasetService service)
        {
            _service = service;
        }
        public async Task<Dataset?> AddDatasetWithUsersAsync(CreateDatasetWithUsersRequestDto datasetCreateDto)
        {
            try
            {
                if (await _service.IsDatasetNameAvailable(datasetCreateDto.DatasetName) == false)
                {
                    return null;
                }
                return await _service.AddDatasetWithUsersAsync(datasetCreateDto);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<IEnumerable<DatasetWithoutUsersDto>> GetAllWithUserStatsAsync()
        {
            var datasets = await _service.GetAllWithUserStatsAsync();
            var datasetDto = datasets.Select(d => d.ToDatasetDtoWithoutUsers());

            return datasetDto;
        }

        public Task<bool> IsDatasetNameAvailable(string datasetName)
        {
            throw new NotImplementedException();
        }

        public async Task<Dataset?> GetByIdAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}

