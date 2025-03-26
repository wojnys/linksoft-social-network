using api.Data;
using api.Dtos.Databaset;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/datasets")]

    [ApiController]

    public class DatasetController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDatasetRepository _datasetRepository;

        public DatasetController(ApplicationDBContext context, IDatasetRepository datasetRepository)
        {
            _datasetRepository = datasetRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var datasets = await _datasetRepository.GetAllAsync();
            var datasetDto = datasets.Select(s => s.ToDatasetDto()); // convert to DTO via mapper
            return Ok(datasetDto);
        }

        [HttpGet("with-stats")]
        public async Task<IActionResult> GetAllWithStats()
        {
            var listAllDatasets = await _datasetRepository.GetAllWithoutUsersAsync();
            var datasetArr = new List<Dataset>();

            foreach (var dat in listAllDatasets)
            {
                var dataset = await _datasetRepository.GetUsersDatasetStat(dat.Id);
                datasetArr.Add(dataset);
            }

            var datasetsWithoutUserDto = datasetArr.Select(s => s.ToDatasetDtoWithoutUsers()); // convert to DTO via mapper
            return Ok(datasetsWithoutUserDto);
        }


        [HttpGet("without-users")]
        public async Task<IActionResult> GetAllWithoutUsers()
        {

            var datasets = await _datasetRepository.GetAllWithoutUsersAsync();
            var datasetWithoutUsersDto = datasets.Select(s => s.ToDatasetDtoWithoutUsers()); // convert to DTO via mappe

            return Ok(datasetWithoutUsersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var dataset = await _datasetRepository.GetByIdAsync(id);
            if (dataset == null)
            {
                return NotFound();
            }
            return Ok(dataset.ToDatasetDto());
        }

        [HttpPost("create-dataset-with-users")]
        public async Task<IActionResult> CreateDatasetWithUsers([FromBody] CreateDatasetWithUsersRequestDto request)
        {
            if (request.DatasetName == null)
            {
                return BadRequest(new { Message = "Dataset name is null", DatasetName = request.DatasetName });
            }

            try
            {
                var result = await _datasetRepository.CreateDatasetWithUsersAsync(request);

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
    }
}