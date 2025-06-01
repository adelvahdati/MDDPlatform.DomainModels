using MDDPlatform.BaseConcepts.ValueObjects;
using MDDPlatform.DomainModels.Core.Exceptions;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class Concepts
{
    private SetOfValueObject<Concept> _Concepts = new();
    internal void AddConcept(Concept concept)
    {
        if(!_Concepts.Add(concept))
            throw new DuplicateDomainConceptError("There is a DomainConcept with the same name and type",concept.Name,concept.Type);
    }

    internal void RemoveConcept(Concept concept)
    {
        if(!_Concepts.Remove(concept))
            throw new DomainConceptNotExistError("Domain concept doesn't exist in this model",concept.Name,concept.Type);
    }
    internal Concept GetConcept(string name,string type)
    {
        Func<Concept, bool> predicate = 
            concept=> concept.Name == name && concept.Type == type;

        Concept? concept =  _Concepts.Get(predicate);
        if(Equals(concept,null))
            throw new DomainConceptNotExistError("Domain concept doesn't exist in this model",name,type);

        return concept;
    }

    internal Concept GetConcept(string fullName)
    {
        Concept? concept =  _Concepts.Get(c=> c.FullName.Value.ToLower().Trim() == fullName.ToLower().Trim());
        if(Equals(concept,null))
            throw new Exception("Domain concept doesn't exist in this model");

        return concept;
    }

    internal List<Concept> ToList()
    {
        return _Concepts.ToList();
    }
}