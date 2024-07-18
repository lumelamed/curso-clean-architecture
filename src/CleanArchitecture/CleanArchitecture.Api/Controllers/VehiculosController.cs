namespace CleanArchitecture.Api.Controllers
{
    using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculosController : ControllerBase
    {
        private readonly ISender sender;

        public VehiculosController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchVehiculos(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
        {
            var query = new SearchVehiculosQuery(startDate, endDate);
            var result = await this.sender.Send(query, cancellationToken);

            return this.Ok(result.Value);
        }
    }
}
