using MDDPlatform.DomainModels.Application.Commands;
using MDDPlatform.Messages.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MDDPlatform.DomainModels.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransformationPatternController : ControllerBase
{

    private readonly IMessageDispatcher _messageDispatcher;

    public TransformationPatternController(IMessageDispatcher messageDispatcher)
    {
        _messageDispatcher = messageDispatcher;
    }
    ///////////////////////////////Object2Concept/////////////////////////////
    [HttpPost("Object2Concept/ConvertInstanceToType")]
    public async Task ConvertInstanceToType([FromBody] ConvertInstanceToType command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ExtractOperationFromChainOfNodes")]
    public async Task ExtractOperationFromChainOfNodes([FromBody] ExtractOperationFromChainOfNodes command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Concept/ExtractOperationAttributeFromChainOfNodes")]
    public async Task ExtractOperationAttributeFromChainOfNodes([FromBody] ExtractOperationAttributeFromChainOfNodes command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ExtractOperationFromOneToOneToManyRelation")]
    public async Task ExtractOperationFromOneToOneToManyRelation([FromBody] ExtractOperationFromOneToOneToManyRelation command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Concept/MapObjectPropertyToConceptAttribute")]
    public async Task MapObjectPropertyToConceptAttribute([FromBody] MapObjectPropertyToConceptAttribute command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Concept/ReplaceAssociationWithRelation")]
    public async Task ReplaceAssociationWithRelation([FromBody] ReplaceAssociationWithRelation command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ReplaceRelationWithAction")]
    public async Task ReplaceRelationWithAction([FromBody] ReplaceRelationWithAction command)
    {
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Concept/ReplaceRelationWithGenericProperty")]
    public async Task ReplaceRelationWithGenericProperty([FromBody] ReplaceRelationWithGenericProperty command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ReplaceRelationWithProperty")]
    public async Task ReplaceRelationWithProperty([FromBody] ReplaceRelationWithProperty command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ReplaceRelationWithOperation")]
    public async Task ReplaceRelationWithOperation([FromBody] ReplaceRelationWithOperation command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ReplaceRelationWithOperationAttributes")]
    public async Task ReplaceRelationWithOperationAttributes([FromBody] ReplaceRelationWithOperationAttributes command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Concept/ReplaceRelationWithConceptAttribute")]
    public async Task ReplaceRelationWithConceptAttribute([FromBody] ReplaceRelationWithConceptAttribute command){
        await _messageDispatcher.HandleAsync(command);
    }

    ///////////////////////////////Object2Object/////////////////////////////

    [HttpPost("Object2Object/MapInstance")]
    public async Task MapInstance([FromBody] MapInstance command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Object/MapOneToOne")]
    public async Task MapOneToOne([FromBody] MapOneToOne command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Object/MapOneToOneWithProperties")]
    public async Task MapOneToOneWithProperties([FromBody] MapOneToOneWithProperties command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Object/MapOneToTwo")]
    public async Task MapOneToTwo([FromBody] MapOneToTwo command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Object/MergerDomainObjectModels")]
    public async Task MergerDomainObjectModels([FromBody] MergerDomainObjectModels command){
        await _messageDispatcher.HandleAsync(command);
    }


    [HttpPost("Object2Object/SetRelationalDimension")]
    public async Task SetRelationalDimension([FromBody] SetRelationalDimension command)
    {
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Object/ReplaceRelationWithChainOfNodes")]
    public async Task ReplaceRelationWithChainOfNodes(ReplaceRelationWithChainOfNodes command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("Object2Object/MapRelatedObjects")]
    public async Task MapRelatedObjects([FromBody] MapRelatedObjects command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Object2Object/ReplaceRelationWithForkNode")]
    public async Task ReplaceRelationWithForkNode([FromBody] ReplaceRelationWithForkNode command){
        await _messageDispatcher.HandleAsync(command);
    }
    ///////////////////////////////RelationalDimension/////////////////////////////

    [HttpPost("RelationalDimension/CopyUpstreamNodePropertyToDownstreamNodeRelation")]
    public async Task CopyUpstreamNodePropertyToDownstreamNodeRelation(CopyUpstreamNodePropertyToDownstreamNodeRelation command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("RelationalDimesion/CopyUpstreamNodeRelationToDownstreamNodeRelation")]
    public async Task CopyUpstreamNodeRelationToDownstreamNodeRelation(CopyUpstreamNodeRelationToDownstreamNodeRelation command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("RelationalDimension/ReplaceRelationalDimensionWithSourceNodeRelation")]
    public async Task ReplaceRelationalDimensionWithSourceNodeRelation(ReplaceRelationalDimensionWithSourceNodeRelation command)
    {
        await _messageDispatcher.HandleAsync(command);        
    }
    [HttpPost("RelationalDimension/MergeUpstreamNodePropertyWithDownStreamNodeRelation")]
    public async Task MergeUpstreamNodePropertyWithDownStreamNodeRelation(MergeUpstreamNodePropertyWithDownStreamNodeRelation command)
    {
        await _messageDispatcher.HandleAsync(command);        
    }
    [HttpPost("RelationalDimension/MergeUpstreamNodeWithDownStreamNode")]
    public async Task MergeUpstreamNodeWithDownStreamNode(MergeUpstreamNodeWithDownStreamNode command)
    {
        await _messageDispatcher.HandleAsync(command);        
    }
    [HttpPost("RelationalDimension/MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute")]
    public async Task MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute(MergeUpstreamNodeWithDownStreamNodePropertyAsAttribute command)
    {
        await _messageDispatcher.HandleAsync(command);        
    }

    [HttpPost("RelationalDimension/SetHigherDimensionRelation")]
    public async Task SetHigherDimensionRelation(SetHigherDimensionRelation command){
        Console.WriteLine("start dispatching SetHigherDimensionRelation requst");
        await _messageDispatcher.HandleAsync(command);
        Console.WriteLine("SetHigherDimensionRelation is dispached");
    }
}
