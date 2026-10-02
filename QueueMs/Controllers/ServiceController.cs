using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueMs.Responses;
using QueueMS.Appilication.DTOs.Service;
using QueueMS.Appilication.Interfaces;

namespace QueueMs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ServiceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateServiceDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return ApiResponses.Ok("Service Created Successfully",  result);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return ApiResponses.Ok("All Services returned", result);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return ApiResponses.NotFound("Service Not Found", result);

            return ApiResponses.Ok("", result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult>Update(int id, [FromBody]UpdateServiceDto dto)
        {
            var result = _service.UpdateAsync(id, dto);

            if(result == null)
                return ApiResponses.NotFound("Service Not Found", result);

            return ApiResponses.Ok("Service Updated Successfully", result);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if(!result)
                return ApiResponses.NotFound("Service Not Found", result);


            return ApiResponses.Ok("Service Deleted Successfully", result);

        }
    }
}
