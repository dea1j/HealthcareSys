using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientService.API.Models;
using PatientService.Application.Commands.CreatePatient;
using PatientService.Application.Commands.DeletePatient;
using PatientService.Application.Commands.UpdatePatient;
using PatientService.Application.Queries.GetAllPatients;
using PatientService.Application.Queries.GetPatientById;

namespace PatientService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator  _mediator;
    private readonly ILogger<PatientsController> _logger;
    
    public PatientsController(IMediator mediator, ILogger<PatientsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all patients");
        var query = new GetAllPatientsQuery();
        var patients = await _mediator.Send(query, cancellationToken);
        return Ok(patients);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving patient with ID: {PatientId}", id);
        var query = new GetPatientByIdQuery(id);
        var patient = await _mediator.Send(query, cancellationToken);

        if (patient is null)
            throw new KeyNotFoundException($"Patient with ID {id} not found");

        return Ok(patient);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new patient with email: {Email}", request.Email);
        var command = request.Adapt<CreatePatientCommand>();
        var patientId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = patientId },
            new { id = patientId });
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdatePatientRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating patient with ID: {PatientId}", id);
        var command = new UpdatePatientCommand(
            id,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Address
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
        _logger.LogInformation("Deleting patient with ID: {PatientId}", id);
        var command = new DeletePatientCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}