using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Application.DTO;

public class RelationalDimensionDto
{
    public string Name{get; set;}
    public string Target {get; set;}

    public RelationalDimensionDto(string name, string target)
    {
        Name = name;
        Target = target;
    }

    public static RelationalDimensionDto CreateFrom(RelationalDimension relationalDimension){
        return new RelationalDimensionDto(relationalDimension.Name,relationalDimension.Target);
    }
    public RelationalDimension ToRelationalDimension(){
        return RelationalDimension.Create(Name,Target);
    }

}