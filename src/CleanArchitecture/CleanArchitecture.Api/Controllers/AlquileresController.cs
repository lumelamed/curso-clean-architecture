namespace CleanArchitecture.Api.Controllers
{
    using CleanArchitecture.Api.Requests;
    using CleanArchitecture.Application.Alquileres.GetAlquiler;
    using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/alquileres")]
    public class AlquileresController : ControllerBase
    {
        private readonly ISender sender;

        public AlquileresController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlquiler(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAlquilerQuery(id);
            var result = await this.sender.Send(query, cancellationToken);

            return result.IsSuccess ? this.Ok(result.Value) : this.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReservarAlquiler(Guid id, [FromBody] AlquilerReservaRequest request, CancellationToken cancellationToken)
        {
            var command = new ReservarAlquilerCommand(request.vehiculoId, request.userId, request.startDate, request.endDate);

            var result = await this.sender.Send(command, cancellationToken);

            if (result?.IsFailure ?? true)
            {
                return this.BadRequest(result?.Error);
            }

            return this.CreatedAtAction(nameof(this.GetAlquiler), new { id = result.Value }, cancellationToken);
        }
    }
}
