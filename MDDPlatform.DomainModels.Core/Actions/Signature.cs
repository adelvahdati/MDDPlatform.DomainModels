using MDDPlatform.DomainModels.Core.ValueObjects;

namespace MDDPlatform.DomainModels.Core.Actions;

public class Signature
{
    public string Code {get;set;}
    public int NumberOfArguments {get;set;}

    public Signature(string code, int numberOfArguments){
        Code= code;
        NumberOfArguments = numberOfArguments;
    }
    public static Signature CreateFrom(Instruction instruction)
    {
        int argNumbers = instruction.Arguments == null? 0 : instruction.Arguments.Count;
        return new(instruction.Code,argNumbers);
    }
    public override string ToString()
    {
        return string.Format("{0}_{1}",Code,NumberOfArguments).ToLower();
    }
}