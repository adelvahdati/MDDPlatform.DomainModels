using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Core.Entities;
public class DomainObject : BaseAggregate<Guid>
{
    private BaseConcept _instance;
    private DomainConceptId _domainConceptId;
    private RelationalDimensions _dimensions;

    public string Name => _instance.Name.Value;
    public string Type => _instance.Type.Value;
    public DomainConceptId DomainConceptId => _domainConceptId;
    public ConceptFullName FullName => _instance.FullName;


    public IReadOnlyList<Property> Properties => _instance.GetProperties();
    public IReadOnlyList<Relation> Relations => _instance.GetRelations();
    public IReadOnlyList<RelationalDimension> RelationalDimensions => _dimensions.ToList();
    public IReadOnlyList<Operation> Operations => _instance.GetOperations();


    private DomainObject(BaseConcept instance, DomainConceptId domainConceptId)
    {
        Id = instance.Id;
        _instance = instance;
        _domainConceptId = domainConceptId;
        _dimensions = new();
    }
    private DomainObject(BaseConcept instance, DomainConceptId domainConceptId, List<RelationalDimension> relationalDimensions)
    {
        Id = instance.Id;
        _instance = instance;
        _domainConceptId = domainConceptId;
        _dimensions = new();
        foreach (var dimension in relationalDimensions)
        {
            _dimensions.AddNewDimension(dimension);
        }
    }



    public static DomainObject Create(BaseConcept instance, DomainConceptId domainConceptId)
    {
        return new DomainObject(instance, domainConceptId);
    }
    public static DomainObject Load(BaseConcept instance, DomainConceptId domainConceptId, List<RelationalDimension> relationalDimensions)
    {
        return new(instance, domainConceptId, relationalDimensions);
    }


    public void SetProperty(string propertyName, string value)
    {
        _instance.SetPropertyValue(propertyName, value);
    }
    public void TrySetProperty(string propertyName, string value)
    {
        _instance.TrySetPropertyValue(propertyName, value);
    }
    public void SetRelationTargetInstance(string relationName, string relationTarget, string targetInstance)
    {
        _instance.SetRelationTargetInstance(relationName, relationTarget, targetInstance);
    }
    internal void TrySetRelationTargetInstance(string relationName, string relationTarget, string targetInstance)
    {
        _instance.TrySetRelationTargetInstance(relationName, relationTarget, targetInstance);
    }
    internal void AddRelationalDimension(string relationName, string relationTarget)
    {
        RelationalDimension dimension = RelationalDimension.Create(relationName, relationTarget);
        _dimensions.AddNewDimension(dimension);
    }


    public List<TargetInstance> GetTargetInstances(string relationName, string relationTarget)
    {
        return _instance.GetTargetInstances(relationName, relationTarget);
    }
    public List<TargetInstance> GetTargetInstances(string relationName)
    {
        return _instance.GetTargetInstances(relationName);
    }
    public bool IsRelatedTo(ConceptFullName targetInstanceFullName, string relationName, string targetType)
    {
        return _instance.IsRelatedTo(targetInstanceFullName, relationName, targetType);
    }

    public bool IsRelatedTo(ConceptFullName targetInstanceFullName, string relationName)
    {
        return _instance.IsRelatedTo(targetInstanceFullName, relationName);
    }

    public string? GetPropertyValue(string propertyName)
    {
        return _instance.GetPropertyValue(propertyName);
    }

    public List<string> GetRelationalTargetInstances(string relationalDimension)
    {
        return _dimensions.GetTargetInstances(relationalDimension);
    }

    internal void Update(string instanceName, List<DomainObjectProperty> properties, List<DomainObjectRelation> relations, List<DomainObjectRelationalDimension> relationalDimensions)
    {
        _instance.SetName(instanceName);
        foreach(var prop in properties){
            _instance.SetPropertyValue(prop.Name,prop.Value);
        }
        _instance.ClearTargetInstances();
        foreach(var rel in relations){
            _instance.SetRelationTargetInstance(rel.RelationName,rel.RelationTarget,rel.TargetInstance);
        }
        _dimensions = new();
        foreach(var relD in relationalDimensions){
            _dimensions.AddNewDimension(RelationalDimension.Create(relD.Name,relD.Target));
        }
    }
}