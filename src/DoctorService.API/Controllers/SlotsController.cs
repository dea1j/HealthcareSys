using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DoctorService.API.Models;
using DoctorService.Application.Commands.AddAvailabilitySlot;
using DoctorService.Application.Commands.BookSlot;
using DoctorService.Application.Queries.GetAvailableSlots;

namespace DoctorService.API.Controllers;

[ApiController]
[Route("api/doctors/{doctorId:guid}/slots")]
public class SlotsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SlotsController> _logger;

    public SlotsController(IMediator mediator, ILogger<SlotsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailableSlots(
        Guid doctorId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving available slots for doctor: {DoctorId}", doctorId);

        var start = startDate ?? DateTime.UtcNow;
        var end = endDate ?? DateTime.UtcNow.AddDays(30);

        var query = new GetAvailableSlotsQuery(doctorId, start, end);
        var slots = await _mediator.Send(query, cancellationToken);

        return Ok(slots);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddSlot(
        Guid doctorId,
        [FromBody] AddAvailabilitySlotRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding availability slot for doctor: {DoctorId}", doctorId);
        
        var command = new AddAvailabilitySlotCommand(
            doctorId,
            request.StartTime,
            request.EndTime
        );

        var slotId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetAvailableSlots),
            new { doctorId },
            new { id = slotId });
    }

    [HttpPut("{slotId:guid}/book")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BookSlot(
        Guid doctorId,
        Guid slotId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Booking slot {SlotId} for doctor {DoctorId}", slotId, doctorId);
        
        var command = new BookSlotCommand(slotId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}