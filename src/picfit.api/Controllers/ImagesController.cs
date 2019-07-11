using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using picfit.application.Commands;
using System.Threading.Tasks;
using picfit.application.Extensions;
using picfit.application.Queries;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Linq;
using System.Net;

namespace picfit.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ILogger<ImagesController> _logger { get; set; }
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AddImageCommandResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(AddImageCommandResult), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(IFormFile image)
        {
            try
            {
                var command = new AddImageCommand(
                    image.OpenReadStream().ConvertToByteArray(),
                    image.FileName);

                var result = await Mediator.Send(command);
                if (result == null)
                    return StatusCode(500);
                if (result.Scaled == null || !result.Scaled.Any())
                    return Conflict(result);
                return Created($"{result.Key}/{result.Scaled}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(AddImageCommandResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(IFormFile image)
        {
            try
            {
                var command = new AddImageCommand(
                    image.OpenReadStream().ConvertToByteArray(),
                    image.FileName,
                    true);

                var result = await Mediator.Send(command);
                if (result == null)
                    return StatusCode(500);
                if (result.Scaled == null || !result.Scaled.Any())
                    return NotFound();
                return Created($"{result.Key}/{result.Scaled}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        [ProducesResponseType(typeof(DeleteImageCommandResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            try
            {
                var command = new DeleteImageCommand(key);
                var result = await Mediator.Send(command);
                if (result == null)
                    return StatusCode(500);
                if (result.Deleted == null || !result.Deleted.Any())
                    return NotFound();
                else
                    return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return StatusCode(500);
            }
        }


        [HttpGet("{key}")]
        public async Task<IActionResult> Get(
            [FromRoute] string key, 
            [FromQuery] int width, 
            [FromQuery] int height,
            [FromQuery] string extension)
        {
            try
            {


            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return StatusCode(500);
            }
        }

        [HttpGet("{key}/{name}")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]        
        public async Task<IActionResult> Get([FromRoute] string key, [FromRoute] string name)
        {
            try
            {
                new FileExtensionContentTypeProvider().TryGetContentType(name, out string contentType);
                var query = new GetScaledImageQuery(key, name);
                var result = await Mediator.Send(query);
                if (result == null)
                    return NotFound();
                return File(result.Data, contentType, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return StatusCode(500);
            }
        }
    }
}
