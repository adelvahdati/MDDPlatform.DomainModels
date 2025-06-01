using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class RelationalDimensions
{
    private SetOfValueObject<RelationalDimension> _dimensions;

    public RelationalDimensions(SetOfValueObject<RelationalDimension> dimensions)
    {
        _dimensions = dimensions;
    }
    public RelationalDimensions()
    {
        _dimensions =new();
    }
    public void AddNewDimension(RelationalDimension dimension)
    {
        _dimensions.Add(dimension);
    }
    public List<RelationalDimension> ToList()
    {
        return _dimensions.ToList();
    }
    public List<string> GetTargetInstances(string relationName)
    {
        return _dimensions.List(rd=>rd.Name.Value.ToLower() == relationName.ToLower())
                    .Select(rd=>rd.Target.Value.ToLower())
                    .ToList();
    }
}