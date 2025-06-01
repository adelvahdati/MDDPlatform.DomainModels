using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;

namespace MDDPlatform.DomainModels.Application.Services;
public class DomainModelService : IDomainModelService
{
    private readonly IDomainModelRepository _domainModelRepository;

    public DomainModelService(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }


    public async Task<List<DomainObjectDto>> GetDomainObjectsByTypeAsync(Guid domainModelId, string type)
    {
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);
        IReadOnlyList<DomainObject> domainObjects = domainModel.GetDomainObjects(type);
        
        return domainObjects.Select(d=>DomainObjectDto.CreateFrom(d))
                            .ToList();
    }

    public async Task<List<DomainObjectDto>> GetDomainObjectsOfTypesAsync(Guid domainModelId, List<string> types)
    {
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);
        IReadOnlyList<DomainObject> domainObjects = domainModel.GetDomainObjects(types);
        
        return domainObjects.Select(d=>DomainObjectDto.CreateFrom(d))
                            .ToList();
    }

    public async Task<List<Tuple<DomainObject,string,DomainObject>>> GetRelatedObjectsAsync(Guid domainModelId, string sourceType, string relationName, string targetType)
    {
        var tuples = new List<Tuple<DomainObject, string, DomainObject>>();
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);

        DomainConcept? sourceDomainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(sourceType));
        if (Equals(sourceDomainConcept, null))
            throw new Exception("Invalid source type");

        DomainConcept? targetDomainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(targetType));
        if (Equals(targetDomainConcept, null))
            throw new Exception("Invalid target type");

        var sourceDomainObjects = sourceDomainConcept.Instances;
        var targetDomainObjects = targetDomainConcept.Instances;

        foreach (var sourceDomainObject in sourceDomainObjects)
        {
            foreach (var targetDomainObject in targetDomainObjects)
            {
                if (sourceDomainObject.IsRelatedTo(targetDomainObject.FullName, relationName, targetType))
                {
                    var tuple = Tuple.Create<DomainObject, string, DomainObject>(sourceDomainObject, relationName, targetDomainObject);
                    tuples.Add(tuple);
                }
            }
        }
        return tuples;
    }

    public async Task<List<Tuple<DomainObject,string,DomainObject>>> GetRelatedObjectsAsync(Guid domainModelId, string sourceType, string relationName)
    {
        var tuples = new List<Tuple<DomainObject, string, DomainObject>>();
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);

        DomainConcept? sourceDomainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(sourceType));
        if (Equals(sourceDomainConcept, null))
            throw new Exception("Invalid source type");
        
        var relationTargets = sourceDomainConcept.GetRelationTarget(relationName)
                                                    .Select(rt=>rt.Value.ToLower())
                                                    .ToList();



        var sourceDomainObjects = sourceDomainConcept.Instances;

        var targetDomainObjects = domainModel.GetDomainObjects(relationTargets);

        foreach (var sourceDomainObject in sourceDomainObjects)
        {
            foreach (var targetDomainObject in targetDomainObjects)
            {
                if (sourceDomainObject.IsRelatedTo(targetDomainObject.FullName, relationName))
                {
                    var tuple = Tuple.Create<DomainObject, string, DomainObject>(sourceDomainObject, relationName, targetDomainObject);
                    tuples.Add(tuple);
                }
            }
        }
        return tuples;
    }
    public async Task<List<Tuple<DomainObject, string, List<DomainObject>>>> GetRelatedObjectsListAsync(Guid domainModelId, string sourceType, string relationName)
    {
        var tuples = new List<Tuple<DomainObject, string, List<DomainObject>>>();
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);

        DomainConcept? sourceDomainConcept = domainModel.GetDomainConcept(ConceptFullName.Create(sourceType));
        if (Equals(sourceDomainConcept, null))
            throw new Exception("Invalid source type");
        
        var relationTargets = sourceDomainConcept.GetRelationTarget(relationName)
                                                    .Select(rt=>rt.Value.ToLower())
                                                    .ToList();



        var sourceDomainObjects = sourceDomainConcept.Instances;

        var targetDomainObjects = domainModel.GetDomainObjects(relationTargets);

        foreach (var sourceDomainObject in sourceDomainObjects)
        {
            var relatedObjects = new List<DomainObject>();
            foreach (var targetDomainObject in targetDomainObjects)
            {
                if (sourceDomainObject.IsRelatedTo(targetDomainObject.FullName, relationName))
                {
                    relatedObjects.Add(targetDomainObject);
                }
            }
            var tuple = Tuple.Create<DomainObject, string, List<DomainObject>>(sourceDomainObject, relationName, relatedObjects);
            tuples.Add(tuple);
        }
        return tuples;
    }

    public async Task<List<Tuple<DomainObject, string, DomainObject, string, List<DomainObject>>>> GetChainOfRelatedObjectAsync(
        Guid domainModelId, 
        string startNode, 
        string start2MiddleRelation, 
        string middleNode, 
        string middle2EndRelation)
    {
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);

        var tuples = new List<Tuple<DomainObject, string, DomainObject, string, List<DomainObject>>>();
        DomainConcept? startNodeConcept = domainModel.GetDomainConcept(ConceptFullName.Create(startNode));

        if (Equals(startNodeConcept, null))
            throw new Exception("Invalid type of start node");

        DomainConcept? middleNodeConcept = domainModel.GetDomainConcept(ConceptFullName.Create(middleNode));
        if (Equals(middleNodeConcept, null))
            throw new Exception("Invalid target type");

        var endNodeTypes = middleNodeConcept.GetRelationTarget(middle2EndRelation)
                                            .Select(rt=>rt.Value.ToLower())
                                            .ToList();
        
        var startNodeInstances = startNodeConcept.Instances;
        var middleNodeInstances = middleNodeConcept.Instances;
        var endNodeInstances = domainModel.GetDomainObjects(endNodeTypes);

        foreach(var startNodeInstance in startNodeInstances)
        {
            foreach(var middleNodeInstance in middleNodeInstances)
            {
                if (startNodeInstance.IsRelatedTo(middleNodeInstance.FullName, start2MiddleRelation, middleNode))
                {
                    var domainObjects = new List<DomainObject>();
                    foreach(var endNodeInstance in  endNodeInstances)
                    {
                        if (middleNodeInstance.IsRelatedTo(endNodeInstance.FullName, middle2EndRelation))
                        {
                            domainObjects.Add(endNodeInstance);
                        }                
                    }
                    var tuple = Tuple.Create<DomainObject,string,DomainObject,string , List<DomainObject>>(startNodeInstance,start2MiddleRelation,middleNodeInstance,middle2EndRelation,domainObjects);
                    tuples.Add(tuple);
                }
            }
        }
        return tuples;        
    }

    public async Task<List<Tuple<DomainObject, string, DomainObject, string, DomainObject>>> GetChainOfRelatedObjectAsync(Guid domainModelId, string firstNode, string firstToMiddleNodeRelation, string middleNode, string middleToLastNodeRelation, string lastNode)
    {
        DomainModel domainModel =  await _domainModelRepository.GetDomainModelAsync(domainModelId);

        var tuples = new List<Tuple<DomainObject, string, DomainObject, string, DomainObject>>();
        DomainConcept? startNodeConcept = domainModel.GetDomainConcept(ConceptFullName.Create(firstNode));

        if (Equals(startNodeConcept, null))
            throw new Exception("Start node not found");

        DomainConcept? middleNodeConcept = domainModel.GetDomainConcept(ConceptFullName.Create(middleNode));
        if (Equals(middleNodeConcept, null))
            throw new Exception("Middle node not found");

        DomainConcept? lastNodeConcept = domainModel.GetDomainConcept(ConceptFullName.Create(lastNode));
        if (Equals(lastNodeConcept, null))
            throw new Exception("Last node not found");
        
        var startNodeInstances = startNodeConcept.Instances;
        var middleNodeInstances = middleNodeConcept.Instances;
        var lastNodeInstances = lastNodeConcept.Instances;

        foreach(var startNodeInstance in startNodeInstances)
        {
            foreach(var middleNodeInstance in middleNodeInstances)
            {
                if (startNodeInstance.IsRelatedTo(middleNodeInstance.FullName, firstToMiddleNodeRelation, middleNode))
                {
                    foreach(var lastNodeInstance in  lastNodeInstances)
                    {
                        if (middleNodeInstance.IsRelatedTo(lastNodeInstance.FullName, middleToLastNodeRelation))
                        {
                            var tuple = Tuple.Create<DomainObject,string,DomainObject,string , DomainObject>(startNodeInstance,firstToMiddleNodeRelation,middleNodeInstance,middleToLastNodeRelation,lastNodeInstance);
                            tuples.Add(tuple);
                        }                
                    }
                }
            }
        }
        return tuples;        
    }
}
