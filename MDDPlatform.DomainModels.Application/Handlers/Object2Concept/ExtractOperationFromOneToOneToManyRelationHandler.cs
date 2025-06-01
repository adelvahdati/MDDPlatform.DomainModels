using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.DomainModels.Application.Events;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;
using MDDPlatform.Messages.Brokers;
using MDDPlatform.Messages.Commands;
using MDDPlatform.SharedKernel.Events;
using MDDPlatform.SharedKernel.Mappers;

namespace MDDPlatform.DomainModels.Application.Handlers;
public class ExtractOperationFromOneToOneToManyRelationHandler : ICommandHandler<ExtractOperationFromOneToOneToManyRelation>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ExtractOperationFromOneToOneToManyRelationHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ExtractOperationFromOneToOneToManyRelation command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ExtractOperationFromOneToOneToManyRelation command)
    {
        IDomainEvent @event;
        try
        {
            await HandleModelOperationAsync(command);            
            @event = new ModelOperationCompleted(command.CoordinationId,command.StepId,command.GetType().Name);
        }
        catch(Exception ex)
        {
            @event = new ModelOperationFailed(command.CoordinationId,command.StepId,command.GetType().Name,ex.Message);
        }
        await _messageBroker.PublishAsync(_eventMapper.Map(@event));
    }

    private async Task HandleModelOperationAsync(ExtractOperationFromOneToOneToManyRelation command)
    {
        var tuples = await _domainModelService.GetChainOfRelatedObjectAsync(command.InputModel,command.FirstNode,command.FirstToMiddleNodeRelation,command.MiddleNode,command.MiddleToLastNodeRelation);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        Dictionary<string,string> keyValues;
        List<string> concepts = new();
        foreach(var tuple in tuples)
        {
            var firstNode = tuple.Item1;
            var middlenode = tuple.Item3;
            var lastNodes = tuple.Item5.Where(domainObject=> domainObject.Type.ToLower()==command.LastNode.ToLower()).ToList();

            keyValues = new();
            keyValues = firstNode.ToKeyValueExpressionResolver("FirstNode")
                                    .AppendKeyValueExpressionResolver(middlenode,"MiddleNode");
            

            string conceptFullName = firstNode.FullName.Value;
            if (!concepts.Contains(conceptFullName))
            {
                outputDomainModel.TryCreateDomainConcept(firstNode.Name, firstNode.Type);
                concepts.Add(conceptFullName);
            }
            string? operationName = command.OperationNameExpression.ResolveExpression(keyValues);
            var OpName = OperationName.Create(operationName);

            OperationOutput operationOutput;
            string? outputType = command.OperationOutputTypeExpression.ResolveExpression(keyValues);
            if(!string.IsNullOrEmpty(outputType))
                operationOutput = OperationOutput.Create(outputType);
            else
                operationOutput = OperationOutput.Void();
    

            var OpInputs = new List<OperationInput>();
            if(lastNodes!=null){
                foreach(var lastNode in lastNodes)
                {
                    keyValues = new();
                    keyValues = lastNode.ToKeyValueExpressionResolver("LastNode");

                    var inputName = command.OperationInputNameExpression.ResolveExpression(keyValues);
                    var inputType = command.OperationInputTypeExpression.ResolveExpression(keyValues);
                    var opInput = OperationInput.Create(inputName,inputType);
                    OpInputs.Add(opInput);
                }
            }
            outputDomainModel.AddOperation(firstNode.Name, firstNode.Type, OpName, OpInputs, operationOutput);                      
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
