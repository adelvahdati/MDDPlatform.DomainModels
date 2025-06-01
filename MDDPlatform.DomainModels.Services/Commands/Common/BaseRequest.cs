using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Services.Commands.Common;
public abstract class BaseRequest : ICommand
{
    public Guid CoordinationId {get;set;}
    public Guid StepId {get;set;}
}