using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientService.API.Models;
using PatientService.Application.Commands.CreatePatient;
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllPatientsQuery();
            var patients = await _mediator.Send(query, cancellationToken);
            return Ok(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patients");
            return StatusCode(500, "An error occurred while retrieving patients");
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetPatientByIdQuery(id);
            var patient = await _mediator.Send(query, cancellationToken);

            if (patient is null)
                return NotFound($"Patient with ID {id} not found");

            return Ok(patient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient {PatientId}", id);
            return StatusCode(500, "An error occurred while retrieving the patient");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = request.Adapt<CreatePatientCommand>();
            var patientId = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = patientId },
                new { id = patientId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating patient");
            return StatusCode(500, "An error occurred while creating the patient");
        }
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdatePatientRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient {PatientId}", id);
            return StatusCode(500, "An error occurred while updating the patient");
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            // You'll need to create a DeletePatientCommand
            // For now, just return NotImplemented
            return StatusCode(501, "Delete operation not yet implemented");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient {PatientId}", id);
            return StatusCode(500, "An error occurred while deleting the patient");
        }
    }
}