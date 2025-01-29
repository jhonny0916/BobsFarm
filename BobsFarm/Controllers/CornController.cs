using AutoMapper;
using BobsFarm.Models.Error;
using BobsFarm_AL.Interfaces;
using BobsFarm_BO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BobsFarm.Controllers
{
    [ApiController]
    public class CornController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ICornService _cornService;
        public CornController(ICornService cornService, IMapper mapper) 
        { 
            _cornService = cornService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/Corn/buyCorn/{clientId}")]
        public async virtual Task<IActionResult> BuyCorn([FromRoute][Required] string clientId)
        {
            try
            {
                var cornBought = await _cornService.BuyCorn(clientId);
                if(cornBought)
                    return Ok();
                else
                    return StatusCode(429, "Too Many Requests");
            }
            catch (BLException e)
            {
                var error = this._mapper.Map<BOError, ErrorResponse>(e._error);
                return BadRequest(error);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
