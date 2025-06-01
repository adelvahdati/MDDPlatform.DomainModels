namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
using Attribute = MDDPlatform.BaseConcepts.ValueObjects.Attribute;
public class AttributeDocument 
{
    public string Name { get; set;}
    public string Value { get; set;}

    private AttributeDocument(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public static AttributeDocument CreateFrom(Attribute attribute)
    {
        return new AttributeDocument(attribute.Name,attribute.Value);
    }

    internal Attribute ToAttribute()
    {
        return new Attribute(Name,Value);
    }
}