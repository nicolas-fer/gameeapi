using Api.Controllers.Base;
using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TeamController : ApiController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _teamService.GetAllAsync();

            return ApiResponse(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _teamService.GetByIdAsync(id);

            return ApiResponse(result);
        }      

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TeamDto? teamDto)
        {
            var result = await _teamService.InsertAsync(teamDto);

            return ApiResponse(result);
        }
        
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] TeamDto? teamDto)
        {
            var result = await _teamService.UpdateAsync(teamDto);

            return ApiResponse(result);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _teamService.DeleteAsync(id);

            return ApiResponse(result);
        }
    }
}
