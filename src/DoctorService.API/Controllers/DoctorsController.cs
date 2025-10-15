using DoctorService.API.Models;
using DoctorService.Application.Commands.CreateDoctor;
using DoctorService.Application.Commands.DeleteDoctor;
using DoctorService.Application.Commands.UpdateDoctor;
using DoctorService.Application.Queries.GetAllDoctors;
using DoctorService.Application.Queries.GetDoctorById;
using DoctorService.Application.Queries.GetDoctorsBySpecialization;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoctorService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(IMediator mediator, ILogger<DoctorsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? specialization = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving doctors. Specialization filter: {Specialization}", specialization);

        if (!string.IsNullOrWhiteSpace(specialization))
        {
            var query = new GetDoctorsBySpecializationQuery(specialization);
            var doctors = await _mediator.Send(query, cancellationToken);
            return Ok(doctors);
        }
        else
        {
            var query = new GetAllDoctorsQuery();
            var doctors = await _mediator.Send(query, cancellationToken);
            return Ok(doctors);
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving doctor with ID: {DoctorId}", id);
        var query = new GetDoctorByIdQuery(id);
        var doctor = await _mediator.Send(query, cancellationToken);

        if (doctor is null)
            throw new KeyNotFoundException($"Doctor with ID {id} not found");

        return Ok(doctor);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] CreateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new doctor with email: {Email}", request.Email);
        var command = request.Adapt<CreateDoctorCommand>();
        var doctorId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = doctorId },
            new { id = doctorId });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating doctor with ID: {DoctorId}", id);
        var command = new UpdateDoctorCommand(
            id,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Specialization,
            request.YearsOfExperience
        );

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting doctor with ID: {DoctorId}", id);
        var command = new DeleteDoctorCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}