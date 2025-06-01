using MDDPlatform.BaseConcepts.ValueObjects;
using Attribute = MDDPlatform.BaseConcepts.ValueObjects.Attribute;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;

public class PropertyDocument
{
    public string Name {get;set;}
    public string Type {get;set;}
    public bool IsCollection {get;set;} = false;
    public string? Value {get;set;}
    public List<AttributeDocument> Attributes { get; set; }


    private PropertyDocument(string name, string type,string? value,bool isCollection = false, List<AttributeDocument>? attributes =null)
    {
        Name = name;
        Type = type;
        IsCollection = isCollection;
        Value = value;
        Attributes = attributes == null ? new() : attributes;
    }
    public static PropertyDocument CreateFrom(Property property)
    {
        var name = property.Name;
        var type = property.Type.Value;
        var isCollection = property.Type.IsCollection;
        PropertyValue propValue = property.Value;
        string? value = propValue.Value;
        var attributes = property.Attributes.Select(attr=>AttributeDocument.CreateFrom(attr)).ToList();
        return new PropertyDocument(name,type,value,isCollection,attributes);
    }
    internal Property ToProperty()
    {
        var property =  Property.Create(Name,Type,IsCollection);
        if(Value !=null)
            property.SetValue(Value);
        if(Attributes == null)
            return property;
            
        foreach (var attribute in Attributes)
        {
            property.SetAttribute(attribute.Name, attribute.Value);
        }                
        return property;
    }
}