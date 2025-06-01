using MDDPlatform.BaseConcepts.ValueObjects;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Models
{
    public class OperationDocument
    {
        public string Name { get; set; }
        public List<OperationInputDocument> Inputs { get; set; }
        public OperationOutputDocument Output { get; set; }
        public List<AttributeDocument> Attributes { get; set; }

        private OperationDocument(string name, List<OperationInputDocument> inputs, OperationOutputDocument output, List<AttributeDocument>? attributes = null)
        {
            Name = name;
            Inputs = inputs;
            Output = output;
            Attributes = attributes == null ? new() : attributes;
        }
        public static OperationDocument CreateFrom(Operation operation)
        {
            var name = operation.Name.Value;
            var inputs = operation.Inputs.Select(input => OperationInputDocument.CreateFrom(input)).ToList();
            var output = OperationOutputDocument.CreateFrom(operation.Output);
            var attributes = operation.Attributes.Select(attr=>AttributeDocument.CreateFrom(attr)).ToList();
            return new OperationDocument(name, inputs, output, attributes);

        }
        public Operation ToOperation()
        {
            OperationName name = OperationName.Create(Name);
            var inputs = Inputs.Select(inputDoc => inputDoc.ToOperationInput()).ToList();
            var output = Output.ToOperationOutput();
            var operation = Operation.Create(name, inputs, output);
            if(Attributes == null)
                return operation;
            foreach (var attribute in Attributes)
            {
                operation.SetAttribute(attribute.Name, attribute.Value);
            }
            return operation;
        }
    }
    public class OperationInputDocument
    {
        public string Name { get; set; }
        public string Type { get; set; }

        private OperationInputDocument(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public static OperationInputDocument CreateFrom(OperationInput input)
        {
            return new OperationInputDocument(input.Name, input.Type);
        }
        public OperationInput ToOperationInput()
        {
            return OperationInput.Create(Name, Type);
        }
    }

    public class OperationOutputDocument
    {
        public string Type { get; set; }
        public bool IsCollection { get; set; }

        private OperationOutputDocument(string type, bool isCollection)
        {
            Type = type;
            IsCollection = isCollection;
        }
        public static OperationOutputDocument CreateFrom(OperationOutput output)
        {
            return new OperationOutputDocument(output.Type, output.IsCollection);
        }
        public OperationOutput ToOperationOutput()
        {
            return OperationOutput.Create(Type, IsCollection);
        }
    }

}