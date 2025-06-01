using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.Entities;
using Attribute = MDDPlatform.BaseConcepts.ValueObjects.Attribute;

namespace MDDPlatform.DomainModels.Core.Entities;
public class DomainConcept : BaseAggregate<Guid>
{
    private BaseConcept _concept;
    private Domain _domain;
    private List<DomainObject> _domainObjects = new();

    public Guid DomainId => _domain.Id;
    
    
    public string Name => _concept.Name.Value;
    public string Type => _concept.Type.Value;
    public ConceptFullName FullName => ConceptFullName.Create(_concept.Name,_concept.Type);
    public Guid ConceptId => _concept.Id;
    
    public IReadOnlyList<DomainObject> Instances => _domainObjects;
    public IReadOnlyList<Property> Properties => _concept.GetProperties();
    public IReadOnlyList<Relation> Relations => _concept.GetRelations();
    public IReadOnlyList<Operation> Operations => _concept.GetOperations();

    public IReadOnlyList<Attribute> Attributes => _concept.Attributes;

    private DomainConcept(BaseConcept concept, Domain domain)
    {
        Id= Guid.NewGuid();
        _concept = concept;
        _domain = domain;
    }
    private DomainConcept(Guid domainConceptId, Domain domain, BaseConcept concept)
    {
        Id = domainConceptId;
        _domain = domain;
        _concept = concept;
    }
    private DomainConcept(Guid domainConceptId, Domain domain, BaseConcept concept, List<DomainObject> instances)
    {
        Id = domainConceptId;
        _domain = domain;
        _concept = concept;
        _domainObjects=instances;
    }


    internal static DomainConcept Create(BaseConcept concept, Domain domain){
        return new(concept,domain);
    }
    internal static DomainConcept Create(BaseConcept concept, Guid domainId){
        return new(concept,new Domain(domainId));
    }
    public static DomainConcept Load(Guid domainConceptId,Guid domainId,BaseConcept concept,List<DomainObject> instances)
    {
        return new(domainConceptId,new Domain(domainId),concept,instances);
    }


    private void CreateInstance(string name)
    {
        BaseConcept instance = _concept.CreateInstance(name);
        DomainObject domainObject = DomainObject.Create(instance,new DomainConceptId(Id));
        _domainObjects.Add(domainObject);

        AddEvent(new DomainObjectCreated(_domain.Id,Id,domainObject.Id,domainObject.Name,domainObject.Type));
    }
    internal void TryCreateInstance(string instanceName)
    {
        bool isExist = _domainObjects.Exists(d=>d.Name.Trim().ToLower() == instanceName.Trim().ToLower());
        if(!isExist)
            CreateInstance(instanceName);
    }
    private void CreateInstance(Guid instanceId,string instanceName)
    {
        BaseConcept instance = _concept.CreateInstance(instanceId,instanceName);
        DomainObject domainObject = DomainObject.Create(instance,new DomainConceptId(Id));
        _domainObjects.Add(domainObject);

        AddEvent(new DomainObjectCreated(_domain.Id,Id,domainObject.Id,domainObject.Name,domainObject.Type));
    }
    internal void TryCreateInstance(Guid instanceId,string instanceName)
    {
        bool isExist = _domainObjects.Exists(d=>d.Name.Trim().ToLower() == instanceName.Trim().ToLower() || d.Id == instanceId);
        if(!isExist)
            CreateInstance(instanceId,instanceName);

    }
    private void CreateInstance(string instanceName, List<DomainObjectProperty> domainObjectProperties, List<DomainObjectRelation> domainObjectRelations, List<DomainObjectRelationalDimension>? relationalDimesions)
    {
        BaseConcept instance = _concept.CreateInstance(instanceName);
        DomainObject domainObject = DomainObject.Create(instance,new DomainConceptId(Id));
        foreach(var property in domainObjectProperties)
        {
            domainObject.SetProperty(property.Name,property.Value);
        }
        foreach(var relation in domainObjectRelations)
        {
            domainObject.SetRelationTargetInstance(relation.RelationName,relation.RelationTarget,relation.TargetInstance);
        }                
        if(relationalDimesions!=null)
        {
            foreach(var relationalDimension in relationalDimesions)
            {
                domainObject.AddRelationalDimension(relationalDimension.Name,relationalDimension.Target);
            }
        }
        _domainObjects.Add(domainObject);
        AddEvent(new DomainObjectCreated(_domain.Id,Id,domainObject.Id,domainObject.Name,domainObject.Type));
    }
    internal void TryCreateInstance(string instanceName, List<DomainObjectProperty> domainObjectProperties, List<DomainObjectRelation> domainObjectRelations, List<DomainObjectRelationalDimension>? relationalDimesions){
        bool isExist = _domainObjects.Exists(d=>d.Name.Trim().ToLower() == instanceName.Trim().ToLower());
        if(!isExist)
            CreateInstance(instanceName,domainObjectProperties,domainObjectRelations,relationalDimesions);

    }

    internal void RemoveInstance(Guid domainObjectId)
    {
        var domainObject = _domainObjects.Find(d=>d.Id == domainObjectId);
        if(!Equals(domainObject,null))
        {
            
            _domainObjects.RemoveAll(d=>d.Id == domainObjectId);
            AddEvent(new DomainObjectRemoved(DomainId,Id,domainObjectId,domainObject.Name,domainObject.Type));
        }                
    }
    internal void Clear()
    {
        _domainObjects.Clear();
    }


    internal bool AddProperty(string propertyName, string propertyType)
    {
         return _concept.AddProperty(propertyName,propertyType);
    }
    internal bool AddProperty(Property property)
    {
        return _concept.AddProperty(property);
    }
    internal bool AddRelation(string relationName, string relationTarget, string multiplicity)
    {
        return _concept.AddRelation(relationName,relationTarget,multiplicity);
    }
    internal bool AddOperation(OperationName name, List<OperationInput> inputs, OperationOutput output)
    {
        return _concept.AddOperation(name,inputs,output);
    }
    internal bool AddOperation(Operation operation)
    {
        return _concept.AddOperation(operation);
    }

    public bool HasRelation(string relationName, string targetType)
    {
        return _concept.HasRelation(relationName,targetType);
    }
    public List<RelationTarget> GetRelationTarget(string relationName)
    {
        return _concept.GetRelationTargets(relationName);
    }

    internal DomainObject? GetDomainObjectByName(string domainObjectName)
    {
        return _domainObjects.Where(d=>d.Name.ToLower() == domainObjectName.ToLower()).FirstOrDefault();
    }
    internal bool HasInstace(Guid domainObjectId)
    {
        return _domainObjects.Exists(d=>d.Id == domainObjectId);
    }

    internal void UpdateInstance(Guid instanceId, string instanceName, List<DomainObjectProperty> properties, List<DomainObjectRelation> relations, List<DomainObjectRelationalDimension> relationalDimensions)
    {
        DomainObject? domainObject = _domainObjects.Where(d=>d.Id == instanceId)
                                                    .FirstOrDefault();
        
        if(Equals(domainObject,null))
            throw new Exception("Domain object not found");

        domainObject.Update(instanceName,properties,relations, relationalDimensions);
        AddEvent(new DomainObjectUpdated(DomainId,Id,domainObject.Id,domainObject.Name,domainObject.Type));
    }

    internal void SetRelationTargetInstance(string domainObjectName, string relationName, string relationTarget, string targetInstance)
    {
        DomainObject? domainObject = _domainObjects.Where(d=>d.Name.ToLower() == domainObjectName.ToLower())
                                                    .FirstOrDefault();
        
        if(Equals(domainObject,null))
            throw new Exception("Domain object not found");
        
        domainObject.SetRelationTargetInstance(relationName,relationTarget,targetInstance);
        _domainObjects.RemoveAll(d=>d.Id == domainObject.Id);
        _domainObjects.Add(domainObject);
    }
    internal void TrySetRelationTargetInstance(string domainObjectName, string relationName, string relationTarget, string targetInstance)
    {
        DomainObject? domainObject = _domainObjects.Where(d=>d.Name.ToLower() == domainObjectName.ToLower())
                                                    .FirstOrDefault();
        
        if(!Equals(domainObject,null))
        {
            domainObject.TrySetRelationTargetInstance(relationName,relationTarget,targetInstance);
            _domainObjects.RemoveAll(d=>d.Id == domainObject.Id);
            _domainObjects.Add(domainObject);
        }
        
    }

    public void SetAttribute(string name, string value)
    {
        _concept.SetAttribute(name,value);
    }

    internal void AppendAttributeValue(string attributeName, string attributeValue)
    {
        _concept.AppendAttributeValue(attributeName,attributeValue);
    }

    internal void TrySetDomainObjectProperty(string domainObjectName, string propertyName, string propertyValue)
    {
        DomainObject? domainObject = _domainObjects.Where(d=>d.Name.ToLower() == domainObjectName.ToLower())
                                                    .FirstOrDefault();
        if(!Equals(domainObject,null))
        {
            domainObject.TrySetProperty(propertyName,propertyValue);
        }
    }

    internal void TryAddRelationalDimension(string domainObjectName, string relationName, string relationTarget)
    {
        DomainObject? domainObject = _domainObjects.Where(d=>d.Name.ToLower() == domainObjectName.ToLower())
                                                    .FirstOrDefault();
        if(!Equals(domainObject,null))
        {
            domainObject.AddRelationalDimension(relationName,relationTarget);
        }
    }

    internal void TrySetOperationAttribute(string operationName, string attributeName, string attributeValue)
    {
        _concept.TrySetOperationAttribute(operationName,attributeName,attributeValue);
    }
}