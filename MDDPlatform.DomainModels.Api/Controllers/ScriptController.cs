using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.Messages.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MDDPlatform.DomainModels.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScriptController : ControllerBase
{
    private readonly IMessageDispatcher _messageDispatcher;

    public ScriptController(IMessageDispatcher messageDispatcher)
    {
        _messageDispatcher = messageDispatcher;
    }
    [HttpPost("Run")]
    public async Task Run([FromBody] RunScript command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
}