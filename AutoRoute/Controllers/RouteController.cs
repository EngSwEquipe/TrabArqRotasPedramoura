using AutoRoute.Services;
using AutoRoute.Views;
using Microsoft.AspNetCore.Mvc;

namespace AutoRoute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _service;
        private readonly IPositionService _positionService;
        private readonly IInMemorySettings _inMemorySettings;
        public RouteController(IRouteService service, IPositionService positionService, IInMemorySettings inMemorySettings)
        {
            _service = service;
            _positionService = positionService;
            _inMemorySettings = inMemorySettings;
        }

        [HttpGet("positions")]
        public IActionResult GetPositions()
        {
            return Ok(_inMemorySettings.GetPositions());
        }

        [HttpGet("getAll")]
        public IActionResult GetRoutes()
        {
            return Ok(_inMemorySettings.GetRoutes());
        }

        [HttpGet]
        public async Task<IActionResult> GetRoute()
        {
            try
            {
                return Ok(await _service.GetRoute());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("position")]
        public async Task<IActionResult> CreatePosition([FromBody] PositionRequestView request)
        {
            try
            {
                return Ok(await _positionService.CreatePosition(request));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
