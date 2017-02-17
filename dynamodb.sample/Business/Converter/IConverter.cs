using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Converter
{
    public interface IConverter<T>
    {
        Document ConvertToDocument(T recomendacao);
        T ConvertToDomain(Dictionary<string, AttributeValue> item);
        T ConvertToDomain(Document doc);
    }
}
