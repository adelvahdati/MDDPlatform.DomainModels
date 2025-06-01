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
public class ExtractOperationFromChainOfNodesHandler : ICommandHandler<ExtractOperationFromChainOfNodes>
{
    private readonly IDomainModelRepository _domainModelRepository;
    private readonly IDomainModelService _domainModelService;
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;

    public ExtractOperationFromChainOfNodesHandler(IDomainModelRepository domainModelRepository, IDomainModelService domainModelService, IMessageBroker messageBroker, IEventMapper eventMapper)
    {
        _domainModelRepository = domainModelRepository;
        _domainModelService = domainModelService;
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
    }

    public void Handle(ExtractOperationFromChainOfNodes command)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(ExtractOperationFromChainOfNodes command)
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

    private async Task HandleModelOperationAsync(ExtractOperationFromChainOfNodes command)
    {
        var tuples = await _domainModelService.GetChainOfRelatedObjectAsync(command.InputModel,command.FirstNode,command.FirstToMiddleNodeRelation,command.MiddleNode,command.MiddleToLastNodeRelation,command.LastNode);
        DomainModel outputDomainModel = await _domainModelRepository.GetDomainModelAsync(command.OutputModel);
        Dictionary<string,string> keyValues;
        List<string> concepts = new();
        foreach(var tuple in tuples)
        {
            var firstNode = tuple.Item1;
            var middlenode = tuple.Item3;
            var lastNode = tuple.Item5;

            keyValues = new();
            keyValues = firstNode.ToKeyValueExpressionResolver("FirstNode")
                                    .AppendKeyValueExpressionResolver(middlenode,"MiddleNode")
                                    .AppendKeyValueExpressionResolver(lastNode,"LastNode");
            

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
            string? outputMultiplicity = command.OperationOutputMultiplicityExpression.ResolveExpression(keyValues);
            if(!string.IsNullOrEmpty(outputType))
            {
                if(!string.IsNullOrEmpty(outputMultiplicity) && outputMultiplicity == "*")
                    operationOutput = OperationOutput.Create(outputType,true);
                else
                    operationOutput = OperationOutput.Create(outputType,false);                    
            }
            else
            {
                operationOutput = OperationOutput.Void();
            }


            var OpInputs = new List<OperationInput>();
            string? operationInputs = command.OperationInputsExpression.ResolveExpression(keyValues);
            if(!string.IsNullOrEmpty(operationInputs))
            {
                OpInputs = operationInputs.Split(",")
                                .Select((input,i)=> OperationInput.Create(string.Format($"item{i+1}"),input))
                                .ToList();
            }
            outputDomainModel.AddOperation(firstNode.Name, firstNode.Type, OpName, OpInputs, operationOutput);                      
        }
        await _domainModelRepository.UpdateDomainModelAsync(outputDomainModel);
    }
}
