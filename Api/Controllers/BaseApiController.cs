using Api.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
                      ?? throw new InvalidOperationException("IMediator service is unavailable.");

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is { IsSuccess: false, Code: 404 }) return NotFound();

        if (result is { IsSuccess: true, Value: not null }) return Ok(result.Value);

        return BadRequest();
    }
}