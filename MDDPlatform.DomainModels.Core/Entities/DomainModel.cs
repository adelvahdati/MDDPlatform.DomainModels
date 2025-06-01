using MDDPlatform.BaseConcepts.Entities;
using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Actions;
using MDDPlatform.DomainModels.Core.Events;
using MDDPlatform.DomainModels.Core.ValueObjects;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DomainModels.Core.Entities;

public class DomainModel : BaseAggregate<Guid>
{
    private List<DomainConcept> _domainConcepts = new();

    public Domain Domain { get; private set; }
    public string Name { get; private set; }
    public string Tag { get; private set; }
    public string Type { get; private set; }
    public int Level { get; private set; }

    public IReadOnlyList<DomainConcept> DomainConcepts => _domainConcepts;
    public IReadOnlyList<Concept> Concepts => _domainConcepts.Select(d => Concept.Create(d.Id, d.Name, d.Type)).ToList();

    private DomainModel(Guid id, string name, string tag, string type, int level, Domain domain)
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Level = level;
        Domain = domain;
        _domainConcepts = new();
    }
    private DomainModel(Guid id, string name, string tag, string type, int level, List<DomainConcept> domainConcepts, Domain domain)
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Level = level;
        Domain = domain;
        _domainConcepts = domainConcepts;
    }


    public static DomainModel Create(Guid id, string name, string tag, string type, int level, Domain domain)
    {
        return new(id, name, tag, type, level, domain);
    }
    public static DomainModel Create(Guid id, string name, string tag, string type, int level, List<DomainConcept> domainConcepts, Domain domain)
    {
        return new(id, name, tag, type, level, domainConcepts, domain);
    }

    ////////////////////////////////////// Domain Concepts /////////////////////////////////

    public void CreateDomainConcept(BaseConcept concept)
    {
        var domainConcept = DomainConcept.Create(concept, Domain);
        _domainConcepts.Add(domainConcept);
        AddEvent(new DomainConceptCreated(Domain.Id, Id, domainConcept.Id, domainConcept.Name, domainConcept.Type));
    }
    public void CreateDomainConcepts(List<BaseConcept> concepts)
    {
        foreach (var concept in concepts)
        {
            CreateDomainConcept(concept);
        }
    }
    public bool TryCreateDomainConcept(string ConceptName,string ConceptType){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
        {
            var concept = BaseConcept.Create(ConceptName,ConceptType);
            domainConcept = DomainConcept.Create(concept, Domain);
            _domainConcepts.Add(domainConcept);
            return true;
        }
        return false;
    }

    public void UpdateModelLanguage(List<BaseConcept> concepts)
    {
        var domainObjects = GetDomainObjects();
        _domainConcepts.Clear();
        foreach (var concept in concepts)
        {
            var domainConcept = DomainConcept.Create(concept, Domain);
            _domainConcepts.Add(domainConcept);
        }
        foreach(var domainObject in domainObjects){
            TryCloneObject(domainObject);
        }        
    }

    public bool AddProperty(string ConceptName,string ConceptType,string propertyName,string propertyType){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");
        
        return domainConcept.AddProperty(propertyName,propertyType);
    }
    public bool TryAddProperty(string ConceptName,string ConceptType,Property property){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(!Equals(domainConcept,null))
        {
            return domainConcept.AddProperty(property);
        }
        return false;                
    }
    public void AddRelation(string ConceptName,string ConceptType, string RelationName,string RelationTarget,string Multiplicity){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");

        domainConcept.AddRelation(RelationName,RelationTarget,Multiplicity);
    }
    public void AddOperation(string ConceptName,string ConceptType,OperationName operationName,List<OperationInput> inputs,OperationOutput operationOutput)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");

        domainConcept.AddOperation(operationName,inputs,operationOutput);        
    }
    public void AddOperation(string ConceptName, string ConceptType, Operation operation)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");
        
        domainConcept.AddOperation(operation);
    }
    public void SetAttribute(string ConceptName,string ConceptType,string AttributeName , string AttributeValue){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");
        
        domainConcept.SetAttribute(AttributeName,AttributeValue);
    }
    public void TrySetAttribute(string ConceptName,string ConceptType,string AttributeName , string AttributeValue){
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(!Equals(domainConcept,null))
        {
            domainConcept.SetAttribute(AttributeName,AttributeValue);
        }        
    }
    public void AppendAttributeValue(string ConceptName,string ConceptType,string AttributeName , string AttributeValue)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Model does not suppert this concept");
        
        domainConcept.AppendAttributeValue(AttributeName,AttributeValue);
    }
    public void TryAppendAttributeValue(string ConceptName,string ConceptType,string AttributeName , string AttributeValue)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == ConceptName.ToLower() && d.Type.ToLower() == ConceptType.ToLower()).FirstOrDefault();
        if(!Equals(domainConcept,null))
        {
            domainConcept.AppendAttributeValue(AttributeName,AttributeValue);
        }                    
    }

    public void RemoveDomainConcept(string name, string type)
    {
        ConceptFullName fullName = ConceptFullName.Create(name,type);
        DomainConcept? domainConcept = GetDomainConcept(fullName);
        if(Equals(domainConcept,null))
            throw new Exception("The model does not contain this concept");
        
        _domainConcepts.RemoveAll(d=>d.Id == domainConcept.Id);        
    }

    public DomainConcept? GetDomainConcept(ConceptFullName fullName ){
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.FullName.Value.ToLower() == fullName.Value.ToLower()).FirstOrDefault();
        return domainConcept;
    }
    public List<DomainConcept> GetDomainConcepts(List<string> fullNames)
    {
        var lowerFullnames = fullNames.Select(fn=>fn.Trim().ToLower()).ToList();
        var domainConcepts = _domainConcepts.Where(d=> lowerFullnames.Contains(d.FullName.Value.ToLower())).ToList();
        return domainConcepts;
    }
    public IReadOnlyList<DomainConcept> GetDomainConceptByType(string type)
    {
        return _domainConcepts.Where(d=>d.Type.ToLower() == type.ToLower()).ToList();
    }
    public DomainConcept? GetDomainConceptById(Guid domainConceptId)
    {
        return _domainConcepts.Where(d=>d.Id == domainConceptId).FirstOrDefault();
    }
    public List<Instance> GetDomainConceptInstances(Guid domainConceptId)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Id == domainConceptId).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("Invalid Domain Concept ID");

        var instances = domainConcept.Instances.Select(domainObject=> Instance.Create(domainObject.Id,domainObject.Name,domainObject.Type)).ToList();
        return instances;

    }

    //////////////////////////////////////// Domain Objects ////////////////////////////////////////
    public void CreateDomainObject(string instanceType, string instanceName)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.FullName.Value.Trim().ToLower() == instanceType.Trim().ToLower()).FirstOrDefault();
        if (Equals(domainConcept, null))
            throw new Exception("Invalid Instance type : The model does not support this type");

        // domainConcept.CreateInstance(instanceName);
        domainConcept.TryCreateInstance(instanceName);

        var events = domainConcept.DomainEvents;
        foreach(var @event in events)
        {
            var payload = EventPayload.Create(@event);
            AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
        }
        domainConcept.ClearEvents();

    }
    public void TryCreateDomainObject(string instanceType,string instanceName)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.FullName.Value.Trim().ToLower() == instanceType.Trim().ToLower()).FirstOrDefault();

        if (!Equals(domainConcept, null))
        {
            domainConcept.TryCreateInstance(instanceName);

            var events = domainConcept.DomainEvents;
            foreach(var @event in events)
            {
                var payload = EventPayload.Create(@event);
                AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
            }
            domainConcept.ClearEvents();
        }
        
    }
    public void CreateDomainObject(string instanceType, string instanceName, Guid instanceId)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.FullName.Value.Trim().ToLower() == instanceType.Trim().ToLower()).FirstOrDefault();
        if (Equals(domainConcept, null))
            throw new Exception("Invalid Instance type : The model does not support this type");

        //domainConcept.CreateInstance(instanceId, instanceName);
        domainConcept.TryCreateInstance(instanceId, instanceName);

        var events = domainConcept.DomainEvents;
        foreach(var @event in events)
        {
            var payload = EventPayload.Create(@event);
            AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
        }
        domainConcept.ClearEvents();
    }
    public void CreateDomainObject(Guid domainConceptId,string instanceName, List<DomainObjectProperty> domainObjectProperties, List<DomainObjectRelation> domainObjectRelations,List<DomainObjectRelationalDimension>? relationalDimesions=null)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.Id == domainConceptId).FirstOrDefault();
        if (Equals(domainConcept, null))
            throw new Exception("Invalid Domain Concept ID : The model does not support this type");

        // domainConcept.CreateInstance(instanceName,domainObjectProperties,domainObjectRelations,relationalDimesions);
        domainConcept.TryCreateInstance(instanceName,domainObjectProperties,domainObjectRelations,relationalDimesions);
        var events = domainConcept.DomainEvents;
        foreach(var @event in events)
        {
            var payload = EventPayload.Create(@event);
            AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
        }
        domainConcept.ClearEvents();

        ResolveMissingTargetInstances(domainObjectRelations);
    }
    public IReadOnlyList<DomainObject> GetDomainObjects()
    {
        return _domainConcepts.SelectMany(domainConcept=>domainConcept.Instances).ToList();
    }
    public IReadOnlyList<DomainObject> GetDomainObjects(Guid DomainConceptId){
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.Id == DomainConceptId).FirstOrDefault();
        if (Equals(domainConcept, null))
            throw new Exception("Invalid instance type : Domain model does not support this type");

        return domainConcept.Instances;

    }
    public IReadOnlyList<DomainObject> GetDomainObjects(string type)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d => d.FullName.Value.Trim().ToLower() == type.Trim().ToLower()).FirstOrDefault();
        if (Equals(domainConcept, null))
            throw new Exception("Invalid instance type : Domain model does not support this type");

        return domainConcept.Instances;
    }
    public IReadOnlyList<DomainObject> GetDomainObjects(List<string> types)
    {
        List<string> lowerTypes = types.Select(t => t.ToLower().Trim()).ToList();
        List<DomainConcept> domainConcepts = _domainConcepts.Where(d => lowerTypes.Contains(d.FullName.Value.Trim().ToLower())).ToList();

        return domainConcepts.SelectMany(d => d.Instances).ToList();
    }
    public DomainObject GetDomainObjectById(Guid domainObjectId)
    {
        DomainObject? domainObject = _domainConcepts.SelectMany(domainConcept=>domainConcept.Instances)
                        .Where(domainObject=>domainObject.Id == domainObjectId)
                        .FirstOrDefault();
        if(Equals(domainObject,null))
            throw new Exception("Invalid domain object ID");

        return domainObject;
    }


    public void AddRelationalDimension(Guid domainObjectId, string relationName, string relationTarget)
    {
        DomainObject domainObject = GetDomainObjectById(domainObjectId);        
        domainObject.AddRelationalDimension(relationName,relationTarget);
    }
    public void TryAddRelationalDimension(string domainObjectType,string domainObjectName, string relationName, string relationTarget)
    {
        DomainConcept? domainConcept = GetDomainConcept(ConceptFullName.Create(domainObjectType));
        if(!Equals(domainConcept,null))
            domainConcept.TryAddRelationalDimension(domainObjectName,relationName,relationTarget);
    }
    public void ClearInstances()
    {
        foreach(var domainConcept in _domainConcepts){
            domainConcept.Clear();
        }
    }
    public void ClearConcepts(){
        _domainConcepts.Clear();
    }
    public void SetDomainObjectProperty(Guid domainObjectId, string propertyName, string propertyValue)
    {
        DomainObject domainObject = GetDomainObjectById(domainObjectId);
        domainObject.SetProperty(propertyName,propertyValue);
    }
    public void TrySetDomainObjectProperty(string domainObjectType,string domainObjectName, string propertyName, string propertyValue)
    {
        DomainConcept? domainConcept = GetDomainConcept(ConceptFullName.Create(domainObjectType));
        if(!Equals(domainConcept,null))
        {
            domainConcept.TrySetDomainObjectProperty(domainObjectName,propertyName,propertyValue);
        }
            
    }

    public void SetRelationTargetInstance(Guid domainObjectId, string relationName, string relationTarget, string targetInstance)
    {
        DomainObject domainObject = GetDomainObjectById(domainObjectId);
        domainObject.SetRelationTargetInstance(relationName,relationTarget,targetInstance);
        ResolveMissingTargetInstances(relationTarget,targetInstance);        
    }
    public void TrySetRelationTargetInstance(Guid domainObjectId, string relationName, string relationTarget, string targetInstance)
    {
        DomainObject domainObject = GetDomainObjectById(domainObjectId);
        domainObject.TrySetRelationTargetInstance(relationName,relationTarget,targetInstance);        
        ResolveMissingTargetInstances(relationTarget,targetInstance);        
    }
    public void SetRelationTargetInstance(string domainObjectType,string domainObjectName, string relationName, string relationTarget, string targetInstance)
    {
        DomainConcept? domainConcept = GetDomainConcept(ConceptFullName.Create(domainObjectType));
        if(Equals(domainConcept,null))
            throw new Exception($"The model does not support {domainObjectType} type");
        
        domainConcept.SetRelationTargetInstance(domainObjectName,relationName,relationTarget,targetInstance);
        ResolveMissingTargetInstances(relationTarget,targetInstance);        
    }
    public void TrySetRelationTargetInstance(string domainObjectType,string domainObjectName, string relationName, string relationTarget, string targetInstance)
    {
        DomainConcept? domainConcept = GetDomainConcept(ConceptFullName.Create(domainObjectType));
        if(!Equals(domainConcept,null)){
            domainConcept.TrySetRelationTargetInstance(domainObjectName,relationName,relationTarget,targetInstance);
            ResolveMissingTargetInstances(relationTarget,targetInstance);        
        }
    }


    public void RemoveDomainObject(Guid domainObjectId)
    {
        DomainObject domainObject = GetDomainObjectById(domainObjectId);
        ConceptFullName fullName = ConceptFullName.Create(domainObject.Type);
        DomainConcept? domainConcept = GetDomainConcept(fullName);
        if(Equals(domainConcept,null))
            throw new Exception("The model does not contain this element");
        
        domainConcept.RemoveInstance(domainObjectId);
        
        var events = domainConcept.DomainEvents;
        foreach(var @event in events)
        {
            var payload = EventPayload.Create(@event);
            AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
        }
        domainConcept.ClearEvents();

        //ToDo : Resolve relation target inconsistencies that happern when an object removed                        
    }
    public void UpdateDomainObject(Guid domainObjectId,string instanceName, List<DomainObjectProperty> properties, List<DomainObjectRelation> relations, List<DomainObjectRelationalDimension> relationalDimensions)
    {
        
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.HasInstace(domainObjectId)).FirstOrDefault();
        if(Equals(domainConcept,null))
            throw new Exception("The model does not contain this element");

        domainConcept.UpdateInstance(domainObjectId, instanceName,properties,relations,relationalDimensions);

        var events = domainConcept.DomainEvents;
        foreach(var @event in events)
        {
            var payload = EventPayload.Create(@event);
            AddEvent(new DomainModelUpdated(Domain.Id,Id,Name,Type,Tag,payload));
        }
        domainConcept.ClearEvents();

        ResolveMissingTargetInstances(relations);        
    }

    public List<string> GetObjectTypes()
    {
        return _domainConcepts.Select(d=>d.FullName.Value.ToLower())
                                .ToList();
    }
    public bool SupportObjectType(string objectType)
    {
        return _domainConcepts.Exists(d=>d.FullName.Value.ToLower() == objectType.ToLower());
    }

    public void Execute(Instruction instruction)
    {
        var factory = new ActionResolverFactory();
        IActionResolver actionResolver = factory.Create(instruction);
        actionResolver.Handle(this, instruction);
    }

    public void TryCloneObject(DomainObject domainObject)
    {
        var domainObjectType = domainObject.Type;
        var domainObjectName = domainObject.Name;

        if(!SupportObjectType(domainObjectType))
            return;
        
        TryCreateDomainObject(domainObjectType,domainObjectName);
        var properties = domainObject.Properties;
        foreach(var prop in properties)
        {
            if(prop.Value.Value!=null)
                TrySetDomainObjectProperty(domainObjectType,domainObjectName,prop.Name.Value,prop.Value.Value);
        }    
        var relations = domainObject.Relations;
        foreach(var rel in relations){
            var targetInstances = rel.GetTargetInstances();
            foreach(var targetInstance in targetInstances)
            {
                TrySetRelationTargetInstance(domainObjectType,domainObjectName,rel.Name,rel.Target,targetInstance.Name);
            }
        }
    }
    private void ResolveMissingTargetInstances(List<DomainObjectRelation> domainObjectRelations){
        string[] targetInstances;
        foreach(var rel in domainObjectRelations)
        {
            var targetInstance = rel.TargetInstance;
            if(targetInstance.Contains(","))
                targetInstances = targetInstance.Split(",");
            else
                targetInstances = new string[] {targetInstance};

            foreach(var item in targetInstances){
                TryCreateDomainObject(rel.RelationTarget.Trim(),item.Trim());
            }
        }
    }
    private void ResolveMissingTargetInstances(DomainObjectRelation domainObjectRelation){
        string[] targetInstances;
            var targetInstance = domainObjectRelation.TargetInstance;
            if(targetInstance.Contains(","))
                targetInstances = targetInstance.Split(",");
            else
                targetInstances = new string[] {targetInstance};

            foreach(var item in targetInstances){
                TryCreateDomainObject(domainObjectRelation.RelationTarget.Trim(),item.Trim());
            }
    }    
    private void ResolveMissingTargetInstances(string RelationTarget,string TargetInstance){
        string[] targetInstances;
            var targetInstance = TargetInstance;
            if(targetInstance.Contains(","))
                targetInstances = targetInstance.Split(",");
            else
                targetInstances = new string[] {targetInstance};

            foreach(var item in targetInstances){
                TryCreateDomainObject(RelationTarget.Trim(),item.Trim());
            }
    }

    public void TrySetOperationAttribute(string conceptName, string conceptType, string operationName, string attributeName, string attributeValue)
    {
        DomainConcept? domainConcept = _domainConcepts.Where(d=>d.Name.ToLower() == conceptName.ToLower() && d.Type.ToLower() == conceptType.ToLower()).FirstOrDefault();
        if(!Equals(domainConcept,null))
        {
            domainConcept.TrySetOperationAttribute(operationName,attributeName,attributeValue);
        }        
    }
}