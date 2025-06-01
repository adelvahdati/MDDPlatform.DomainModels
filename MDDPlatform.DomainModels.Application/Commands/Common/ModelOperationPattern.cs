using MDDPlatform.Messages.Commands;

namespace MDDPlatform.DomainModels.Application.Commands;
public abstract class ModelOperationPattern : ICommand
{
    public Guid CoordinationId {get;set;}    
    public Guid StepId {get;set;}
}