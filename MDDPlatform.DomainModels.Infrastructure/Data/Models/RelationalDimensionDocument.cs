using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models;
public class RelationalDimensionDocument
{
    public string Name{get; set;}
    public string Target {get; set;}

    private RelationalDimensionDocument(string name, string target)
    {
        Name = name;
        Target = target;
    }

    public static RelationalDimensionDocument CreateFrom(RelationalDimension relationalDimension){
        return new RelationalDimensionDocument(relationalDimension.Name,relationalDimension.Target);
    }
    public RelationalDimension ToRelationalDimension(){
        return RelationalDimension.Create(Name,Target);
    }
}