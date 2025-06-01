using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Core.Entities;

namespace MDDPlatform.DomainModels.Application;
public static class Extensions
{
    public static string ResolveExpression(this string expression, Dictionary<string,string> keyValues)
    {
        
        string expr = expression;
        var terms = expression.Split('+');        
        foreach(var term in terms)
        {
            if(keyValues.ContainsKey(term))
                expr = expr.Replace(term,keyValues[term]);
        }
        expr = expr.Replace("+","");
        return expr;        
    }
    public static Dictionary<string,string> ToKeyValueExpressionResolver(this DomainObject domainObject, string variableName)
    {
        Dictionary<string,string> keyValues = new();
        string key = string.Format("{0}.{1}",variableName,"Name");
        string? value = domainObject.Name;
        keyValues.Add(key,value);

        key = string.Format("{0}.{1}",variableName,"_Type");
        value = domainObject.Type;
        keyValues.Add(key,value);


        foreach(var prop in domainObject.Properties)
        {
            value = domainObject.GetPropertyValue(prop.Name.Value);
            if(value!=null)
            {
                key = string.Format("{0}.{1}",variableName,prop.Name.Value);
                if(!keyValues.ContainsKey(key))
                    keyValues.Add(key,value);
            }    
        }
        foreach(var rel in domainObject.Relations)
        {
            var targetInstances = rel.GetTargetInstances().Select(ti=>ti.Name).ToList();
            if(targetInstances.Count>0)
            {
                key = string.Format("{0}.{1}({2})",variableName,rel.Name.Value,rel.Target.Value);
                value = string.Join(",",targetInstances);
                if(!keyValues.ContainsKey(key))
                    keyValues.Add(key,value);
            }
        }

        return keyValues;
    }
    public static Dictionary<string,string> ToKeyValueExpressionResolver(this DomainObjectDto domainObject, string variableName)
    {
        Dictionary<string,string> keyValues = new();
        string key = string.Format("{0}.{1}",variableName,"Name");
        string? value = domainObject.InstanceName;
        keyValues.Add(key,value);

        key = string.Format("{0}.{1}",variableName,"_Type");
        value = domainObject.InstanceType;
        keyValues.Add(key,value);

        foreach(var prop in domainObject.Properties)
        {
            value = prop.Value;
            if(value!=null)
            {
                key = string.Format("{0}.{1}",variableName,prop.Name);
                if(!keyValues.ContainsKey(key))
                    keyValues.Add(key,value);
            }    
        }
        foreach(var rel in domainObject.Relations)
        {
            if(rel.TargetInstances.Count>0)
            {
                key = string.Format("{0}.{1}({2})",variableName,rel.RelationName,rel.RelationTarget);
                value = string.Join(",",rel.TargetInstances);
                if(!keyValues.ContainsKey(key))
                    keyValues.Add(key,value);
            }
        }
        return keyValues;
    }
    public static Dictionary<string,string> AppendKeyValueExpressionResolver(this Dictionary<string,string> keyValues,DomainObject domainObject,string variableName)
    {
        var items = domainObject.ToKeyValueExpressionResolver(variableName);
        foreach(var item in items)
        {
            if(!keyValues.ContainsKey(item.Key))
                keyValues.Add(item.Key,item.Value);
        }
        return keyValues;
    }
}