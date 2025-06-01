using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Application.Events;
public class ModelOperationCompleted : IDomainEvent
{
    public Guid CoordinationId {get;set;}    
    public Guid StepId {get;set;}
    public string Action {get;set;}

    public ModelOperationCompleted(Guid coordinationId, Guid stepId, string action)
    {
        this.CoordinationId = coordinationId;
        this.StepId = stepId;
        this.Action = action;
    }
}