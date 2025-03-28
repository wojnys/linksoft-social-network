using api.Dtos.Databaset;
using api.Interfaces;
using api.Interfaces.Services;
using api.Mappers;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/datasets")]

    [ApiController]

    public class DatasetController : ControllerBase
    {
        private readonly IDatasetService _datasetService;

        public DatasetController(IDatasetService datasetService)
        {
            _datasetService = datasetService;


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var datasets = await _datasetService.GetAllWithUserStatsAsync();

                return Ok(datasets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpPost("create-dataset-with-users")]
        public async Task<IActionResult> CreateDataset([FromBody] CreateDatasetWithUsersRequestDto request)
        {
            if (request.DatasetName == null)
            {
                return BadRequest(new { Message = "Dataset name is null", DatasetName = request.DatasetName });
            }

            try
            {

                if (!await _datasetService.IsDatasetNameAvailable(request.DatasetName))
                {
                    return Conflict(new { Message = "Dataset name already exists", DatasetName = request.DatasetName });
                }

                var result = await _datasetService.AddDatasetWithUsersAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToDatasetDtoWithoutUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var dataset = await _datasetService.GetByIdAsync(id);

                if (dataset == null)
                {
                    return NotFound();
                }

                return Ok(dataset.ToDatasetDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}