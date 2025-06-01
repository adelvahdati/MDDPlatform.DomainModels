using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Exceptions;
using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;

public class Instances
{
    private SetOfValueObject<Instance> _instances=new();
    internal void AddInstance(Instance instance)
    {
        if(!_instances.Add(instance))
            throw new DuplicateDomainObjectError("There is a Domain object with the same name and type",instance.Name,instance.Type);
    }

    internal void RemoveInstance(Instance instance)
    {
        if(!_instances.Remove(instance))
            throw new DomainObjectNotExistError("Domain object doesn't exist in this model",instance.Name,instance.Type);
    }
    internal Instance GetInstance(string name,string type)
    {
        Func<Instance, bool> predicate = 
            instance=> instance.Name == name && instance.Type == type;

        Instance? instance =  _instances.Get(predicate);
        if(Equals(instance,null))
            throw new DomainObjectNotExistError("Domain object doesn't exist in this model",name,type);
        
        return instance;

    }
    public List<Instance> ToList(){
        return _instances.ToList();        
    }

    internal void Clear()
    {
        _instances = new();
    }
}