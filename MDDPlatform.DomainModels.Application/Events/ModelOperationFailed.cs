using MDDPlatform.SharedKernel.Events;

namespace MDDPlatform.DomainModels.Application.Events;
public class ModelOperationFailed : IDomainEvent
{
    public Guid CoordinationId {get;set;}    
    public Guid StepId {get;set;}
    public string Action {get;set;}
    public string ErrorMessage {get;set;}

    public ModelOperationFailed(Guid coordinationId, Guid stepId, string action, string errorMessage)
    {
        CoordinationId = coordinationId;
        StepId = stepId;
        Action = action;
        ErrorMessage = errorMessage;
    }
}