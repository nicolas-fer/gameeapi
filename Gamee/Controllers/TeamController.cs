using Api.Controllers.Base;
using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{    
    public class TeamController : GameeController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _teamService.GetAllAsync();

            return GameeResponse(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _teamService.GetByIdAsync(id);

            return GameeResponse(result);
        }      

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TeamDto teamDto)
        {
            var result = await _teamService.InsertAsync(teamDto);

            return GameeResponse(result);
        }
        
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] TeamDto teamDto)
        {
            var result = await _teamService.UpdateAsync(teamDto);

            return GameeResponse(result);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _teamService.DeleteAsync(id);

            return GameeResponse(result);
        }
    }
}
