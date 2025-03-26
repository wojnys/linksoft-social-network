using api.Dtos.Databaset;
using api.Interfaces;
using api.Mappers;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/datasets")]

    [ApiController]

    public class DatasetController : ControllerBase
    {
        private readonly IDatasetApplication _application;
        public DatasetController(IDatasetApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var datasets = await _application.GetAllWithUserStatsAsync();

            return Ok(datasets);
        }

        // [HttpGet("with-users")]
        // public async Task<IActionResult> GetAllWithUsers()
        // {
        //     var datasets = await _unitOfWork.GetAllWithUsersAsync();
        //     var datasetsDto = datasets.Select(s => s.ToDatasetDto());

        //     return Ok(datasetsDto);
        // }

        [HttpPost("create-dataset-with-users")]
        public async Task<IActionResult> CreateDataset([FromBody] CreateDatasetWithUsersRequestDto request)
        {
            if (request.DatasetName == null)
            {
                return BadRequest(new { Message = "Dataset name is null", DatasetName = request.DatasetName });
            }

            try
            {

                var result = await _application.AddDatasetWithUsersAsync(request);

                if (result == null)
                {
                    return BadRequest(new { Message = "Dataset name already exists", DatasetName = request.DatasetName });
                }

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
            var dataset = await _application.GetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound();
            }

            return Ok(dataset.ToDatasetDto());


        }
    }
}