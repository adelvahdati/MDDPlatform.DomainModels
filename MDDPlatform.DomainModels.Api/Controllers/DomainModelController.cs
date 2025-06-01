using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Queries;
using MDDPlatform.DomainModels.Application.Services;
using MDDPlatform.DomainModels.Services.Commands;
using MDDPlatform.Messages.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MDDPlatform.DomainModels.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DomainModelController : ControllerBase
{
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly IModelOperationService _modelOperationService;
    public DomainModelController(IMessageDispatcher messageDispatcher, IModelOperationService modelOperationService)
    {
        _messageDispatcher = messageDispatcher;
        _modelOperationService = modelOperationService;
    }
    [HttpPost("{domainModelId}/UpdateLanguage/{languageId}")]
    public async Task UpdateModelLangaue(Guid domainModelId,Guid languageId){
        var command = new UpdateModelLanguage(domainModelId,languageId);
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("DomainConcept/Create")]
    public async Task AddConcept([FromBody] CreateDomainConcept command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("DomainConcept/Remove")]
    public async Task RemoveConcept([FromBody] RemoveDomainConcept command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("DomainConcept/Instance/Create")]
    public async Task CreateDomainConceptInstnace([FromBody] CreateDomainConceptInstance command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/Create")]
    public async Task CreateInstance([FromBody] CreateDomainObject command){
        await _messageDispatcher.HandleAsync(command);
    }

    [HttpPost("{domainModelId}/Clear")]
    public async Task ClearDeomainModel([FromRoute] Guid domainModelId)
    {
        var command = new ClearDomainModel(domainModelId);
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("{domainModelId}/ConceptBasedModel/Clear")]
    public async Task ClearConceptBasedModel([FromRoute] Guid domainModelId)
    {
        var command = new ClearConceptBasedModel(domainModelId);
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpDelete("{domainModelId}/Instance/{domainObjectId}")]
    public async Task RemoveInstance(Guid domainModelId, Guid domainObjectId){
        var command = new RemoveDomainObject(domainModelId, domainObjectId);
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/Property/SetValue")]
    public async Task SetDomainObjectProperty([FromBody] SetDomainObjectProperty command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/CreateOrUpdateProperties")]
    public async Task CreateOrUpdateProperties([FromBody] CreateOrUpdateDomainObjectProperties command){
        //await _messageDispatcher.HandleAsync(command);
        await _modelOperationService.HandleAsync(command);
    }
    [HttpPost("DomainObject/CreateOrUpdateInstances")]
    public async Task CreateOrUpdateInstances([FromBody] CreateOrUpdateInstances command){
        await _modelOperationService.HandleAsync(command);
    }
    [HttpPost("DomainObject/SetRelationTargetInstances")]
    public async Task SetRelationTargetInstances(SetRelationTargetInstances command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/RelationTarget/SetInstance")]
    public async Task SetDomainObjectRelation([FromBody] SetDomainObjectRelation command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/RelationalDimessions/Add")]
    public async Task AddRelationalDimenssion([FromBody] AddRelationalDimension command){
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("DomainObject/Update")]
    public async Task UpdateDomainObject([FromBody] UpdateDomainObject command)
    {
        await _messageDispatcher.HandleAsync(command);
    }
    [HttpPost("Reuse")]
    public async Task ReuseModel([FromBody] ReuseModel command){
        await _messageDispatcher.HandleAsync(command);
    }

    // ----------------------------Query DomainModels ----------------------

    [HttpGet("{domainModelId}")]
    public async Task<DomainModelDto> GetDomainModel(Guid domainModelId){
        var query = new GetDomainModelById(domainModelId);
        return await _messageDispatcher.HandleAsync<DomainModelDto>(query);
    }
    [HttpGet("Domain/{domainId}/Models")]
    public async Task<List<DomainModelDto>> GetDomainModels(Guid domainId){
        var query = new GetDomainModels(domainId);
        return await _messageDispatcher.HandleAsync<List<DomainModelDto>>(query);
    }
    [HttpGet("{domainModelId}/Elements")]
    public async Task<DomainModelElementsDto?> GetDomainModelElements(Guid domainModelId){
        var query = new GetDomainModelElements(domainModelId);
        return await _messageDispatcher.HandleAsync<DomainModelElementsDto?>(query);
    }


    // ----------------------------Query Concepts ----------------------------
    [HttpGet("{domainModelId}/Concept/{name}/{type}")]
    public async Task<ConceptDto> GetConcept(Guid domainModelId,string name,string type){
        var query = new GetDomainModelConcept(domainModelId,name,type);
        return await _messageDispatcher.HandleAsync<ConceptDto>(query);
    }
    [HttpGet("{domainModelId}/Concepts")]
    public async Task<List<ConceptDto>> GetConcepts(Guid domainModelId){
        var query = new GetDomainModelConcepts(domainModelId);
        return await _messageDispatcher.HandleAsync<List<ConceptDto>>(query);
    }


    // ----------------------------Query DomainConcepts ----------------------------
    [HttpGet("{domainModelId}/DomainConcept/{fullname}")]
    public async Task<DomainConceptDto> GetDomainConceptByFullName(Guid domainModelId,string fullname)
    {
        var query = new GetDomainConceptByFullName(fullname,domainModelId);
        return await _messageDispatcher.HandleAsync<DomainConceptDto>(query);
    }
    [HttpPost("DomainConcepts/FilterByFullnames")]
    public async Task<List<DomainConceptDto>> GetDomainConceptsByFullNames([FromBody] GetDomainConceptsByFullNames query)
    {
        return await _messageDispatcher.HandleAsync<List<DomainConceptDto>>(query);
    }    
   [HttpGet("{domainModelId}/DomainConceptId/{domainConceptId}")]
    public async Task<DomainConceptDto> GetDomainConcept(Guid domainModelId, Guid domainConceptId){
        var query = new GetDomainConceptById(domainModelId, domainConceptId);
        return await _messageDispatcher.HandleAsync<DomainConceptDto>(query);
    }

    [HttpGet("{domainModelId}/DomainConcepts")]
    public async Task<List<ElementDto>> GetDomainConcepts(Guid domainModelId)
    {
        var query = new GetModelElements(domainModelId);
        return await _messageDispatcher.HandleAsync<List<ElementDto>>(query);
    }

    // ----------------------------Query DomainObjects ----------------------------
    [HttpGet("{domainModelId}/DomainObjetcs")]
    public async Task<List<DomainObjectDto>> GetDomainObjects(Guid domainModelId)
    {
        var query = new GetDomainObjects(domainModelId);
        return await _messageDispatcher.HandleAsync<List<DomainObjectDto>>(query);
    }
    [HttpGet("{domainModelId}/DomainObjetcs/{type}")]
    public async Task<List<DomainObjectDto>> GetDomainObjectsByType(Guid domainModelId,string type)
    {
        var query = new GetDomainObjectsByType(domainModelId,type);
        return await _messageDispatcher.HandleAsync<List<DomainObjectDto>>(query);
    }
    [HttpPost("DomainObjects/FilterByTypes")]
    public async Task<List<DomainObjectDto>> GetDomainObjectsOfTypes([FromBody] GetDomainObjetcsOfTypes query)
    {
        return await _messageDispatcher.HandleAsync<List<DomainObjectDto>>(query);
    }
    [HttpPost("DomainObjects/FilterByRelationAndTarget")]
    public async Task<List<DomainObjectTupleDto>> GetDomainObjectTuples([FromBody] GetDomainObjectTuples query)
    {
        return await _messageDispatcher.HandleAsync<List<DomainObjectTupleDto>>(query);
    }
    [HttpPost("DomainObjects/FilterByRelation")]
    public async Task<List<DomainObjectTupleDto>> GetRelatedDomainObjects([FromBody] GetRelatedDomainObjects query)
    {
        return await _messageDispatcher.HandleAsync<List<DomainObjectTupleDto>>(query);
    }

    [HttpGet("{domainModelId}/DomainConcept/{domainConceptId}/DomainObjects")]
    public async Task<List<DomainObjectDto>> GetDomainObjectsByTypeId(Guid domainModelId, Guid domainConceptId)
    {
        var query = new GetDomainObjectsByTypeId(domainModelId,domainConceptId);
        return await _messageDispatcher.HandleAsync<List<DomainObjectDto>>(query);
    }

    [HttpGet("{domainModelId}/DomainObject/{domainObjectId}")]
    public async Task<DomainObjectDto> GetDomainObjectById(Guid domainModelId, Guid domainObjectId){
        var query = new GetDomainObjectById(domainModelId, domainObjectId);
        return await _messageDispatcher.HandleAsync<DomainObjectDto>(query);
    }

     // ----------------------------Query Instances ----------------------------
    [HttpGet("{domainModelId}/DomainConcept/{domainConceptId}/Instances")]
    public async Task<List<InstanceDto>> GetInstances(Guid domainModelId,Guid domainConceptId){
        var query = new GetDomainConceptInstances(domainModelId,domainConceptId);
        return await _messageDispatcher.HandleAsync<List<InstanceDto>>(query);
    }


}