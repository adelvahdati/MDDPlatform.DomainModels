using MDDPlatform.SharedKernel.ValueObjects;

namespace MDDPlatform.DomainModels.Core.ValueObjects;
public class Instruction : ValueObject
{
    public string Code { get; set; }
    public List<string> Arguments { get;  set; }

    public  Instruction()
    {
        Code = string.Empty;
        Arguments = new();
    }
    protected Instruction(string code, params string[] arguments)
    {
        Code = code;
        Arguments = arguments.ToList();
    }
    protected Instruction(string code, List<string> arguments)
    {
        Code = code;
        Arguments = arguments;
    }
    public static Instruction Load(string code,List<string> arguments)
    {
        return new(code,arguments);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return string.Join(",", Arguments);
    }
}